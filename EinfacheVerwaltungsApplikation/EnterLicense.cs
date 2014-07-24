using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ManageAdministerExalt.Classes;

namespace ManageAdministerExalt
{
    public partial class EnterLicense : Form
    {
        public EnterLicense()
        {
            InitializeComponent();
            this.Text = Config.Name + " :: " + "Lizenzeingabe";
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tb_username.Text)) MessageBox.Show("Bitte Benutzername eingeben", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (String.IsNullOrEmpty(tb_licensekey.Text)) MessageBox.Show("Bitte Lizenzschlüssel eingeben", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (WebDaD.Toolkit.Licensing.License.Check(tb_username.Text, tb_licensekey.Text))
            {
                Config.Username = tb_username.Text;
                Config.LicenseKey = tb_licensekey.Text;
                SelectModule m = new SelectModule();
                this.Hide();
                m.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Lizenzschlüssel ist falsch!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
