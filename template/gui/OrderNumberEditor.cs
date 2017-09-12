using System;
using System.Windows.Forms;

namespace gui
{
    public partial class OrderNumberEditor : Form
    {
        public OrderNumberEditor()
        {
            InitializeComponent();
        }

        public static string Show(string oldValue)
        {
            int i;
            int.TryParse(oldValue, out i);
            var f = new OrderNumberEditor {numericUpDown1 = {Value = i}};
            return f.ShowDialog() == DialogResult.OK ? f.numericUpDown1.Value.ToString() : oldValue;
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
