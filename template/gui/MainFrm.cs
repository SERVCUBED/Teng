using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
#if !Linux
using ICSharpCode.AvalonEdit.Highlighting;
#endif
using TemplateEngine;

namespace gui
{
    public partial class MainFrm : Form
    {
        private Engine _engine;
        private string _filePath;

        public MainFrm(string filePath)
        {
            InitializeComponent();

            if (filePath != null && File.Exists(filePath))
                LoadFile(filePath);
        }

        #region UI

        private void ResetUI()
        {
            templatesListBox.Items.Clear();
            pagesListBox.Items.Clear();
            pagePartsListBox.Items.Clear();
            templatesGroupBox.Enabled = false;
            pagesGroupBox.Enabled = false;
            pagePartsGroupBox.Enabled = false;
            logTextBox.Clear();
        }

        private void ReloadUI()
        {
            templatesListBox.Items.Clear();
            templatesListBox.Items.Add("All Pages");
            templatesListBox.Items.AddRange(_engine.Templates?.Keys.ToArray());
            templatesGroupBox.Enabled = true;
            projectToolStripMenuItem.Enabled = true;

            pagesGroupBox.Enabled = false;
            pagePartsGroupBox.Enabled = false;
            pagesListBox.Items.Clear();
            pagePartsListBox.Items.Clear();
        }

        #endregion

