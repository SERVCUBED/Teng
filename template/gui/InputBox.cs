using System;
using System.Windows.Forms;

namespace gui
{
    public partial class InputBox : Form
    {
        public InputBox()
        {
            InitializeComponent();
        }

        public static string Prompt(string message, string defaultContent = "")
        {
            var f = new InputBox
            {
                label1 = {Text = message},
                textBox1 = {Text = defaultContent}
            };
            if (f.ShowDialog() == DialogResult.OK)
                return f.textBox1.Text;
            return defaultContent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
