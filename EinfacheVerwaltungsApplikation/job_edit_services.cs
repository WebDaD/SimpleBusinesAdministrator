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
    public partial class job_edit_services : Form
    {
        private Dictionary<Service, int> services;

        public Dictionary<Service, int> Services { get { return this.services; } }

        private bool changes;

        public job_edit_services(Dictionary<Service,int> services, string jobid)
        {
            InitializeComponent();
            this.services = services;
            this.changes = false;
            //TODO: Load Services, fill left list and listview
            this.Text = Config.Name + " :: " + "Leistungen :: "+jobid; 

            //TODO: enable save on change to changes
        }

        private void btn_add_Click(object sender, EventArgs e)
        {

            this.changes = true;
            btn_save.Enabled = true;
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {

            this.changes = true;
            btn_save.Enabled = true;
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {

            this.changes = true;
            btn_save.Enabled = true;
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {

            this.changes = true;
            btn_save.Enabled = true;
        }

        private void cms_Services_Opening(object sender, CancelEventArgs e)
        {

        }

        private void lv_on_job_Resize(object sender, EventArgs e)
        {
            lv_on_job.Columns[lv_on_job.Columns.Count - 1].Width = -2;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            if (changes)
            {
                if (MessageBox.Show("Sie haben Änderungen vorgenommen.\nWirklich Schließen?", "Frage", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else { this.Close(); }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            //TODO: Write lv_on_job to this.services
            this.Close();
        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.changes = true;
            btn_save.Enabled = true;
        }

        private void plus1EinheitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.changes = true;
            btn_save.Enabled = true;
        }

        private void minus1EinheitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.changes = true;
            btn_save.Enabled = true;
        }
    }
}
