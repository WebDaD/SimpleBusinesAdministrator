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
        private List<Service> avaiableServices;

        public Dictionary<Service, int> Services { get { return this.services; } }

        private bool changes;

        public job_edit_services(Dictionary<Service,int> services, string jobid)
        {
            InitializeComponent();
            this.services = services;
            this.changes = false;
            this.Text = Config.Name + " :: " + "Leistungen :: " + jobid;
            this.avaiableServices = getAvaiableServices();

            lv_avaiable.Items.Clear();
            foreach (Service item in avaiableServices)
            {
                ListViewItem t = new ListViewItem(item.NiceID);
                t.SubItems.Add(item.Name);
                lv_avaiable.Items.Add(t);
            }

            lv_on_job.Items.Clear();
            foreach (KeyValuePair<Service,int> item in this.services)
            {
                 ListViewItem t = new ListViewItem(item.Key.NiceID);
                t.SubItems.Add(item.Key.Name);
                t.SubItems.Add(item.Value.ToString());
                lv_avaiable.Items.Add(t);
            } 
        }

        private List<Service> getAvaiableServices()
        {
            //TODO get all services, remove the ones in this.services and fill lv_avaiable
            throw new NotImplementedException();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            //TODO only if left has select
            //TODO Delete item in lv_avaiable, avaiableservices
            //TODO Add item to lv_on_job, this.services
            this.changes = true;
            btn_save.Enabled = true;
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            //TODO only if right has select
            //TODO Add item in lv_avaiable, avaiableservices
            //TODO Delete item to lv_on_job, this.services
            this.changes = true;
            btn_save.Enabled = true;
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            //TODO only if selected
            //TODO change this.services, lv_on_job
            this.changes = true;
            btn_save.Enabled = true;
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {
            //TODO only if selected
            //TODO change this.services, lv_on_job
            this.changes = true;
            btn_save.Enabled = true;
        }

        private void cms_Services_Opening(object sender, CancelEventArgs e)
        {
            //TODO only show points if selected
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
            this.Close();
        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO like btn_remove
            this.changes = true;
            btn_save.Enabled = true;
        }

        private void plus1EinheitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO like btn_plus
            this.changes = true;
            btn_save.Enabled = true;
        }

        private void minus1EinheitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO like btn_minus
            this.changes = true;
            btn_save.Enabled = true;
        }

        private void lv_avaiable_Resize(object sender, EventArgs e)
        {
            lv_avaiable.Columns[lv_avaiable.Columns.Count - 1].Width = -2;
        }
    }
}
