namespace gui
{
    partial class MainFrm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openStaticDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openOutputDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testPageRegexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToDiskNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.templatesGroupBox = new System.Windows.Forms.GroupBox();
            this.templatesEditBtn = new System.Windows.Forms.Button();
            this.templatesRemBtn = new System.Windows.Forms.Button();
            this.templateAddBtn = new System.Windows.Forms.Button();
            this.templatesListBox = new System.Windows.Forms.ListBox();
            this.pagesGroupBox = new System.Windows.Forms.GroupBox();
            this.pageGenerateBtn = new System.Windows.Forms.Button();
            this.pagesRemBtn = new System.Windows.Forms.Button();
            this.pagesAddBtn = new System.Windows.Forms.Button();
            this.pagesListBox = new System.Windows.Forms.ListBox();
            this.pagePartsGroupBox = new System.Windows.Forms.GroupBox();
            this.pagePartsEditBtn = new System.Windows.Forms.Button();
            this.pagePartsListBox = new System.Windows.Forms.ListBox();
            this.pagePartsRemBtn = new System.Windows.Forms.Button();
            this.pagePartsAddBtn = new System.Windows.Forms.Button();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.archiveSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.templatesGroupBox.SuspendLayout();
            this.pagesGroupBox.SuspendLayout();
            this.pagePartsGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.projectToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(634, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Enabled = false;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Enabled = false;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem,
            this.projectSettingsToolStripMenuItem,
            this.openStaticDirectoryToolStripMenuItem,
            this.openOutputDirectoryToolStripMenuItem,
            this.archiveToolStripMenuItem,
            this.testPageRegexToolStripMenuItem,
            this.saveToDiskNowToolStripMenuItem});
            this.projectToolStripMenuItem.Enabled = false;
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.reloadToolStripMenuItem.Text = "&Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // projectSettingsToolStripMenuItem
            // 
            this.projectSettingsToolStripMenuItem.Enabled = false;
            this.projectSettingsToolStripMenuItem.Name = "projectSettingsToolStripMenuItem";
            this.projectSettingsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.projectSettingsToolStripMenuItem.Text = "&Project settings";
            // 
            // openStaticDirectoryToolStripMenuItem
            // 
            this.openStaticDirectoryToolStripMenuItem.Name = "openStaticDirectoryToolStripMenuItem";
            this.openStaticDirectoryToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.openStaticDirectoryToolStripMenuItem.Text = "Open &static directory";
            this.openStaticDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openStaticDirectoryToolStripMenuItem_Click);
            // 
            // openOutputDirectoryToolStripMenuItem
            // 
            this.openOutputDirectoryToolStripMenuItem.Name = "openOutputDirectoryToolStripMenuItem";
            this.openOutputDirectoryToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.openOutputDirectoryToolStripMenuItem.Text = "Open &output directory";
            this.openOutputDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openOutputDirectoryToolStripMenuItem_Click);
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.archiveToolStripMenuItem.Text = "&Archive";
            this.archiveToolStripMenuItem.Click += new System.EventHandler(this.archiveToolStripMenuItem_Click);
            // 
            // testPageRegexToolStripMenuItem
            // 
            this.testPageRegexToolStripMenuItem.Name = "testPageRegexToolStripMenuItem";
            this.testPageRegexToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.testPageRegexToolStripMenuItem.Text = "&Test page Regex";
            this.testPageRegexToolStripMenuItem.Click += new System.EventHandler(this.testPageRegexToolStripMenuItem_Click);
            // 
            // saveToDiskNowToolStripMenuItem
            // 
            this.saveToDiskNowToolStripMenuItem.Name = "saveToDiskNowToolStripMenuItem";
            this.saveToDiskNowToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.saveToDiskNowToolStripMenuItem.Text = "&Save to disk now";
            this.saveToDiskNowToolStripMenuItem.Click += new System.EventHandler(this.saveToDiskNowToolStripMenuItem_Click);
            // 
            // templatesGroupBox
            // 
            this.templatesGroupBox.Controls.Add(this.templatesEditBtn);
            this.templatesGroupBox.Controls.Add(this.templatesRemBtn);
            this.templatesGroupBox.Controls.Add(this.templateAddBtn);
            this.templatesGroupBox.Controls.Add(this.templatesListBox);
            this.templatesGroupBox.Enabled = false;
            this.templatesGroupBox.Location = new System.Drawing.Point(13, 28);
            this.templatesGroupBox.Name = "templatesGroupBox";
            this.templatesGroupBox.Size = new System.Drawing.Size(200, 306);
            this.templatesGroupBox.TabIndex = 1;
            this.templatesGroupBox.TabStop = false;
            this.templatesGroupBox.Text = "Templates";
            // 
            // templatesEditBtn
            // 
            this.templatesEditBtn.Location = new System.Drawing.Point(82, 264);
            this.templatesEditBtn.Name = "templatesEditBtn";
            this.templatesEditBtn.Size = new System.Drawing.Size(70, 23);
            this.templatesEditBtn.TabIndex = 3;
            this.templatesEditBtn.Text = "Edit";
            this.templatesEditBtn.UseVisualStyleBackColor = true;
            this.templatesEditBtn.Click += new System.EventHandler(this.templatesEditBtn_Click);
            // 
            // templatesRemBtn
            // 
            this.templatesRemBtn.Location = new System.Drawing.Point(44, 264);
            this.templatesRemBtn.Name = "templatesRemBtn";
            this.templatesRemBtn.Size = new System.Drawing.Size(32, 23);
            this.templatesRemBtn.TabIndex = 2;
            this.templatesRemBtn.Text = "-";
            this.templatesRemBtn.UseVisualStyleBackColor = true;
            this.templatesRemBtn.Click += new System.EventHandler(this.templatesRemBtn_Click);
            // 
            // templateAddBtn
            // 
            this.templateAddBtn.Location = new System.Drawing.Point(6, 264);
            this.templateAddBtn.Name = "templateAddBtn";
            this.templateAddBtn.Size = new System.Drawing.Size(32, 23);
            this.templateAddBtn.TabIndex = 1;
            this.templateAddBtn.Text = "+";
            this.templateAddBtn.UseVisualStyleBackColor = true;
            this.templateAddBtn.Click += new System.EventHandler(this.templateAddBtn_Click);
            // 
            // templatesListBox
            // 
            this.templatesListBox.FormattingEnabled = true;
            this.templatesListBox.Location = new System.Drawing.Point(7, 20);
            this.templatesListBox.Name = "templatesListBox";
            this.templatesListBox.Size = new System.Drawing.Size(187, 238);
            this.templatesListBox.TabIndex = 0;
            this.templatesListBox.SelectedIndexChanged += new System.EventHandler(this.templatesListBox_SelectedIndexChanged);
            // 
            // pagesGroupBox
            // 
            this.pagesGroupBox.Controls.Add(this.pageGenerateBtn);
            this.pagesGroupBox.Controls.Add(this.pagesRemBtn);
            this.pagesGroupBox.Controls.Add(this.pagesAddBtn);
            this.pagesGroupBox.Controls.Add(this.pagesListBox);
            this.pagesGroupBox.Enabled = false;
            this.pagesGroupBox.Location = new System.Drawing.Point(219, 28);
            this.pagesGroupBox.Name = "pagesGroupBox";
            this.pagesGroupBox.Size = new System.Drawing.Size(200, 306);
            this.pagesGroupBox.TabIndex = 2;
            this.pagesGroupBox.TabStop = false;
            this.pagesGroupBox.Text = "Pages";
            // 
            // pageGenerateBtn
            // 
            this.pageGenerateBtn.Location = new System.Drawing.Point(82, 263);
            this.pageGenerateBtn.Name = "pageGenerateBtn";
            this.pageGenerateBtn.Size = new System.Drawing.Size(70, 23);
            this.pageGenerateBtn.TabIndex = 6;
            this.pageGenerateBtn.Text = "Generate";
            this.pageGenerateBtn.UseVisualStyleBackColor = true;
            this.pageGenerateBtn.Click += new System.EventHandler(this.pageGenerateBtn_Click);
            // 
            // pagesRemBtn
            // 
            this.pagesRemBtn.Location = new System.Drawing.Point(44, 263);
            this.pagesRemBtn.Name = "pagesRemBtn";
            this.pagesRemBtn.Size = new System.Drawing.Size(32, 23);
            this.pagesRemBtn.TabIndex = 5;
            this.pagesRemBtn.Text = "-";
            this.pagesRemBtn.UseVisualStyleBackColor = true;
            this.pagesRemBtn.Click += new System.EventHandler(this.pagesRemBtn_Click);
            // 
            // pagesAddBtn
            // 
            this.pagesAddBtn.Location = new System.Drawing.Point(6, 263);
            this.pagesAddBtn.Name = "pagesAddBtn";
            this.pagesAddBtn.Size = new System.Drawing.Size(32, 23);
            this.pagesAddBtn.TabIndex = 4;
            this.pagesAddBtn.Text = "+";
            this.pagesAddBtn.UseVisualStyleBackColor = true;
            this.pagesAddBtn.Click += new System.EventHandler(this.pagesAddBtn_Click);
            // 
            // pagesListBox
            // 
            this.pagesListBox.FormattingEnabled = true;
            this.pagesListBox.Location = new System.Drawing.Point(6, 19);
            this.pagesListBox.Name = "pagesListBox";
            this.pagesListBox.Size = new System.Drawing.Size(187, 238);
            this.pagesListBox.TabIndex = 1;
            this.pagesListBox.SelectedIndexChanged += new System.EventHandler(this.pagesListBox_SelectedIndexChanged);
            // 
            // pagePartsGroupBox
            // 
            this.pagePartsGroupBox.Controls.Add(this.pagePartsEditBtn);
            this.pagePartsGroupBox.Controls.Add(this.pagePartsListBox);
            this.pagePartsGroupBox.Controls.Add(this.pagePartsRemBtn);
            this.pagePartsGroupBox.Controls.Add(this.pagePartsAddBtn);
            this.pagePartsGroupBox.Enabled = false;
            this.pagePartsGroupBox.Location = new System.Drawing.Point(425, 28);
            this.pagePartsGroupBox.Name = "pagePartsGroupBox";
            this.pagePartsGroupBox.Size = new System.Drawing.Size(200, 306);
            this.pagePartsGroupBox.TabIndex = 3;
            this.pagePartsGroupBox.TabStop = false;
            this.pagePartsGroupBox.Text = "Page Parts";
            // 
            // pagePartsEditBtn
            // 
            this.pagePartsEditBtn.Location = new System.Drawing.Point(83, 263);
            this.pagePartsEditBtn.Name = "pagePartsEditBtn";
            this.pagePartsEditBtn.Size = new System.Drawing.Size(70, 23);
            this.pagePartsEditBtn.TabIndex = 9;
            this.pagePartsEditBtn.Text = "Edit";
            this.pagePartsEditBtn.UseVisualStyleBackColor = true;
            this.pagePartsEditBtn.Click += new System.EventHandler(this.pagePartsEditBtn_Click);
            // 
            // pagePartsListBox
            // 
            this.pagePartsListBox.FormattingEnabled = true;
            this.pagePartsListBox.Location = new System.Drawing.Point(7, 19);
            this.pagePartsListBox.Name = "pagePartsListBox";
            this.pagePartsListBox.Size = new System.Drawing.Size(187, 238);
            this.pagePartsListBox.TabIndex = 1;
            // 
            // pagePartsRemBtn
            // 
            this.pagePartsRemBtn.Location = new System.Drawing.Point(45, 263);
            this.pagePartsRemBtn.Name = "pagePartsRemBtn";
            this.pagePartsRemBtn.Size = new System.Drawing.Size(32, 23);
            this.pagePartsRemBtn.TabIndex = 8;
            this.pagePartsRemBtn.Text = "-";
            this.pagePartsRemBtn.UseVisualStyleBackColor = true;
            this.pagePartsRemBtn.Click += new System.EventHandler(this.pagePartsRemBtn_Click);
            // 
            // pagePartsAddBtn
            // 
            this.pagePartsAddBtn.Location = new System.Drawing.Point(7, 263);
            this.pagePartsAddBtn.Name = "pagePartsAddBtn";
            this.pagePartsAddBtn.Size = new System.Drawing.Size(32, 23);
            this.pagePartsAddBtn.TabIndex = 7;
            this.pagePartsAddBtn.Text = "+";
            this.pagePartsAddBtn.UseVisualStyleBackColor = true;
            this.pagePartsAddBtn.Click += new System.EventHandler(this.pagePartsAddBtn_Click);
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.logTextBox.Location = new System.Drawing.Point(13, 340);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTextBox.Size = new System.Drawing.Size(406, 100);
            this.logTextBox.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button10);
            this.groupBox1.Location = new System.Drawing.Point(425, 340);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Clean output";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(7, 19);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(128, 23);
            this.button10.TabIndex = 0;
            this.button10.Text = "Generate all pages";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "project.teng";
            this.openFileDialog1.Filter = "Teng project files (project.teng)|project.teng|All files (*.*)|*.*";
            // 
            // archiveSaveFileDialog
            // 
            this.archiveSaveFileDialog.Filter = "Zip archives (*.zip)|*.zip|All files (*.*)|*.*";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 445);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.pagePartsGroupBox);
            this.Controls.Add(this.pagesGroupBox);
            this.Controls.Add(this.templatesGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainFrm";
            this.Text = "TengDK";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.templatesGroupBox.ResumeLayout(false);
            this.pagesGroupBox.ResumeLayout(false);
            this.pagePartsGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openStaticDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openOutputDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testPageRegexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToDiskNowToolStripMenuItem;
        private System.Windows.Forms.GroupBox templatesGroupBox;
        private System.Windows.Forms.Button templatesEditBtn;
        private System.Windows.Forms.Button templatesRemBtn;
        private System.Windows.Forms.Button templateAddBtn;
        private System.Windows.Forms.ListBox templatesListBox;
        private System.Windows.Forms.GroupBox pagesGroupBox;
        private System.Windows.Forms.Button pageGenerateBtn;
        private System.Windows.Forms.Button pagesRemBtn;
        private System.Windows.Forms.Button pagesAddBtn;
        private System.Windows.Forms.ListBox pagesListBox;
        private System.Windows.Forms.GroupBox pagePartsGroupBox;
        private System.Windows.Forms.Button pagePartsEditBtn;
        private System.Windows.Forms.ListBox pagePartsListBox;
        private System.Windows.Forms.Button pagePartsRemBtn;
        private System.Windows.Forms.Button pagePartsAddBtn;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog archiveSaveFileDialog;
    }
}

