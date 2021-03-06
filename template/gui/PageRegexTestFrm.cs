﻿using System;
using System.Windows.Forms;
using TemplateEngine;

namespace gui
{
    public partial class PageRegexTestFrm : Form
    {
        private readonly Engine _engine;

        public PageRegexTestFrm(Engine engine)
        {
            _engine = engine;
            InitializeComponent();
            listBox1.Items.AddRange(_engine.GetPageNamesMatchRegex(textBox1.Text, false).ToArray());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            
            listBox1.Items.AddRange(_engine.GetPageNamesMatchRegex(textBox1.Text, false).ToArray());
        }
    }
}
