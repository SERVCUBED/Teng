using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using NUglify;

namespace templategen
{
    internal partial class TemplateEngine
    {
        private readonly string _outputDirectory;
        private readonly string _workingDirectory;
        private readonly Dictionary<string, string> _fileFormats;
        private readonly Dictionary<string, bool> _shouldMin;
        private readonly Dictionary<string, string> _templates = new Dictionary<string, string>(); // name, value
        private int _threadCnt = 0;
        private readonly bool _useMultipleThreads;

        private readonly Dictionary<string, Dictionary<string, string>> _pages =
            new Dictionary<string, Dictionary<string, string>>(); // name -> use,title,body etc

        public TemplateEngine(string outputdirectory, string workingDirectory, Dictionary<string, bool> shouldMin1, Dictionary<string, string> fileFormats, bool useMultipleThreads)
        {
            _outputDirectory = outputdirectory;
            _workingDirectory = workingDirectory;
            _shouldMin = shouldMin1;
            _useMultipleThreads = useMultipleThreads;
            _fileFormats = fileFormats;
            if (!_fileFormats.ContainsKey("default"))
                _fileFormats.Add("default", "%n.html"); // Add default item
            if (!_shouldMin.ContainsKey("default"))
                _shouldMin.Add("default", true); // Add default item
        }

        public bool VerifyArgs()
        {
            foreach (var fileFormat in _fileFormats)
            {
                if (!fileFormat.Value.Contains("%n"))
                {
                    Console.WriteLine("File format must contain %n (see usage)");
                    return false;
                }
            }

            if (!Directory.Exists(_workingDirectory))
            {
                Console.WriteLine("Working directory must exist");
                return false;
            }

            return true;
        }

