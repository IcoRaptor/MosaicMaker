using System;
using System.Windows.Forms;

[assembly: CLSCompliant(true)]

namespace MosaicMakerNS
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}