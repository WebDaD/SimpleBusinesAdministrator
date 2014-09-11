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