        public void CleanOutput()
        {
            if (!Directory.Exists(_outputDirectory))
                return;
            try
            {
                Clean(_outputDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to clean directory " + _outputDirectory + "\r\n" + ex.Message);
                Environment.Exit(0);
            }
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

        private void GetFiles()
        {
            foreach (var file in Directory.GetFiles(_workingDirectory + "templates"))
            {
                try
                {
                    _templates.Add(file.Substring(file.LastIndexOf(Path.DirectorySeparatorChar) + 1).ToLower(),
                        File.ReadAllText(file));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to read file: " + file + "\r\n" + ex.Message);
#if DEBUG
                    throw;
#endif
                }
            }
            foreach (var file in Directory.GetFiles(_workingDirectory + "pages"))
            {
                try
                {
                    var n = file.Substring(file.LastIndexOf(Path.DirectorySeparatorChar) + 1).Split('.');
                    if (n.Length < 2)
                        continue;

                    if (!_pages.ContainsKey(n[0].ToLower()))
                        _pages.Add(n[0].ToLower(), new Dictionary<string, string>());

                    _pages[n[0].ToLower()].Add(n[1].ToLower(), File.ReadAllText(file));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to read file: " + file + "\r\n" + ex.Message);
#if DEBUG
                    throw;
#endif
                }
            }
        }

        private string GetPropertyValue(string pageName, string propertyName)
        {
            // Make case insensitive
            pageName = pageName.ToLower();
            propertyName = propertyName.ToLower();

            if (_pages.ContainsKey(pageName) && _pages[pageName].ContainsKey(propertyName))
                return _pages[pageName][propertyName];

            if (_pages.ContainsKey("default") && _pages["default"].ContainsKey(propertyName))
                return _pages["default"][propertyName];

            Console.Write("> No value or default value for ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(pageName + "." + propertyName);
            Console.ForegroundColor = ConsoleColor.White;
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

        private string ParseMarkdownData(string templateData) => templateData == String.Empty ? 
            String.Empty : Markdig.Markdown.ToHtml(templateData);

        private bool ShouldMin(string pageName)
        {
            var templateName = GetPropertyValue(pageName, "use");

            return _shouldMin.ContainsKey(templateName) ?
                _shouldMin[templateName] : _shouldMin["default"]; // Always contains "default" value
        }

        private string FormatOutputFilename(string name)
        {
            var templateName = GetPropertyValue(name, "use");

            string format = _fileFormats.ContainsKey(templateName) ? 
                _fileFormats[templateName] : _fileFormats["default"]; // Always contains "default" value

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

            if (m.Groups[1].Value == "template" && _templates.ContainsKey(m.Groups[2].Value))
                return ParseTemplateData(_templates[m.Groups[2].Value], pageName);

            Console.Write("> Invalid property name ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(m.Groups[1].Value + "." + m.Groups[2].Value);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" in page ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(pageName);
            Console.ForegroundColor = ConsoleColor.White;
            return String.Empty;
        }

        private void ParsePageAndSave(string pageName)
        {
            var template = GetPropertyValue(pageName, "use");
            var content = ParseTemplateData(_templates[template], pageName);
            try
            {
                // Minify output
                if (ShouldMin(pageName))
                    content = Uglify.Html(content).ToString();

                File.WriteAllText(_outputDirectory + FormatOutputFilename(pageName), content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("> Unable to write page: " + pageName + "\r\n" + ex.Message);
#if DEBUG
                throw;
#endif
            }
        }

        private void RecursiveCopyStaticFiles(string directoryPath)
        {
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                try
                {
                    var shouldMin = ShouldMin("default");
                    if (shouldMin && file.EndsWith(".js"))
                    {
                        var contents = File.ReadAllText(file);
                        contents = Uglify.Js(contents).ToString();
                        File.WriteAllText(MakeRelativeToOutputFromStaticDirctory(file), contents);
                    }
                    else if (shouldMin && file.EndsWith(".css"))
                    {
                        var contents = File.ReadAllText(file);
                        contents = Uglify.Css(contents).ToString();
                        File.WriteAllText(MakeRelativeToOutputFromStaticDirctory(file), contents);
                    }
                    else if (shouldMin && file.EndsWith(".html"))
                    {
                        var contents = File.ReadAllText(file);
                        contents = Uglify.Html(contents).ToString();
                        File.WriteAllText(MakeRelativeToOutputFromStaticDirctory(file), contents);
                    }
                    else
                        File.Copy(file, MakeRelativeToOutputFromStaticDirctory(file), true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to copy static file " + file + "\r\n" + ex.Message);
#if DEBUG
                    throw;
#endif
                }
            }

            foreach (var directory in Directory.GetDirectories(directoryPath))
            {
                if (!Directory.Exists(MakeRelativeToOutputFromStaticDirctory(directory)))
                    Directory.CreateDirectory(MakeRelativeToOutputFromStaticDirctory(directory));
                RecursiveCopyStaticFiles(directory);
            }
        }

        private string MakeRelativeToOutputFromStaticDirctory(string p)
            => p.Replace(_workingDirectory + "static" + Path.DirectorySeparatorChar, _outputDirectory);

        private void StartSafeThread(Func<bool> func)
        {
            while (_threadCnt > 10) // Max 10 threads
            {
                Thread.Sleep(1);
            }

            _threadCnt++;
            if (_useMultipleThreads)
            {
                var f = func;
                var t = new Thread(() =>
                {
                    f.Invoke();
                    _threadCnt--;
                });
                t.Start();
            }
            else
            {
                func.Invoke();
                _threadCnt--;
            }
        }

        public void Generate()
        {
            // Read .project file

            // Check for incfile file

            // Load files into arrays
            GetFiles();

            // Parse and save (not default page)
            Directory.CreateDirectory(_outputDirectory);
            foreach (var page in _pages)
            {
                if (page.Key != "default")
                    StartSafeThread(() =>
                    {
                        var key = page.Key;
                        ParsePageAndSave(key);
                        return true;
                    });
            }

            // Copy static files
            var dir = _workingDirectory + "static" + Path.DirectorySeparatorChar;
            if (Directory.Exists(dir))
                StartSafeThread(() =>
                {
                    RecursiveCopyStaticFiles(dir);
                    return true;
                });

            while (_threadCnt > 0)
            {
                Thread.Sleep(10);
            }

            /*
             * *	For each page:
		     *          *	load page.use if exists (if not, default.use. If that not exists, fail)
		     *          *	Parse template file, loading page content from {{t page.asdf}} -> <pagename>.asdf -> default.asdf where exists
			 *              and {{t incfile.a}} -> all items under incfile group a
		     *          *	Minify output
		     *          *	Copy to <pagename>.format in output directory
             * *	Copy all .project 'copy' values where exists
             */
        }
    }
}
