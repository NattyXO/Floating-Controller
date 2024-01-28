using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Floating_Controller
{
    
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public void ToastShow(string type, string message)
        {
            TaostNotification toast = new TaostNotification(type, message);
            toast.Show();
        }

        private void picScreenShot_Click(object sender, EventArgs e)
        {
            // Call picExpandClose_Click with appropriate parameters
            Program.MainForm.picExpandClose_Click(sender, e);

            // Minimize Form1
            Program.MainForm.WindowState = FormWindowState.Minimized;

            // Capture the entire screen
            Bitmap screenshot = CaptureScreen();

            // Save the screenshot to the Pictures folder
            SaveScreenshot(screenshot);
            ToastShow("SUCCESS", "Save Successful.");

            // Restore Form1 to normal state
            Program.MainForm.WindowState = FormWindowState.Normal;
            Program.MainForm.picExpand_Click(sender, e);

        }
        private Bitmap CaptureScreen()
        {
            // Create a bitmap to store the screenshot
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            // Create a graphics object from the bitmap
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Capture the entire screen and draw it onto the bitmap
                g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            }

            return bitmap;
        }

        private void SaveScreenshot(Bitmap screenshot)
        {
            // Determine the path to the Pictures folder
            string picturesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Screenshots");

            // Create the Pictures\Screenshots folder if it doesn't exist
            if (!Directory.Exists(picturesFolder))
            {
                Directory.CreateDirectory(picturesFolder);
            }

            // Generate a unique filename for the screenshot (you can customize this)
            string fileName = $"Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";

            // Combine the path to the Pictures folder with the filename
            string filePath = Path.Combine(picturesFolder, fileName);

            // Save the screenshot to the specified file path
            screenshot.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

            // Optionally, you can display a message to the user or open the folder containing the screenshot
            ToastShow("SUCCESS", "Save Successful.");
        }

        private void Menu_KeyDown(object sender, KeyEventArgs e)
        {
           
            
        }

        private void bunifuGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
