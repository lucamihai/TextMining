using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace TextMining.GUI
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
