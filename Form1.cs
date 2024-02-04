using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Floating_Controller
{
    public partial class frmFloat : Form
    {
        public frmFloat()
        {
            InitializeComponent();
            picExpandClose.Visible = false;
            picAbout.Visible = false;

            // Set the location of the Menu form to the left and vertically centered of frmFloat
            int leftPosition = this.Left - menuForm.Width-10;
            int topPosition = this.Top + (this.Height - menuForm.Height) / 2;
            menuForm.Location = new Point(leftPosition, topPosition);
            menuForm.Show();
        }
        Menu menuForm = new Menu();
        About aboutForm = new About();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        public void picExpandClose_Click(object sender, EventArgs e)
        {
            picExpand.Visible = true;
            picExpandClose.Visible = false;
            picAbout.Visible = false;
            // Check if the menuForm is not null and is not disposed before trying to close it
            if (menuForm != null && !menuForm.IsDisposed)
            {
                menuForm.Hide();
            }
            if (aboutForm != null && !aboutForm.IsDisposed)
            {
                aboutForm.Hide();
            }
        }

        public void picExpand_Click(object sender, EventArgs e)
        {
            picExpand.Visible = false;
            picAbout.Visible = true;
            picExpandClose.Visible = true;
            // Check if the menuForm is not null and is not disposed before trying to show it
            if (menuForm != null && !menuForm.IsDisposed)
            {
                // Set the location of the Menu form to the left and vertically centered of frmFloat
                int leftPosition = this.Left - menuForm.Width;
                int topPosition = this.Top + (this.Height - menuForm.Height) / 2;
                menuForm.Location = new Point(leftPosition, topPosition);
                menuForm.Show();
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void bunifuGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void picAbout_Click(object sender, EventArgs e)
        {
            if (aboutForm != null && !aboutForm.IsDisposed)
            {
                // Set the location of the Menu form to the left and vertically centered of frmFloat
                int leftPosition = this.Right;
                int topPosition = this.Top + (this.Height - aboutForm.Height) / 2;
                aboutForm.Location = new Point(leftPosition, topPosition);
                aboutForm.Show();
            }
        }
    }
}
