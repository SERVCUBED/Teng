using System;
using System.Windows.Forms;

namespace gui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var filepath = args.Length == 0 ? null : String.Join(" ", args);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFrm(filepath));
        }
    }
}
