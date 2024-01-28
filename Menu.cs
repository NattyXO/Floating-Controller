using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

        private void bunifuGradientPanel1_Click(object sender, EventArgs e)
        {

        }

        private void picScreenShot_Click(object sender, EventArgs e)
        {
            Program.MainForm.picExpandClose_Click(sender, e);

            // Minimize Form1
            Program.MainForm.WindowState = FormWindowState.Minimized;
            // Capture the entire screen
            Bitmap screenshot = CaptureScreen();

            // Save the screenshot to the Pictures folder
            SaveScreenshot(screenshot);
            // Minimize Form1
            Program.MainForm.WindowState = FormWindowState.Normal;
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
            MessageBox.Show($"Screenshot saved to {filePath}", "Screenshot Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
