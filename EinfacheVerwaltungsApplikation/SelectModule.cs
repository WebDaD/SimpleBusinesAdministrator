using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using WebDaD.Toolkit.Database;
using ManageAdministerExalt.Classes;

namespace ManageAdministerExalt
{
    public partial class SelectModule : Form
    {
        public SelectModule()
        {
            InitializeComponent();
            this.Text = Config.Name + " :: " + "Datenbank";
        }

        private void SelectModule_Load(object sender, EventArgs e)
        {
            foreach (DatabaseType t in (DatabaseType[])Enum.GetValues(typeof(DatabaseType)))
            {
                cb_module.Items.Add(t.ToString());
            }
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            DialogResult t = ofd.ShowDialog();
            if (t == System.Windows.Forms.DialogResult.OK)
            {
                tb_path.Text = ofd.FileName;
            }

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (cb_module.SelectedIndex < 0)
            {
                MessageBox.Show("Bitte ein Modul wählen", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(tb_path.Text))
            {
                MessageBox.Show("Bitte den Pfad der Datenbank wählen", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Config.DatabasePath = tb_path.Text;
            Config.DatabaseType = (DatabaseType)Enum.Parse(typeof(DatabaseType), cb_module.SelectedItem.ToString());
            Config.BasePath = Application.StartupPath;
            //TODO: restliche Daten, wenn typ passend
            Main m = new Main();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void tb_path_Click(object sender, EventArgs e)
        {
            btn_browse_Click(sender, e);
        }

        private void cb_module_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //TODO: if MySQL enable textboxes, rename lb_path to Server
        }
    }
}
