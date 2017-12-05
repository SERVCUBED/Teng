using System;
using System.Windows.Forms;
using ICSharpCode.AvalonEdit;

namespace gui
{
    public partial class PartEditor : Form
    {
        public TextEditor TextEditor => (elementHost1.Child as Editor)?.textEditor;

        public PartEditor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