        private void LoadFile(string filePath)
        {
            ResetUI();
            _filePath = filePath;

            if (filePath == null)
            {
                Log("Cannot load empty file path.");
                return;
            }

            try
            {
                string workingDirectory;
                string outputDirectory;
                //bool shouldGen = false;
                //bool shouldCleanOutput = false;
                List<string> noMinify = new List<string>();
                Dictionary<string, string> fileFormats = new Dictionary<string, string>();

                var contents = File.ReadAllText(filePath);
                var split = contents.Split('\n');
                if (split.Length >= 3)
                {
                    workingDirectory = filePath.Substring(0, filePath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                    if (workingDirectory == String.Empty)
                        workingDirectory = ".".AddDirectorySeparatorChar();

                    var path = split[0].Replace('\r', '\\');
                    if (Path.IsPathRooted(path))
                        outputDirectory = path;
                    else
                        outputDirectory = workingDirectory.AddDirectorySeparatorChar() + path.AddDirectorySeparatorChar();
                    //shouldGen = split[1].Contains("True");
                    //shouldCleanOutput = split[2].Contains("True");
                }
                else
                {
                    MessageBox.Show("Entries missing from file. Ignoring file.");
                    return;
                }

                var ffPath = workingDirectory + "fileformats";
                if (File.Exists(ffPath))
                {
                    Log("Using fileformats file");
                    try
                    {
                        var t = File.ReadAllText(ffPath);
                        var lines = t.Split('\n');
                        foreach (var line in lines)
                        {
                            var items = line.Split(':');

                            if (items.Length < 2)
                                continue;

                            for (int i = 1; i < items.Length; i++)
                            {
                                if (StripNewlineChars(items[i]).Trim() == "nomin")
                                    noMinify.Add(items[0]);
                                else
                                    fileFormats.Add(items[0], StripNewlineChars(items[i]).Trim());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log(@"Error when loading fileformats file: " + ex.Message + Environment.NewLine +
                                          ex.StackTrace);
                        return;
                    }

                    Log("Starting template engine...");
                    _engine = new Engine(outputDirectory, workingDirectory, noMinify, fileFormats, true);

                    var verifyResult = _engine.VerifyArgs();
                    if (verifyResult != null)
                    {
                        Log(verifyResult);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error when loading file: " + ex.Message + Environment.NewLine + ex.StackTrace);
                Log(@"Error when loading file: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }

            if (_engine == null)
                return;

            ReloadUI();
        }

        private static string StripNewlineChars(string s) => s.Replace("\r", "").Replace("\n", "").Trim();

        private void Log(string message)
        {
            logTextBox.Text += message + Environment.NewLine;
        }

        private static bool ShowConfirmation()
            => DialogResult.Yes == MessageBox.Show(@"Are you sure?", @"Confirmation required", MessageBoxButtons.YesNo);

        private void OpenFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Log("Directory does not exist.");
                return;
            }

            var p = new Process
            {
                StartInfo =
                {
                    FileName = "explorer.exe",
                    Arguments = path
                }
            };
            p.Start();
        }

        #region Editing Items

        private void EditPagePart(string page, string part)
        {
            if (!_engine.Pages.ContainsKey(page))
                return;
            if (!_engine.Pages[page].ContainsKey(part))
                _engine.Pages[page].Add(part, String.Empty);

            if (part == "order")
            {
                _engine.Pages[page][part] = OrderNumberEditor.Show(_engine.Pages[page][part]);
                return;
            }

            var f = new PartEditor();
            f.TextEditor.Text = _engine.Pages[page][part];
            f.Text = $@"Editing page part: {page}.{part} - TengDK";
#if !Linux
            if (part.EndsWith("md"))
                f.TextEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("MarkDown");
#endif
            if (f.ShowDialog() == DialogResult.OK)
            {
                _engine.Pages[page][part] = f.TextEditor.Text;
                if (part == "use")
                {
                    Log($"Template changed for page {page}. Reloading UI.");
                    ReloadUI();
                }
            }
        }

        private void EditTemplate(string name)
        {
            if (!_engine.Templates.ContainsKey(name))
                _engine.Templates.Add(name, "");

            var f = new TemplateFrm
            {
                Editor = { Text = _engine.Templates[name] },
                Format = { Text = _engine.FileFormatsDictionary.ContainsKey(name) ? _engine.FileFormatsDictionary[name] : "default" },
                MinifyChk = { Checked = !_engine.NoMinList.Contains(name) },
                Text = $@"Editing template: {name} - TengDK"
            };

            if (f.ShowDialog() != DialogResult.OK) return;

            _engine.Templates[name] = f.Editor.Text;
            if (_engine.FileFormatsDictionary.ContainsKey(name))
            {
                if (f.Format.Text == "default")
                    _engine.FileFormatsDictionary.Remove(name);
                else
                    _engine.FileFormatsDictionary[name] = f.Format.Text;
            }
            else if (f.Format.Text != "default")
                _engine.FileFormatsDictionary.Add(name, f.Format.Text);

            if (_engine.NoMinList.Contains(name) && f.MinifyChk.Checked)
                _engine.NoMinList.Remove(name);
            else if (!f.MinifyChk.Checked && !_engine.NoMinList.Contains(name))
                _engine.NoMinList.Add(name);
        }

#endregion

        #region Event Handlers

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                LoadFile(openFileDialog1.FileName);
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFile(_filePath);
        }

        private void templatesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (templatesListBox.SelectedIndex == -1)
                return;

            pagesListBox.Items.Clear();

            var item = templatesListBox.SelectedItem.ToString();
            if (item == "All Pages")
                pagesListBox.Items.AddRange(_engine.Pages.Keys.ToArray());
            else
                pagesListBox.Items.AddRange(_engine.GetPagesUsingTemplate(item).Keys.ToArray());

            pagesGroupBox.Enabled = true;

            pagePartsListBox.Items.Clear();
            pagePartsGroupBox.Enabled = false;
        }

        private void pagesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pagesListBox.SelectedIndex == -1)
                return;

            pagePartsListBox.Items.Clear();
            pagePartsListBox.Items.AddRange(_engine.GetPageParts(pagesListBox.SelectedItem.ToString()).Keys.ToArray());
            pagePartsGroupBox.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button10.Enabled = false;
            Log("Generating");
            var r = _engine?.Generate();

            if (r != null)
                foreach (var item in r)
                    Log(item);
            Log("Done");
            button10.Enabled = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveToDiskNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var r = _engine?.SaveAllFiles();
            if (r == null)
                return;

            foreach (var item in r)
                Log(item);
            Log("Saved all files");
        }

        private void pageGenerateBtn_Click(object sender, EventArgs e)
        {
            if (pagesListBox.SelectedIndex < 0)
                return;

            var pn = pagesListBox.SelectedItem.ToString();
            Log("Generating page: " + pn);
            _engine.ParsePageAndSave(pn);
            Log("Done");
        }

        private void templateAddBtn_Click(object sender, EventArgs e)
        {
            var name = InputBox.Prompt("Enter name (alpanumeric only, no spaces):");
            if (name == String.Empty)
                return;
            name = name.Trim();
            name = Regex.Match(name.ToLower(), "[a-z0-9]+").Value;

            if (_engine.Templates.ContainsKey(name))
            {
                MessageBox.Show("Name already exists.");
                return;
            }

            templatesListBox.Items.Add(name);
            EditTemplate(name);
        }

        private void templatesRemBtn_Click(object sender, EventArgs e)
        {
            if (templatesListBox.SelectedIndex < 0)
                return;
            var item = templatesListBox.SelectedItem.ToString();

            if (item == "All Pages")
                return;

            if (!ShowConfirmation())
                return;

            templatesListBox.Items.Remove(item);

            if (item.StartsWith("Regex: "))
                return;

            _engine.RemoveTemplateAndPages(item);

            pagePartsListBox.Items.Clear();
            pagePartsGroupBox.Enabled = false;
            pagesListBox.Items.Clear();
            pagesGroupBox.Enabled = false;
        }

        private void templatesEditBtn_Click(object sender, EventArgs e)
        {
            if (templatesListBox.SelectedIndex == -1)
                return;

            var item = templatesListBox.SelectedItem.ToString();
            if (item != "All Pages" || !item.StartsWith("Regex: "))
                EditTemplate(item);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _engine?.SaveAllFiles();
        }

        private void pagesAddBtn_Click(object sender, EventArgs e)
        {
            var templ = templatesListBox.SelectedItem.ToString();
            if (templ == "All Pages")
            {
                MessageBox.Show(@"Please select a template.");
                return;
            }

            var name = InputBox.Prompt("Enter name (alpanumeric only, no spaces):");
            if (name == String.Empty)
                return;
            name = name.Trim();
            name = Regex.Match(name.ToLower(), "[a-z0-9]+").Value;

            if (_engine.Pages.ContainsKey(name))
            {
                MessageBox.Show("Name already exists.");
                return;
            }

            pagesListBox.Items.Add(name);
            _engine.Pages.Add(name, new Dictionary<string, string>());
            _engine.Pages[name].Add("use", templ);
        }

        private void pagesRemBtn_Click(object sender, EventArgs e)
        {
            if (pagesListBox.SelectedIndex < 0)
                return;
            var item = pagesListBox.SelectedItem.ToString();

            if (item == "default")
            {
                MessageBox.Show("Default page cannot be deleted.");
                return;
            }

            if (!ShowConfirmation())
                return;
            pagesListBox.Items.Remove(item);
            _engine.RemovePage(item);

            pagePartsListBox.Items.Clear();
            pagePartsGroupBox.Enabled = false;
        }

        private void pagePartsAddBtn_Click(object sender, EventArgs e)
        {
            var name = InputBox.Prompt("Enter name (alpanumeric only, no spaces):");
            if (name == String.Empty)
                return;
            name = name.Trim();
            name = Regex.Match(name.ToLower(), "[a-z0-9]+").Value;
            var selectedPage = pagesListBox.SelectedItem.ToString();

            if (_engine.Pages[selectedPage].ContainsKey(name))
            {
                MessageBox.Show("Name already exists.");
                return;
            }

            pagePartsListBox.Items.Add(name);
            EditPagePart(selectedPage, name);
        }

        private void pagePartsRemBtn_Click(object sender, EventArgs e)
        {
            if (!ShowConfirmation())
                return;
            if (templatesListBox.SelectedIndex < 0)
                return;

            var item = pagePartsListBox.SelectedItem.ToString();
            pagePartsListBox.Items.Remove(item);
            _engine.RemovePagePart(pagesListBox.SelectedItem.ToString(), item);
        }

        private void pagePartsEditBtn_Click(object sender, EventArgs e)
        {
            if (pagePartsListBox.SelectedIndex > -1)
                EditPagePart(pagesListBox.SelectedItem.ToString(), pagePartsListBox.SelectedItem.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _engine?.CleanOutput();
        }

        private void openStaticDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFolder(_engine.WorkingDirectory + "static" + Path.DirectorySeparatorChar);
        }

        private void openOutputDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFolder(_engine.OutputDirectory);
        }

        private void testPageRegexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new PageRegexTestFrm(_engine);
            f.ShowDialog();
        }

        private void archiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (archiveSaveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            Log("Beginning archive");
            saveToDiskNowToolStripMenuItem_Click(null, null);
            try
            {
                System.IO.Compression.ZipFile.CreateFromDirectory(_engine.WorkingDirectory, archiveSaveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                Log("Error saving archive:\r\n" + ex.Message);
                return;
            }
            Log("Done");
        }

#endregion
    }

    public static class Extensions
    {
        public static string AddDirectorySeparatorChar(this string path)
        {
            if (path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                return path;
            return path + Path.DirectorySeparatorChar;
        }
    }
}
