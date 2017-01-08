using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.AvalonEdit;

namespace gui
{
    public partial class TemplateFrm : Form
    {

        public TextEditor Editor => (elementHost1.Child as Editor)?.textEditor;
        public TemplateFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Format.Text = @"default";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Format.Text != @"default" && !Format.Text.Contains("%n"))
            {
                MessageBox.Show(@"File format must contain %n. This is to be replaced by the page name.");
                return;
            }
            DialogResult = DialogResult.OK;
        }
    }
}
