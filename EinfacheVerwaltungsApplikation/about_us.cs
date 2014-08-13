using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using ManageAdministerExalt.Classes;

namespace ManageAdministerExalt
{
    public partial class about_us : Form
    {
        private string TopCaption = "About " + Application.ProductName;

        public about_us()
        {
            InitializeComponent();
        }

        public about_us(string TopCaption, string Link)
        {
            InitializeComponent();
            this.productNameLabel.Text = Config.Name;
            this.productVersionLabel.Text = Config.Version;
            this.TopCaption = TopCaption;
            this.linkLabel.Text = Link;
        }

        private void topPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawIcon(Icon.ExtractAssociatedIcon(Application.ExecutablePath), 20, 8);
            e.Graphics.DrawString(TopCaption, new Font("Segoe UI", 14f), Brushes.Azure, new PointF(70, 10));
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.linkLabel.Text);
        }

    }
}
