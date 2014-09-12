using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ManageAdministerExalt.Classes;
using WebDaD.Toolkit.Helper;
using WebDaD.Toolkit.Database;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace ManageAdministerExalt
{
    public partial class Options : Form
    {
        private bool editmode;

        public bool ChangedDatabase;
        public bool ChangedTimer;
        public bool ChangedTabs;

        public Options()
        {
            InitializeComponent();
            this.Text = Config.Name + " :: " + "Optionen";
            this.Icon = Properties.Resources.simba;

            this.editmode = false;

            //Allgemeines
            ck_all_askforexit.Checked = Config.AskForExit;
            cb_all_default_tab.Items.Add(new ComboBoxItem("tab_customers", "Kunden"));
            cb_all_default_tab.Items.Add(new ComboBoxItem("tab_services", "Leistungen"));
            cb_all_default_tab.Items.Add(new ComboBoxItem("tab_terms", "AGB's"));
            cb_all_default_tab.Items.Add(new ComboBoxItem("tab_reports", "Berichte"));
            cb_all_default_tab.Items.Add(new ComboBoxItem("tab_worker", "Mitarbeiter"));
            cb_all_default_tab.Items.Add(new ComboBoxItem("tab_jobs", "Aufträge"));
            cb_all_default_tab.Items.Add(new ComboBoxItem("tab_items", "Lager"));
            cb_all_default_tab.Items.Add(new ComboBoxItem("tab_expenses", "Ausgaben"));
            foreach (object o in cb_all_default_tab.Items)
            {
                if (o is ComboBoxItem)
                {
                    if (((ComboBoxItem)o).Value == Config.DefaultTab)
                    {
                        cb_all_default_tab.SelectedItem = o;
                        break;
                    }
                }
            }
            cb_tabs_customers.Checked = Config.Tabs["tab_customers"];
            cb_tabs_services.Checked = Config.Tabs["tab_services"];
            cb_tabs_terms.Checked = Config.Tabs["tab_terms"];
            cb_tabs_reports.Checked = Config.Tabs["tab_reports"];
            cb_tabs_workers.Checked = Config.Tabs["tab_worker"];
            cb_tabs_jobs.Checked = Config.Tabs["tab_jobs"];
            cb_tabs_items.Checked = Config.Tabs["tab_items"];
            cb_tabs_expenses.Checked = Config.Tabs["tab_expenses"];


            //Database
            cb_database_type.Items.Add(DatabaseType.MySQL.ToString());
            cb_database_type.Items.Add(DatabaseType.SQLite.ToString());
            cb_database_type.SelectedIndex = cb_database_type.FindString(Config.DatabaseType.ToString());
            if ((DatabaseType)Enum.Parse(typeof(DatabaseType), cb_database_type.SelectedItem.ToString()) == DatabaseType.SQLite)
            {
                lb_database_server.Text = "Datei";
                txt_database_user.Enabled = false;
                txt_database_password.Enabled = false;
                txt_database_name.Enabled = false;
                Database_SQLite d = Database_SQLite.createFromConnectionString(Config.DatabaseConnectionString);
                txt_database_server.Text = d.datasource;
                d = null;
            }
            else //mysql
            {
                Database_MySQL m = Database_MySQL.createFromConnectionString(Config.DatabaseConnectionString);
                txt_database_server.Text = m.server;
                txt_database_user.Text = m.user;
                txt_database_password.Text = m.password;
                txt_database_name.Text = m.database;
                m = null;
            }


            // Paths
            txt_paths_base.Text = Config.BasePath;
            txt_paths_backup.Text = Config.BackupFolder;
            txt_paths_wktohtml.Text = Config.WKHTMLTOPDFPath;

            txt_paths_customers.Text = Config.Paths["customers"];
            txt_paths_services.Text = Config.Paths["services"];
            txt_paths_terms.Text = Config.Paths["terms"];
            txt_paths_reports.Text = Config.Paths["reports"];
            txt_paths_worker.Text = Config.Paths["workers"];
            txt_paths_jobs.Text = Config.Paths["jobs"];
            txt_paths_items.Text = Config.Paths["items"];
            txt_paths_expenses.Text = Config.Paths["expenses"];

            //ID'S
            txt_ids_customers.Text = Config.IDFormating["customers"];
            txt_ids_services.Text = Config.IDFormating["services"];
            txt_ids_terms.Text = Config.IDFormating["terms"];
            txt_ids_reports.Text = Config.IDFormating["reports"];
            txt_ids_workers.Text = Config.IDFormating["workers"];
            txt_ids_jobs.Text = Config.IDFormating["jobs"];
            txt_ids_items.Text = Config.IDFormating["items"];
            txt_ids_expenses.Text = Config.IDFormating["expenses"];
            txt_ids_bills.Text = Config.IDFormating["bills"];

            //Timer
            cb_timer_active.Checked = Config.Timer;
            nd_timer_minutes.Value = Config.TimerInterval;
            cb_timer_backup.Checked = Config.TimerActions["backup"];
            cb_timer_refresh.Checked = Config.TimerActions["refresh"];
            cb_timer_reminder.Checked = Config.TimerActions["reminders"];


        }

        private void setEditmode(bool edit)
        {
            btn_save.Enabled = edit;
            btn_save_close.Enabled = edit;
            editmode = edit;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            if (editmode)
            {
                if (MessageBox.Show("Sie haben Einstellungen geändert.\n Wirklich Abbrechen?", "Frage", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            //Tabs
            Config.AskForExit = ck_all_askforexit.Checked;
            Config.DefaultTab = ((ComboBoxItem)cb_all_default_tab.SelectedItem).Value;
            if (cb_tabs_customers.Checked != Config.Tabs["tab_customers"] ||
                cb_tabs_services.Checked != Config.Tabs["tab_services"] ||
                cb_tabs_terms.Checked != Config.Tabs["tab_terms"] ||
                cb_tabs_reports.Checked != Config.Tabs["tab_reports"] ||
                cb_tabs_workers.Checked != Config.Tabs["tab_worker"] ||
                cb_tabs_jobs.Checked != Config.Tabs["tab_jobs"] ||
                cb_tabs_items.Checked != Config.Tabs["tab_items"] ||
                cb_tabs_expenses.Checked != Config.Tabs["tab_expenses"]
                ) ChangedTabs = true;
            Dictionary<string, bool> tabs = new Dictionary<string, bool>();
            tabs.Add("tab_customers", cb_tabs_customers.Checked);
            tabs.Add("tab_services", cb_tabs_services.Checked);
            tabs.Add("tab_terms", cb_tabs_terms.Checked);
            tabs.Add("tab_reports", cb_tabs_reports.Checked);
            tabs.Add("tab_worker", cb_tabs_workers.Checked);
            tabs.Add("tab_jobs", cb_tabs_jobs.Checked);
            tabs.Add("tab_items", cb_tabs_items.Checked);
            tabs.Add("tab_expenses", cb_tabs_expenses.Checked);

            Config.Tabs = tabs;

            //Database
            if ((DatabaseType)Enum.Parse(typeof(DatabaseType), cb_database_type.SelectedItem.ToString()) == DatabaseType.SQLite)
            {
                lb_database_server.Text = "Datei";
                txt_database_user.Enabled = false;
                txt_database_password.Enabled = false;
                txt_database_name.Enabled = false;
                Database_SQLite d = Database_SQLite.createFromConnectionString(Config.DatabaseConnectionString);
                txt_database_server.Text = d.datasource;
                if ((DatabaseType)Enum.Parse(typeof(DatabaseType), cb_database_type.SelectedItem.ToString()) != Config.DatabaseType ||
                    Config.DatabaseConnectionString != d.ConnectionString()
                    )ChangedDatabase=true;
                Config.DatabaseType = (DatabaseType)Enum.Parse(typeof(DatabaseType), cb_database_type.SelectedItem.ToString());
                Config.DatabaseConnectionString = d.ConnectionString();
                d = null;
            }
            else //mysql
            {
                Database_MySQL m = Database_MySQL.createFromConnectionString(Config.DatabaseConnectionString);
                txt_database_server.Text = m.server;
                txt_database_user.Text = m.user;
                txt_database_password.Text = m.password;
                txt_database_name.Text = m.database;
                if ((DatabaseType)Enum.Parse(typeof(DatabaseType), cb_database_type.SelectedItem.ToString()) != Config.DatabaseType ||
                    Config.DatabaseConnectionString != m.ConnectionString()
                    ) ChangedDatabase = true;
                Config.DatabaseType = (DatabaseType)Enum.Parse(typeof(DatabaseType), cb_database_type.SelectedItem.ToString());
                Config.DatabaseConnectionString = m.ConnectionString();
                m = null;
            }

            //Paths:
            if (Config.BasePath != txt_paths_base.Text)
            {
                FileSystem.CopyDirectory(Config.BasePath, txt_paths_base.Text, true);
                Config.BasePath = txt_paths_base.Text;
            }
            if (Config.BackupFolder != txt_paths_backup.Text)
            {
                FileSystem.CopyDirectory(Config.BackupFolder, txt_paths_backup.Text, true);
                Config.BackupFolder = txt_paths_backup.Text;
            }
            Config.WKHTMLTOPDFPath = txt_paths_wktohtml.Text;
            if (Config.Paths["customers"] != txt_paths_customers.Text)
            {
                FileSystem.CopyDirectory(   Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["customers"], 
                                            Config.BasePath + Path.DirectorySeparatorChar + txt_paths_customers.Text, true);
            }
            if (Config.Paths["services"] != txt_paths_services.Text)
            {
                FileSystem.CopyDirectory(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["services"],
                                            Config.BasePath + Path.DirectorySeparatorChar + txt_paths_services.Text, true);
            }
            if (Config.Paths["terms"] != txt_paths_terms.Text)
            {
                FileSystem.CopyDirectory(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["terms"],
                                            Config.BasePath + Path.DirectorySeparatorChar + txt_paths_terms.Text, true);
            }
            if (Config.Paths["reports"] != txt_paths_reports.Text)
            {
                FileSystem.CopyDirectory(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["reports"],
                                            Config.BasePath + Path.DirectorySeparatorChar + txt_paths_reports.Text, true);
            }
            if (Config.Paths["workers"] != txt_paths_worker.Text)
            {
                FileSystem.CopyDirectory(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["workers"],
                                            Config.BasePath + Path.DirectorySeparatorChar + txt_paths_worker.Text, true);
            }
            if (Config.Paths["jobs"] != txt_paths_jobs.Text)
            {
                FileSystem.CopyDirectory(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["jobs"],
                                            Config.BasePath + Path.DirectorySeparatorChar + txt_paths_jobs.Text, true);
            }
            if (Config.Paths["items"] != txt_paths_items.Text)
            {
                FileSystem.CopyDirectory(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["items"],
                                            Config.BasePath + Path.DirectorySeparatorChar + txt_paths_items.Text, true);
            }
            if (Config.Paths["expenses"] != txt_paths_expenses.Text)
            {
                FileSystem.CopyDirectory(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["expenses"],
                                            Config.BasePath + Path.DirectorySeparatorChar + txt_paths_expenses.Text, true);
            }
            Dictionary<string, string> paths = new Dictionary<string, string>();
            paths.Add("customers", txt_paths_customers.Text);
            paths.Add("services", txt_paths_services.Text);
            paths.Add("terms", txt_paths_terms.Text);
            paths.Add("reports", txt_paths_reports.Text);
            paths.Add("workers", txt_paths_worker.Text);
            paths.Add("jobs", txt_paths_jobs.Text);
            paths.Add("items", txt_paths_items.Text);
            paths.Add("expenses", txt_paths_expenses.Text);
            Config.Paths = paths;

            //ID
            renameFolders(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["customers"], Config.IDFormating["customers"], txt_ids_customers.Text);
            renameFolders(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["services"], Config.IDFormating["services"], txt_ids_services.Text);
            renameFolders(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["terms"], Config.IDFormating["terms"], txt_ids_terms.Text);
            renameFolders(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["reports"], Config.IDFormating["reports"], txt_ids_reports.Text);
            renameFolders(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["workers"], Config.IDFormating["workers"], txt_ids_workers.Text);
            renameFolders(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["jobs"], Config.IDFormating["jobs"], txt_ids_jobs.Text);
            renameFolders(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["items"], Config.IDFormating["items"], txt_ids_items.Text);
            renameFolders(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["expenses"], Config.IDFormating["expenses"], txt_ids_expenses.Text);
            renameFolders(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["bills"], Config.IDFormating["bills"], txt_ids_bills.Text);
            Dictionary<string, string> formating = new Dictionary<string, string>();
            formating.Add("customers", txt_ids_customers.Text);
            formating.Add("services", txt_ids_services.Text);
            formating.Add("terms", txt_ids_terms.Text);
            formating.Add("reports", txt_ids_reports.Text);
            formating.Add("workers", txt_ids_workers.Text);
            formating.Add("jobs", txt_ids_jobs.Text);
            formating.Add("items", txt_ids_items.Text);
            formating.Add("expenses", txt_ids_expenses.Text);
            formating.Add("bills", txt_ids_bills.Text);
            Config.IDFormating = formating;

            //Timer
            if (cb_timer_active.Checked != Config.Timer ||
                nd_timer_minutes.Value != Config.TimerInterval ||
                cb_timer_backup.Checked != Config.TimerActions["backup"] ||
                cb_timer_refresh.Checked != Config.TimerActions["refresh"] ||
                cb_timer_reminder.Checked != Config.TimerActions["reminders"]
                ) ChangedTabs = true;
            Config.Timer = cb_timer_active.Checked;
            Config.TimerInterval = Convert.ToInt32(nd_timer_minutes.Value);
            Dictionary<string, bool> timer = new Dictionary<string, bool>();
            timer.Add("backup", cb_timer_backup.Checked);
            timer.Add("refresh", cb_timer_refresh.Checked);
            timer.Add("reminders", cb_timer_reminder.Checked);
            Config.TimerActions = timer;
        }

        private void renameFolders(string basePath, string from, string to)
        {
            if (from != to)
            {
                foreach (string item in Directory.GetDirectories(basePath))
                {
                    string id = Config.GetIDFromFormattedName(item);
                    string newName = Config.CreateNiceID(to, id);
                    FileSystem.CopyDirectory(basePath + Path.DirectorySeparatorChar + item, basePath + Path.DirectorySeparatorChar + newName);
                }
            }
        }

        private void btn_save_close_Click(object sender, EventArgs e)
        {
            btn_save_Click(sender, e);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void Content_TextChanged(object sender, EventArgs e)
        {
            setEditmode(true);
        }


        private void txt_database_server_Click(object sender, EventArgs e)
        {
            //TODO: if type == sqlite --> open filebrowser
        }

        private void txt_paths_base_Click(object sender, EventArgs e)
        {
            //TODO: open folderbrowser
        }

        private void txt_paths_backup_Click(object sender, EventArgs e)
        {
            //TODO: open folderbrowser
        }

        private void txt_paths_wktohtml_Click(object sender, EventArgs e)
        {
            //TODO: open filebrowser (*.exe
        }


    }
}
