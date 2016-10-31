﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gui
{
    public partial class Form1 : Form
    {
        private Process _p;

        public Form1()
        {
            InitializeComponent();

            var currentDir = Directory.GetCurrentDirectory();
            workingDirectoryTxt.Text = currentDir;
            outputDirectoryTxt.Text = currentDir + Path.DirectorySeparatorChar + "output";

            var guessedExePath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "templategen.exe";
            if (File.Exists(guessedExePath))
            {
                exeDirectoryTxt.Text = guessedExePath;
                openFileDialog1.FileName = guessedExePath;
            }

            //var filePath = currentDir + Path.DirectorySeparatorChar + ".project";
            //if (File.Exists(filePath))
            //{
            //    var contents = File.ReadAllText(filePath);
            //    var split = contents.Split('\n');
            //    if (split.Length >= 8)
            //    {
            //        outputDirectoryTxt.Text = workingDirectoryTxt + split[0];
            //        generateChk.Checked = split[1] == "True";
            //        cleanOutputChk.Checked = split[2] == "True";
            //    }
            //}

        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = workingDirectoryTxt.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                workingDirectoryTxt.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = outputDirectoryTxt.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                outputDirectoryTxt.Text = folderBrowserDialog1.SelectedPath;
        }

        private void LogText(string text)
        {
            outputTxt.Text += text + Environment.NewLine;
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            if (!File.Exists(exeDirectoryTxt.Text))
            {
                LogText("Path to templategen.exe must be valid");
                return;
            }

            if (!Directory.Exists(workingDirectoryTxt.Text))
            {
                LogText("Working directory path must exist");
                return;
            }

            if (fileFormatChk.Checked && !fileFormatTxt.Text.Contains("%n"))
            {
                LogText("File format must contain %n (where the pagename goes)");
                return;
            }

            workingDirectoryTxt.Text = workingDirectoryTxt.Text.AddDirectorySeparatorChar();
            outputDirectoryTxt.Text = outputDirectoryTxt.Text.AddDirectorySeparatorChar();

            outputTxt.Clear();
            Run();
        }

        private void Run()
        {
            var args = "";
            if (generateChk.Checked)
                args += "-generate ";
            if (cleanOutputChk.Checked)
                args += "-clean ";
            if (!minifyOutputChk.Checked)
                args += "-nominify ";
            args += $"\"-dir={workingDirectoryTxt.Text}\\\" \"-output={outputDirectoryTxt.Text}\\\" ";
            if (fileFormatChk.Checked)
                args += $"\"-format={fileFormatTxt.Text}\"";

            _p = new Process
            {
                StartInfo =
                {
                    FileName = exeDirectoryTxt.Text,
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                },
                EnableRaisingEvents = true
            };


            _p.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            _p.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
            _p.Exited += POnExited;

            _p.Start();
            runBtn.Enabled = false;

            _p.BeginOutputReadLine();
            _p.BeginErrorReadLine();
        }

        private void POnExited(object sender, EventArgs eventArgs)
        {
            runBtn.Invoke(new Action(() => runBtn.Enabled = true));
        }

        private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                outputTxt.Invoke(new Action(() => outputTxt.Text += outLine.Data + Environment.NewLine));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                exeDirectoryTxt.Text = openFileDialog1.FileName;
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
