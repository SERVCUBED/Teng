using System;
using System.Globalization;
using System.Windows.Forms;

namespace gui
{
    public partial class OrderNumberEditor : Form
    {
        private OrderNumberEditor()
        {
            InitializeComponent();
        }

        public static string Show(string oldValue)
        {
            int.TryParse(oldValue, out var i);
            var f = new OrderNumberEditor {numericUpDown1 = {Value = i}};
            return f.ShowDialog() == DialogResult.OK ? f.numericUpDown1.Value.ToString(CultureInfo.InvariantCulture) : oldValue;
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
