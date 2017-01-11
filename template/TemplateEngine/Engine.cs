using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUglify;

namespace TemplateEngine
{
    public partial class Engine
    {
        public readonly string OutputDirectory;
        public readonly string WorkingDirectory;
        public readonly Dictionary<string, string> FileFormatsDictionary;
        public readonly List<string> NoMinList;
        public readonly Dictionary<string, string> Templates = new Dictionary<string, string>(); // name, value
        private readonly bool _useMultipleThreads;
        private List<string> _warnings = new List<string>();
        private ThreadManager _threadManager;

        public readonly Dictionary<string, Dictionary<string, string>> Pages =
            new Dictionary<string, Dictionary<string, string>>(); // name -> use,title,body etc

        public Engine(string outputdirectory, string workingDirectory, List<string> noMin1,
            Dictionary<string, string> fileFormatsDictionary, bool useMultipleThreads)
        {
            OutputDirectory = outputdirectory;
            WorkingDirectory = workingDirectory;
            NoMinList = noMin1;
            _useMultipleThreads = useMultipleThreads;
            FileFormatsDictionary = fileFormatsDictionary;
            if (!FileFormatsDictionary.ContainsKey("default"))
                FileFormatsDictionary.Add("default", "%n.html"); // Add default item

            _threadManager = useMultipleThreads ? new ThreadManager() : new ThreadManager(10, 0);

            GetFiles();
        }

        /// <summary>
        /// Make sure all arguments are valid before generating
        /// </summary>
        /// <returns>Error message. Null on success</returns>
        public string VerifyArgs()
        {
            foreach (var fileFormat in FileFormatsDictionary)
            {
                if (!fileFormat.Value.Contains("%n"))
                {
                    return "File format must contain %n (see usage)";
                }
            }

            if (!Directory.Exists(WorkingDirectory))
            {
                return "Working directory must exist";
            }

            return null;
        }

        /// <summary>
        /// Delete all files in the output directory
        /// </summary>
        /// <returns>Error message. Null on success</returns>
        public string CleanOutput()
        {
            if (!Directory.Exists(OutputDirectory))
                return null;
            try
            {
                Clean(OutputDirectory);
            }
            catch (Exception ex)
            {
                return "Unable to clean directory " + OutputDirectory + "\r\n" + ex.Message;
            }
            return null;
        }

