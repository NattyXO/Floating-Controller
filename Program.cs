using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Floating_Controller
{
    static class Program
    {
        public static frmFloat MainForm { get; private set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create an instance of Form1
            MainForm = new frmFloat();

            // Run the application
            Application.Run(MainForm);
        }
    }
}
