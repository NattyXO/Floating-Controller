using System;
using System.Drawing;
using System.Windows.Forms;

namespace Floating_Controller
{
    public partial class TaostNotification : Form
    {
        int toastX, toastY;
        public TaostNotification(string type, string message)
        {
            InitializeComponent();

            lblMessage.Text = message;

            lblType.Text = type;

            switch (type)
            {
                case "SUCCESS":
                    pnlBorder.BackColor = Color.FromArgb(0, 176, 47);
                    picIcon.Image = Properties.Resources.icons8_ok_240;
                    break;
                case "WARNING":
                    pnlBorder.BackColor = Color.FromArgb(208, 22, 0);
                    picIcon.Image = Properties.Resources.icons8_cancel_240;
                    break;
                case "INFO":
                    pnlBorder.BackColor = Color.FromArgb(47, 104, 237);
                    picIcon.Image = Properties.Resources.icons8_info_240;
                    break;

            }
        }
        private void TaostNotification_Load(object sender, EventArgs e)
        {
            AdjustWidthAndPosition();

            toastHide.Start();
            //toastTimer.Start();
        }
        private void AdjustWidthAndPosition()
        {
            // Set form width based on the label's width
            this.Width = lblMessage.Width + 60; // Adjust 40 based on your design

            // Set form position
            Position();
        }

        private void toastTimer_Tick(object sender, EventArgs e)
        {
            toastY -= 10;
            this.Location = new Point(toastX, toastY);
            if (toastY <= 10)
            {
                toastTimer.Stop();
                toastHide.Start();
            }
        }
        int y = 100;
        private void toastHide_Tick(object sender, EventArgs e)
        {
            // Decrease y by a larger value to speed up the hiding
            y--; // Adjust this value to control the speed of hiding

            if (y <= 0)
            {
                toastY += 1;
                this.Location = new Point(toastX, toastY += 10);
                if (toastY > 760)
                {
                    toastHide.Stop();
                    y = 100;
                    this.Close();

                }
            }
        }
        private void Position()
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            toastX = screenWidth - this.Width - 5;
            toastY = screenHeight - this.Height - 5;

            this.Location = new Point(toastX, toastY);
        }
    }
}
