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
            this.Icon = Properties.Resources.simba;
        }

        private void SelectModule_Load(object sender, EventArgs e)
        {
            foreach (DatabaseType t in (DatabaseType[])Enum.GetValues(typeof(DatabaseType)))
            {
                cb_module.Items.Add(t.ToString());
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
            
            
            Config.DatabaseType = (DatabaseType)Enum.Parse(typeof(DatabaseType), cb_module.SelectedItem.ToString());

            switch (Config.DatabaseType)
            {
                case DatabaseType.SQLite:
                    if (String.IsNullOrEmpty(tb_path.Text))
                    {
                        MessageBox.Show("Bitte den Pfad der Datenbank wählen", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Config.DatabaseConnectionString = Database_SQLite.GetConnectionString(tb_path.Text);
                    break;
                case DatabaseType.MySQL:
                    if (String.IsNullOrEmpty(tb_path.Text))
                    {
                        MessageBox.Show("Bitte den Server der Datenbank wählen", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (String.IsNullOrEmpty(tb_user.Text))
                    {
                        MessageBox.Show("Bitte den User der Datenbank wählen", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (String.IsNullOrEmpty(tb_pwd.Text))
                    {
                        MessageBox.Show("Bitte das Password der Datenbank wählen", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (String.IsNullOrEmpty(tb_name.Text))
                    {
                        MessageBox.Show("Bitte den Name der Datenbank wählen", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Config.DatabaseConnectionString = Database_MySQL.GetConnectionString(tb_path.Text, tb_name.Text, tb_user.Text, tb_pwd.Text);
                    break;
                default:
                    break;
            }
            if (String.IsNullOrEmpty(txt_basepath.Text))
            {
                MessageBox.Show("Bitte den Basispfad wählen", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Config.BasePath = txt_basepath.Text;
            if (String.IsNullOrEmpty(txt_backup.Text))
            {
                MessageBox.Show("Bitte den Backup-Ordner wählen", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Config.BackupFolder = txt_backup.Text;
            if (String.IsNullOrEmpty(txt_wkhtml.Text))
            {
                MessageBox.Show("Bitte den Pfad von wkhtmltopdf.exe wählen", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Config.WKHTMLTOPDFPath = txt_wkhtml.Text;

            Config.FirstStart = false;
            Main m = new Main();
            this.Hide();
            m.ShowDialog();
            this.Close();
        }

        private void tb_path_Click(object sender, EventArgs e)
        {
            if ((DatabaseType)Enum.Parse(typeof(DatabaseType), cb_module.SelectedItem.ToString()) == DatabaseType.SQLite)
            {
                if (DialogResult.OK == openFileDialog_DB.ShowDialog())
                {
                    tb_path.Text = openFileDialog_DB.FileName;
                }
            }
        }

        private void cb_module_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((DatabaseType)Enum.Parse(typeof(DatabaseType), cb_module.SelectedItem.ToString()) == DatabaseType.SQLite)
            {
                lb_path.Text = "Datei";
                tb_user.Enabled = false;
                tb_pwd.Enabled = false;
                tb_name.Enabled = false;
            }
            else
            {
                lb_path.Text = "Server";
                tb_user.Enabled = true;
                tb_pwd.Enabled = true;
                tb_name.Enabled = true;
            }
        }

        private void txt_basepath_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == folderBrowserDialog.ShowDialog())
            {
                txt_basepath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void txt_backup_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == folderBrowserDialog.ShowDialog())
            {
                txt_backup.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void txt_wkhtml_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog_EXE.ShowDialog())
            {
                txt_wkhtml.Text = openFileDialog_EXE.FileName;
            }
        }
    }
}