        private void Clean(string directory)
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                File.Delete(file);
            }
            foreach (var d in Directory.GetDirectories(directory))
            {
                Clean(d);
            }
            Directory.Delete(directory);
        }

        public void GetFiles()
        {
            foreach (var file in Directory.GetFiles(WorkingDirectory + "templates"))
            {
                try
                {
                    Templates.Add(file.Substring(file.LastIndexOf(Path.DirectorySeparatorChar) + 1).ToLower(),
                        File.ReadAllText(file));
                }
                catch (Exception ex)
                {
                    _warnings.Add("Unable to read file: " + file + "\r\n" + ex.Message);
#if DEBUG
                    throw;
#endif
                }
            }
            foreach (var file in Directory.GetFiles(WorkingDirectory + "pages"))
            {
                try
                {
                    var n = file.Substring(file.LastIndexOf(Path.DirectorySeparatorChar) + 1).Split('.');
                    if (n.Length < 2)
                        continue;

                    if (!Pages.ContainsKey(n[0].ToLower()))
                        Pages.Add(n[0].ToLower(), new Dictionary<string, string>());

                    Pages[n[0].ToLower()].Add(n[1].ToLower(), File.ReadAllText(file));
                }
                catch (Exception ex)
                {
                    _warnings.Add("Unable to read file: " + file + "\r\n" + ex.Message);
#if DEBUG
                    throw;
#endif
                }
            }
        }

        public string GetPropertyValue(string pageName, string propertyName)
        {
            // Make case insensitive
            pageName = pageName.ToLower();
            propertyName = propertyName.ToLower();

            if (Pages.ContainsKey(pageName) && Pages[pageName].ContainsKey(propertyName))
                return Pages[pageName][propertyName];

            if (Pages.ContainsKey("default") && Pages["default"].ContainsKey(propertyName))
                return Pages["default"][propertyName];

            _warnings.Add("No value or default value for " + pageName + "." + propertyName);
            return String.Empty;
        }

        private string ParseTemplateData(string templateData, string pageName)
        {
            // Parse menu content
            var data = ParseMenuForeach(templateData, pageName);

            // Parse other page-specific template data
            var pattern = @"\{\{t ([a-zA-Z0-9_]+)\.([a-zA-Z0-9_]+)\}\}";
            return Regex.Replace(data, pattern, m => ParseTemplateRegex(m, pageName));
        }

        private string ParseMarkdownData(string templateData) => templateData == String.Empty
            ? String.Empty
            : Markdig.Markdown.ToHtml(templateData);

        private bool ShouldMinPage(string pageName)
        {
            var templateName = GetPropertyValue(pageName, "use");

            return !NoMinList.Contains(templateName);
        }

        private string FormatOutputFilename(string name)
        {
            var templateName = GetPropertyValue(name, "use");

            string format = FileFormatsDictionary.ContainsKey(templateName)
                ? FileFormatsDictionary[templateName]
                : FileFormatsDictionary["default"]; // Always contains "default" value

            return format.Replace("%n", name);
        }

        private string ParseTemplateRegex(Match m, string pageName)
        {
            if (m.Groups[1].Value == "page")
                return ParseTemplateData(GetPropertyValue(pageName, m.Groups[2].Value), pageName);

            if (m.Groups[1].Value == "pagemd")
                return ParseMarkdownData(GetPropertyValue(pageName, m.Groups[2].Value));

            if (m.Groups[1].Value == "a")
                return FormatOutputFilename(m.Groups[2].Value);

            if (m.Groups[1].Value == "template" && Templates.ContainsKey(m.Groups[2].Value))
                return ParseTemplateData(Templates[m.Groups[2].Value], pageName);

            _warnings.Add("Invalid property name " + m.Groups[1].Value + "." + m.Groups[2].Value);
            return String.Empty;
        }

        public void ParsePageAndSave(string pageName)
        {
            var template = GetPropertyValue(pageName, "use");
            var content = ParseTemplateData(Templates[template], pageName);
            try
            {
                // Minify output
                if (ShouldMinPage(pageName))
                    content = Uglify.Html(content).ToString();

                File.WriteAllText(OutputDirectory + FormatOutputFilename(pageName), content);
            }
            catch (Exception ex)
            {
                _warnings.Add("Unable to write page: " + pageName + "\r\n" + ex.Message);
#if DEBUG
                throw;
#endif
            }
        }

        /// <summary>
        /// Save a page and all its parts to the filesystem.
        /// </summary>
        /// <param name="pageName">The name of the page to save.</param>
        /// <returns>A list of all warnings.</returns>
        public List<string> SavePage(string pageName)
        {
            _warnings = new List<string>();
            if (!Pages.ContainsKey(pageName))
                throw new KeyNotFoundException($"The page {pageName} could not be found.");
            var pageItems = Pages[pageName];
            var pagePath = $"{WorkingDirectory}pages{Path.DirectorySeparatorChar}{pageName}";
            foreach (var pageItem in pageItems)
            {
                var path = pagePath + "." + pageItem.Key;
                try
                {
                    File.WriteAllText(path, pageItem.Value);
                }
                catch (Exception ex)
                {
                    _warnings.Add($"Unable to save page item: {pageName}.{pageItem.Key}\r\n{ex.Message}");
#if DEBUG
                    throw;
#endif
                }
            }
            return _warnings;
        }

        /// <summary>
        /// Saves the file minify data to the fileformats file.
        /// </summary>
        public void SaveFileFormatMinifyData()
        {
            var lines = new Dictionary<string, string>();

            foreach (var format in FileFormatsDictionary)
            {
                // Don't save default value.
                if (format.Key == "default" && format.Value == "%n.html")
                    continue;

                if (lines.ContainsKey(format.Key))
                    lines[format.Key] = ":" + format.Value;
                else
                    lines.Add(format.Key, ":" + format.Value);
            }

            foreach (var b in NoMinList)
            {
                if (lines.ContainsKey(b))
                    lines[b] += ":nomin";
                else
                    lines.Add(b, ":nomin");
            }

            string text = String.Empty;
            foreach (var line in lines)
            {
                text += line.Key + line.Value + Environment.NewLine;
            }
            File.WriteAllText(WorkingDirectory + "fileformats", text);
        }

        /// <summary>
        /// Save all the template contents to the file system.
        /// </summary>
        /// <returns>A list of all warnings.</returns>
        public List<string> SaveTemplates()
        {
            _warnings = new List<string>();
            var templateDir = $"{WorkingDirectory}{Path.DirectorySeparatorChar}templates{Path.DirectorySeparatorChar}";

            foreach (var template in Templates)
            {
                try
                {
                    File.WriteAllText(templateDir + template.Key, template.Value);
                }
                catch (Exception ex)
                {
                    _warnings.Add("Unable to save template file " + template.Key + "\r\n" + ex.Message);
                }
            }
            return _warnings;
        }

        /// <summary>
        /// Saves all template, page and fileformat files to the file system.
        /// </summary>
        /// <returns>A list of all warnings.</returns>
        public List<string> SaveAllFiles()
        {
            var w = new List<string>();
            w.AddRange(SaveTemplates());
            foreach (var page in Pages)
            {
                w.AddRange(SavePage(page.Key));
            }
            SaveFileFormatMinifyData();

            return w;
        }

        public void RemoveTemplateAndPages(string templateName)
        {
            if (!Templates.ContainsKey(templateName))
                return;

            List<string> toRemove = new List<string>();
            foreach (var page in Pages)
            {
                if (!(page.Value.ContainsKey("use") && page.Value["use"] == templateName))
                    continue;

                toRemove.Add(page.Key);
            }

            foreach(var page in toRemove)
                RemovePage(page);

            Templates.Remove(templateName);
            File.Delete(WorkingDirectory + "templates" + Path.DirectorySeparatorChar + templateName);
        }

        public void RemovePage(string pageName)
        {
            var pagePath = WorkingDirectory + "pages" + Path.DirectorySeparatorChar + pageName;
            foreach (var part in Pages[pageName].Keys)
            {
                File.Delete(pagePath + "." + part);
            }

            Pages.Remove(pageName);
        }

        public void RemovePagePart(string pageName, string partName)
        {
            var path = WorkingDirectory + "pages" + Path.DirectorySeparatorChar + pageName + "." + partName;
            File.Delete(path);
            Pages[pageName].Remove(partName);
        }

        /// <summary>
        /// Gets a dictionary of all pages using a specific template. Keys are the page name and values are a 
        /// dictionary of all the page parts.
        /// </summary>
        /// <param name="templateName">The template name.</param>
        /// <returns>A dictionary of all pages using a specific template.</returns>
        public Dictionary<string, Dictionary<string, string>> GetPagesUsingTemplate(string templateName) =>
            Pages.Where(p => GetPropertyValue(p.Key, "use") == templateName).ToDictionary(k => k.Key, v => v.Value);

        public Dictionary<string, string> GetPageParts(string pagename) => Pages.ContainsKey(pagename)? Pages[pagename] : null;

        private void RecursiveCopyStaticFiles(string directoryPath)
        {
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                try
                {
                    var shouldMin = ShouldMinPage("static");
                    if (shouldMin && file.EndsWith(".js"))
                    {
                        var contents = File.ReadAllText(file);
                        contents = Uglify.Js(contents).ToString();
                        File.WriteAllText(MakeRelativeToOutputFromStaticDirectory(file), contents);
                    }
                    else if (shouldMin && file.EndsWith(".css"))
                    {
                        var contents = File.ReadAllText(file);
                        contents = Uglify.Css(contents).ToString();
                        File.WriteAllText(MakeRelativeToOutputFromStaticDirectory(file), contents);
                    }
                    else if (shouldMin && file.EndsWith(".html"))
                    {
                        var contents = File.ReadAllText(file);
                        contents = Uglify.Html(contents).ToString();
                        File.WriteAllText(MakeRelativeToOutputFromStaticDirectory(file), contents);
                    }
                    else
                        File.Copy(file, MakeRelativeToOutputFromStaticDirectory(file), true);
                }
                catch (Exception ex)
                {
                    _warnings.Add("Unable to copy static file " + file + "\r\n" + ex.Message);
#if DEBUG
                    throw;
#endif
                }
            }

            foreach (var directory in Directory.GetDirectories(directoryPath))
            {
                if (!Directory.Exists(MakeRelativeToOutputFromStaticDirectory(directory)))
                    Directory.CreateDirectory(MakeRelativeToOutputFromStaticDirectory(directory));
                RecursiveCopyStaticFiles(directory);
            }
        }

        private string MakeRelativeToOutputFromStaticDirectory(string p)
            => p.Replace(WorkingDirectory + "static" + Path.DirectorySeparatorChar, OutputDirectory);

        public List<string> Generate()
        {
            _warnings = new List<string>();
            // Check for incfile file

            // Parse and save (not default page)
            Directory.CreateDirectory(OutputDirectory);
            foreach (var page in Pages)
            {
                if (page.Key != "default")
                    _threadManager.RunThread(() =>
                    {
                        var key = page.Key;
                        ParsePageAndSave(key);
                    });
            }

            // Copy static files
            var dir = WorkingDirectory + "static" + Path.DirectorySeparatorChar;
            if (Directory.Exists(dir))
                _threadManager.RunThread(() =>
                {
                    RecursiveCopyStaticFiles(dir);
                });

            _threadManager.WaitForFinish();

            return _warnings;
        }
    }
}
