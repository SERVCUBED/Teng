using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ICSharpCode.AvalonEdit.Highlighting;
using TemplateEngine;

namespace gui
{
    public partial class Form1 : Form
    {
        private Engine engine;
        private string _filePath;

        public Form1(string filePath)
        {
            InitializeComponent();

            if (filePath != null && File.Exists(filePath))
                LoadFile(filePath);
        }

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

        private void LoadFile(string filePath)
        {
            ResetUI();
            _filePath = filePath;
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

                    var path = split[0].Replace('\r', '\\');
                    if (Path.IsPathRooted(path))
                        outputDirectory = path;
                    else
                        outputDirectory = workingDirectory.AddDirectorySeparatorChar() + path;
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
                    engine = new Engine(outputDirectory, workingDirectory, noMinify, fileFormats, true);

                    var verifyResult = engine.VerifyArgs();
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

            if (engine == null)
                return;
            
            templatesListBox.Items.AddRange(engine.Templates?.Keys.ToArray());
            templatesGroupBox.Enabled = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                LoadFile(openFileDialog1.FileName);
        }

        private string StripNewlineChars(string s) => s.Replace("\r", "").Replace("\n", "").Trim();

        private void Log(string message)
        {
            logTextBox.Text += message + Environment.NewLine;
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
            pagesListBox.Items.AddRange(engine.GetPagesUsingTemplate(templatesListBox.SelectedItem.ToString()).Keys.ToArray());
            pagesGroupBox.Enabled = true;

            pagePartsListBox.Items.Clear();
            pagePartsGroupBox.Enabled = false;
        }

        private void pagesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pagesListBox.SelectedIndex == -1)
                return;

            pagePartsListBox.Items.Clear();
            pagePartsListBox.Items.AddRange(engine.GetPageParts(pagesListBox.SelectedItem.ToString()).Keys.ToArray());
            pagePartsGroupBox.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button10.Enabled = false;
            Log("Generating");
            var r = engine?.Generate();

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
            var r = engine.SaveAllFiles();
            foreach (var item in r)
                Log(item);
            Log("Saved all files");
        }

        private void pageGenerateBtn_Click(object sender, EventArgs e)
        {
            var pn = pagesListBox.SelectedItem.ToString();
            Log("Generating page: " + pn);
            engine.ParsePageAndSave(pn);
            Log("Done");
        }

        private void templateAddBtn_Click(object sender, EventArgs e)
        {
            var name = InputBox.Prompt("Enter name (alpanumeric only, no spaces):");
            if (name == String.Empty)
                return;
            name = name.Trim();
            name = Regex.Match(name.ToLower(), "[a-z0-9]+").Value;

            if (engine.Templates.ContainsKey(name))
            {
                MessageBox.Show("Name already exists.");
                return;
            }

            templatesListBox.Items.Add(name);
            EditTemplate(name);
        }

        private void templatesRemBtn_Click(object sender, EventArgs e)
        {
            if (!ShowConfirmation())
                return;
            if (templatesListBox.SelectedIndex < 0)
                return;
            var item = templatesListBox.SelectedItem.ToString();
            templatesListBox.Items.Remove(item);
            engine.RemoveTemplateAndPages(item);

            pagePartsListBox.Items.Clear();
            pagePartsGroupBox.Enabled = false;
            pagesListBox.Items.Clear();
            pagesGroupBox.Enabled = false;
        }

        private void templatesEditBtn_Click(object sender, EventArgs e)
        {
            if (templatesListBox.SelectedIndex > -1)
                EditTemplate(templatesListBox.SelectedItem.ToString());
        }

        private void EditTemplate(string name)
        {
            if (!engine.Templates.ContainsKey(name))
                engine.Templates.Add(name, "");

            var f = new TemplateFrm
            {
                Editor = {Text = engine.Templates[name]},
                Format = {Text = engine.FileFormatsDictionary.ContainsKey(name)? engine.FileFormatsDictionary[name] : "default"},
                MinifyChk = {Checked = !engine.NoMinList.Contains(name)},
                Text = @"Editing template: " + name
            };

            if (f.ShowDialog() != DialogResult.OK) return;

            engine.Templates[name] = f.Editor.Text;
            if (engine.FileFormatsDictionary.ContainsKey(name))
            {
                if (f.Format.Text == "default")
                    engine.FileFormatsDictionary.Remove(name);
                else
                    engine.FileFormatsDictionary[name] = f.Format.Text;
            }
            else if (f.Format.Text != "default")
                engine.FileFormatsDictionary.Add(name, f.Format.Text);

            if (engine.NoMinList.Contains(name) && f.MinifyChk.Checked)
                engine.NoMinList.Remove(name);
            else if (!f.MinifyChk.Checked)
                engine.NoMinList.Add(name);
        }

        private bool ShowConfirmation()
            => DialogResult.Yes == MessageBox.Show(@"Are you sure?", @"Confirmation required", MessageBoxButtons.YesNo);

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            engine?.SaveAllFiles();
        }

        private void pagesAddBtn_Click(object sender, EventArgs e)
        {
            var name = InputBox.Prompt("Enter name (alpanumeric only, no spaces):");
            if (name == String.Empty)
                return;
            name = name.Trim();
            name = Regex.Match(name.ToLower(), "[a-z0-9]+").Value;

            if (engine.Pages.ContainsKey(name))
            {
                MessageBox.Show("Name already exists.");
                return;
            }

            pagesListBox.Items.Add(name);
            engine.Pages.Add(name, new Dictionary<string, string>());
            engine.Pages[name].Add("use", templatesListBox.SelectedItem.ToString());
            //EditPage(name);
        }

        private void pagesRemBtn_Click(object sender, EventArgs e)
        {
            if (!ShowConfirmation())
                return;
            if (pagesListBox.SelectedIndex < 0)
                return;
            var item = pagesListBox.SelectedItem.ToString();
            pagesListBox.Items.Remove(item);
            engine.RemovePage(item);

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

            if (engine.Pages[selectedPage].ContainsKey(name))
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
            engine.RemovePagePart(pagesListBox.SelectedItem.ToString(), item);
        }

        private void EditPagePart(string page, string part)
        {
            if (!engine.Pages.ContainsKey(page))
                return;
            if (!engine.Pages[page].ContainsKey(part))
                engine.Pages[page].Add(part, String.Empty);

            var f = new PartEditor();
            f.TextEditor.Text = engine.Pages[page][part];
            f.Text = $"Editing page part: {page}.{part}";
            if (part.EndsWith("md"))
                f.TextEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("MarkDown");
            if (f.ShowDialog() == DialogResult.OK)
                engine.Pages[page][part] = f.TextEditor.Text;
        }

        private void pagePartsEditBtn_Click(object sender, EventArgs e)
        {
            if (pagePartsListBox.SelectedIndex > -1)
                EditPagePart(pagesListBox.SelectedItem.ToString(), pagePartsListBox.SelectedItem.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            engine?.CleanOutput();
        }
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
