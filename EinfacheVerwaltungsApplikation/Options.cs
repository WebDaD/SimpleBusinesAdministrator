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

namespace ManageAdministerExalt
{
    public partial class Options : Form
    {
        private bool editmode;
        public Options()
        {
            InitializeComponent();
            this.Text = Config.Name + " :: " + "Optionen";
            this.Icon = Properties.Resources.simba;

            this.editmode = false;

            //TODO: load settings from config
            
            //Allgemeines
            ck_all_askforexit.Checked = Config.AskForExit;
            cb_all_default_tab.Items.Add(new ComboBoxItem("tab_customers","Kunden"));
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
            lb_active_tabs.Items.Add(new ComboBoxItem("tab_customers", "Kunden"));
            lb_active_tabs.Items.Add(new ComboBoxItem("tab_services", "Leistungen"));
            lb_active_tabs.Items.Add(new ComboBoxItem("tab_terms", "AGB's"));
            lb_active_tabs.Items.Add(new ComboBoxItem("tab_reports", "Berichte"));
            lb_active_tabs.Items.Add(new ComboBoxItem("tab_worker", "Mitarbeiter"));
            lb_active_tabs.Items.Add(new ComboBoxItem("tab_jobs", "Aufträge"));
            lb_active_tabs.Items.Add(new ComboBoxItem("tab_items", "Lager"));
            lb_active_tabs.Items.Add(new ComboBoxItem("tab_expenses", "Ausgaben"));
            foreach (string at in Config.ActiveTabs)
            {
                foreach (object o in lb_active_tabs.Items)
                {
                    if (o is ComboBoxItem)
                    {
                        if (((ComboBoxItem)o).Value == at)
                        {
                            lb_active_tabs.SetSelected(lb_active_tabs.Items.IndexOf(o), true);
                            break;
                        }
                    }
                }
            }
           

            //Database


            //Paths


            //ID'S
            txt_ids_customers.Text = Config.IDFormating["customers"];


            //Timer


        }

        private void setEditmode(bool edit)
        {
            btn_save.Enabled = edit;
            btn_save_close.Enabled = edit;
            editmode = edit;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (editmode)
            {
                //msg
            }
            else
            {
                this.Close();
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            //TODO: Write Fields to Config.Props
        }

        private void btn_save_close_Click(object sender, EventArgs e)
        {
            btn_save_Click(sender, e);
            this.Close();
        }

        private void Content_TextChanged(object sender, EventArgs e)
        {
            setEditmode(true);
        }


        private void txt_database_server_Click(object sender, EventArgs e)
        {
            //if type == sqlite --> open filebrowser
        }

        private void txt_paths_base_Click(object sender, EventArgs e)
        {

        }

        private void txt_paths_backup_Click(object sender, EventArgs e)
        {

        }

        private void txt_paths_wktohtml_Click(object sender, EventArgs e)
        {

        }
    }
}
