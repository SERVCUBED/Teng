namespace gui
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.workingDirectoryTxt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.outputDirectoryTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.exeDirectoryTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.generateChk = new System.Windows.Forms.CheckBox();
            this.cleanOutputChk = new System.Windows.Forms.CheckBox();
            this.fileFormatChk = new System.Windows.Forms.CheckBox();
            this.fileFormatTxt = new System.Windows.Forms.TextBox();
            this.minifyOutputChk = new System.Windows.Forms.CheckBox();
            this.outputTxt = new System.Windows.Forms.TextBox();
            this.runBtn = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File DIrectory (working directory):";
            // 
            // workingDirectoryTxt
            // 
            this.workingDirectoryTxt.Location = new System.Drawing.Point(16, 30);
            this.workingDirectoryTxt.Name = "workingDirectoryTxt";
            this.workingDirectoryTxt.Size = new System.Drawing.Size(343, 20);
            this.workingDirectoryTxt.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(365, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Select...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(365, 68);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Select...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // outputDirectoryTxt
            // 
            this.outputDirectoryTxt.Location = new System.Drawing.Point(16, 70);
            this.outputDirectoryTxt.Name = "outputDirectoryTxt";
            this.outputDirectoryTxt.Size = new System.Drawing.Size(343, 20);
            this.outputDirectoryTxt.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output DIrectory:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(365, 108);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Select...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // exeDirectoryTxt
            // 
            this.exeDirectoryTxt.Location = new System.Drawing.Point(16, 110);
            this.exeDirectoryTxt.Name = "exeDirectoryTxt";
            this.exeDirectoryTxt.Size = new System.Drawing.Size(343, 20);
            this.exeDirectoryTxt.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Templategen.exe Location:";
            // 
            // generateChk
            // 
            this.generateChk.AutoSize = true;
            this.generateChk.Checked = true;
            this.generateChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.generateChk.Location = new System.Drawing.Point(16, 137);
            this.generateChk.Name = "generateChk";
            this.generateChk.Size = new System.Drawing.Size(70, 17);
            this.generateChk.TabIndex = 7;
            this.generateChk.Text = "Generate";
            this.generateChk.UseVisualStyleBackColor = true;
            // 
            // cleanOutputChk
            // 
            this.cleanOutputChk.AutoSize = true;
            this.cleanOutputChk.Checked = true;
            this.cleanOutputChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cleanOutputChk.Location = new System.Drawing.Point(16, 161);
            this.cleanOutputChk.Name = "cleanOutputChk";
            this.cleanOutputChk.Size = new System.Drawing.Size(86, 17);
            this.cleanOutputChk.TabIndex = 8;
            this.cleanOutputChk.Text = "Clean output";
            this.cleanOutputChk.UseVisualStyleBackColor = true;
            // 
            // fileFormatChk
            // 
            this.fileFormatChk.AutoSize = true;
            this.fileFormatChk.Location = new System.Drawing.Point(16, 207);
            this.fileFormatChk.Name = "fileFormatChk";
            this.fileFormatChk.Size = new System.Drawing.Size(185, 17);
            this.fileFormatChk.TabIndex = 10;
            this.fileFormatChk.Text = "Use non-default output file format:";
            this.fileFormatChk.UseVisualStyleBackColor = true;
            // 
            // fileFormatTxt
            // 
            this.fileFormatTxt.Location = new System.Drawing.Point(207, 204);
            this.fileFormatTxt.Name = "fileFormatTxt";
            this.fileFormatTxt.Size = new System.Drawing.Size(100, 20);
            this.fileFormatTxt.TabIndex = 11;
            this.fileFormatTxt.Text = "%n.html";
            // 
            // minifyOutputChk
            // 
            this.minifyOutputChk.AutoSize = true;
            this.minifyOutputChk.Checked = true;
            this.minifyOutputChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.minifyOutputChk.Location = new System.Drawing.Point(16, 184);
            this.minifyOutputChk.Name = "minifyOutputChk";
            this.minifyOutputChk.Size = new System.Drawing.Size(183, 17);
            this.minifyOutputChk.TabIndex = 9;
            this.minifyOutputChk.Text = "Minify all JS, CSS and HTML files";
            this.minifyOutputChk.UseVisualStyleBackColor = true;
            // 
            // outputTxt
            // 
            this.outputTxt.BackColor = System.Drawing.Color.Black;
            this.outputTxt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputTxt.ForeColor = System.Drawing.Color.White;
            this.outputTxt.Location = new System.Drawing.Point(16, 230);
            this.outputTxt.Multiline = true;
            this.outputTxt.Name = "outputTxt";
            this.outputTxt.ReadOnly = true;
            this.outputTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputTxt.Size = new System.Drawing.Size(424, 154);
            this.outputTxt.TabIndex = 13;
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(365, 203);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(75, 23);
            this.runBtn.TabIndex = 12;
            this.runBtn.Text = "Run";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "templategen.exe";
            this.openFileDialog1.Filter = "templategen.exe|templategen.exe";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 393);
            this.Controls.Add(this.runBtn);
            this.Controls.Add(this.outputTxt);
            this.Controls.Add(this.fileFormatTxt);
            this.Controls.Add(this.fileFormatChk);
            this.Controls.Add(this.cleanOutputChk);
            this.Controls.Add(this.generateChk);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.exeDirectoryTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.outputDirectoryTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.workingDirectoryTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.minifyOutputChk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Template Engine GUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox workingDirectoryTxt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox outputDirectoryTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox exeDirectoryTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox generateChk;
        private System.Windows.Forms.CheckBox cleanOutputChk;
        private System.Windows.Forms.CheckBox fileFormatChk;
        private System.Windows.Forms.TextBox fileFormatTxt;
        private System.Windows.Forms.CheckBox minifyOutputChk;
        private System.Windows.Forms.TextBox outputTxt;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

