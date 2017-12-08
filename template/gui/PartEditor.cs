using System;
using System.Windows.Forms;
#if !Linux
using ICSharpCode.AvalonEdit;
#endif

namespace gui
{
    public partial class PartEditor : Form
    {

#if Linux
        public TextBox TextEditor => elementHost1; // as TextBox
#else
        public TextEditor TextEditor => (elementHost1.Child as Editor)?.textEditor;
#endif

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
