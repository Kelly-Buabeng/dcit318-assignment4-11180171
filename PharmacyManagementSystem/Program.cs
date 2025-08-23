using System;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Startup error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.Error.WriteLine(ex.ToString());
            }
        }
    }
}