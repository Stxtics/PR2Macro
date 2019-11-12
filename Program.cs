using System;
using System.Windows.Forms;

namespace PR2Macro
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += (sender, eventArg) =>
            {
                MessageBox.Show(eventArg.ExceptionObject.ToString());
            };
            Application.Run(new MainForm());
        }
    }
}
