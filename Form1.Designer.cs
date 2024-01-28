
namespace Floating_Controller
{
    partial class frmFloat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFloat));
            this.picExpand = new Bunifu.UI.WinForms.BunifuPictureBox();
            this.picClose = new Bunifu.UI.WinForms.BunifuPictureBox();
            this.picExpandClose = new Bunifu.UI.WinForms.BunifuPictureBox();
            this.bunifuGradientPanel1 = new Bunifu.UI.WinForms.BunifuGradientPanel();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picExpand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExpandClose)).BeginInit();
            this.bunifuGradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picExpand
            // 
            this.picExpand.AllowFocused = false;
            this.picExpand.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picExpand.AutoSizeHeight = true;
            this.picExpand.BorderRadius = 0;
            this.picExpand.Image = ((System.Drawing.Image)(resources.GetObject("picExpand.Image")));
            this.picExpand.IsCircle = true;
            this.picExpand.Location = new System.Drawing.Point(12, 8);
            this.picExpand.Name = "picExpand";
            this.picExpand.Size = new System.Drawing.Size(34, 34);
            this.picExpand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picExpand.TabIndex = 1;
            this.picExpand.TabStop = false;
            this.picExpand.Type = Bunifu.UI.WinForms.BunifuPictureBox.Types.Square;
            this.picExpand.Click += new System.EventHandler(this.picExpand_Click);
            // 
            // picClose
            // 
            this.picClose.AllowFocused = false;
            this.picClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picClose.AutoSizeHeight = true;
            this.picClose.BackColor = System.Drawing.Color.Transparent;
            this.picClose.BorderRadius = 17;
            this.picClose.Image = ((System.Drawing.Image)(resources.GetObject("picClose.Image")));
            this.picClose.IsCircle = true;
            this.picClose.Location = new System.Drawing.Point(92, 8);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(34, 34);
            this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picClose.TabIndex = 0;
            this.picClose.TabStop = false;
            this.picClose.Type = Bunifu.UI.WinForms.BunifuPictureBox.Types.Circle;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // picExpandClose
            // 
            this.picExpandClose.AllowFocused = false;
            this.picExpandClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picExpandClose.AutoSizeHeight = true;
            this.picExpandClose.BackColor = System.Drawing.Color.Transparent;
            this.picExpandClose.BorderRadius = 17;
            this.picExpandClose.Image = ((System.Drawing.Image)(resources.GetObject("picExpandClose.Image")));
            this.picExpandClose.IsCircle = true;
            this.picExpandClose.Location = new System.Drawing.Point(12, 8);
            this.picExpandClose.Name = "picExpandClose";
            this.picExpandClose.Size = new System.Drawing.Size(34, 34);
            this.picExpandClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picExpandClose.TabIndex = 2;
            this.picExpandClose.TabStop = false;
            this.picExpandClose.Type = Bunifu.UI.WinForms.BunifuPictureBox.Types.Circle;
            this.picExpandClose.Click += new System.EventHandler(this.picExpandClose_Click);
            // 
            // bunifuGradientPanel1
            // 
            this.bunifuGradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuGradientPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuGradientPanel1.BackgroundImage")));
            this.bunifuGradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuGradientPanel1.BorderRadius = 1;
            this.bunifuGradientPanel1.Controls.Add(this.picExpand);
            this.bunifuGradientPanel1.Controls.Add(this.picExpandClose);
            this.bunifuGradientPanel1.Controls.Add(this.picClose);
            this.bunifuGradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bunifuGradientPanel1.GradientBottomLeft = System.Drawing.Color.Gray;
            this.bunifuGradientPanel1.GradientBottomRight = System.Drawing.Color.Crimson;
            this.bunifuGradientPanel1.GradientTopLeft = System.Drawing.Color.DarkSlateGray;
            this.bunifuGradientPanel1.GradientTopRight = System.Drawing.Color.DarkKhaki;
            this.bunifuGradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.bunifuGradientPanel1.Name = "bunifuGradientPanel1";
            this.bunifuGradientPanel1.Quality = 10;
            this.bunifuGradientPanel1.Size = new System.Drawing.Size(138, 50);
            this.bunifuGradientPanel1.TabIndex = 3;
            this.bunifuGradientPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bunifuGradientPanel1_MouseDown);
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 10;
            this.bunifuElipse1.TargetControl = this;
            // 
            // frmFloat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(138, 50);
            this.ControlBox = false;
            this.Controls.Add(this.bunifuGradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFloat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Float";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.picExpand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExpandClose)).EndInit();
            this.bunifuGradientPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.UI.WinForms.BunifuPictureBox picClose;
        private Bunifu.UI.WinForms.BunifuPictureBox picExpand;
        private Bunifu.UI.WinForms.BunifuPictureBox picExpandClose;
        private Bunifu.UI.WinForms.BunifuGradientPanel bunifuGradientPanel1;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
    }
}

