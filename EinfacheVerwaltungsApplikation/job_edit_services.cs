using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ManageAdministerExalt.Classes;
using WebDaD.Toolkit.Database;

namespace ManageAdministerExalt
{
    public partial class job_edit_services : Form
    {
        private Dictionary<Service, int> services;
        private List<Service> avaiableServices;
        private Database db;

        private Service selectedService;

        public Dictionary<Service, int> Services { get { return this.services; } }



        private bool changes;

        public job_edit_services(Dictionary<Service,int> services, string jobid, Database db)
        {
            InitializeComponent();
            this.services = services;
            this.db = db;
            this.changes = false;
            this.Text = Config.Name + " :: " + "Leistungen :: " + jobid;
            this.avaiableServices = getAvaiableServices();

            lv_avaiable.Items.Clear();
            foreach (Service item in avaiableServices)
            {
                ListViewItem t = new ListViewItem(item.NiceID);
                t.SubItems.Add(item.Name);
                t.Tag = item;
                lv_avaiable.Items.Add(t);
            }

            lv_on_job.Items.Clear();
            foreach (KeyValuePair<Service,int> item in this.services)
            {
                ListViewItem t = new ListViewItem(item.Key.NiceID);
                t.SubItems.Add(item.Key.Name);
                t.SubItems.Add(item.Value.ToString());
                t.Tag = item;
                lv_on_job.Items.Add(t);
            } 
        }

        private List<Service> getAvaiableServices()
        {
            List<Service> r = new List<Service>();

            foreach (CRUDable item in new Service(this.db).GetFullList())
            {
                r.Add((Service)item);
            }

            foreach (KeyValuePair<Service,int> item in this.services)
            {
                r.Remove(item.Key);
            }
            return r;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (lv_avaiable.SelectedIndices.Count > 0)
            {
                selectedService = (Service)lv_avaiable.SelectedItems[0].Tag;

                this.avaiableServices.Remove(selectedService);
                ListViewItem tmp = null;
                foreach (ListViewItem item in lv_avaiable.Items)
                {
                    if ((Service)item.Tag == selectedService) { tmp = item; break; }
                }
                if(tmp != null)lv_avaiable.Items.Remove(tmp);

                this.services.Add(selectedService, 1);
                ListViewItem t = new ListViewItem(selectedService.NiceID);
                t.SubItems.Add(selectedService.Name);
                t.SubItems.Add(1.ToString());
                t.Tag = selectedService;
                lv_on_job.Items.Add(t);

                this.changes = true;
                btn_save.Enabled = true;
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (lv_on_job.SelectedIndices.Count > 0)
            {
                selectedService = (Service)lv_on_job.SelectedItems[0].Tag;

                this.services.Remove(selectedService);
                ListViewItem tmp = null;
                foreach (ListViewItem item in lv_on_job.Items)
                {
                    if ((Service)item.Tag == selectedService) { tmp = item; break; }
                }
                if (tmp != null) lv_on_job.Items.Remove(tmp);

                this.avaiableServices.Add(selectedService);
                ListViewItem t = new ListViewItem(selectedService.NiceID);
                t.SubItems.Add(selectedService.Name);
                t.Tag = selectedService;
                lv_avaiable.Items.Add(t);

                this.changes = true;
                btn_save.Enabled = true;
            }
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            if (lv_on_job.SelectedIndices.Count > 0)
            {
                selectedService = (Service)lv_on_job.SelectedItems[0].Tag;
                this.services[selectedService]++;
                foreach (ListViewItem item in lv_on_job.Items)
                {
                    if ((Service)item.Tag == selectedService) 
                    {
                        item.SubItems[2].Text = this.services[selectedService].ToString(); 
                        break; 
                    }
                }
                this.changes = true;
                btn_save.Enabled = true;
            }
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {
            if (lv_on_job.SelectedIndices.Count > 0)
            {
                selectedService = (Service)lv_on_job.SelectedItems[0].Tag;
                if(this.services[selectedService]>0)
                    this.services[selectedService]--;
                foreach (ListViewItem item in lv_on_job.Items)
                {
                    if ((Service)item.Tag == selectedService)
                    {
                        item.SubItems[2].Text = this.services[selectedService].ToString();
                        break;
                    }
                }
                this.changes = true;
                btn_save.Enabled = true;
            }
        }

        private void cms_Services_Opening(object sender, CancelEventArgs e)
        {
            löschenToolStripMenuItem.Enabled = lv_on_job.SelectedIndices.Count > 0;
            plus1EinheitToolStripMenuItem.Enabled = lv_on_job.SelectedIndices.Count > 0;
            minus1EinheitToolStripMenuItem.Enabled = lv_on_job.SelectedIndices.Count > 0;
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
                else { return; }
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
            btn_remove_Click(sender, e);
        }

        private void plus1EinheitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_plus_Click(sender, e);
        }

        private void minus1EinheitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_minus_Click(sender, e);
        }

        private void lv_avaiable_Resize(object sender, EventArgs e)
        {
            lv_avaiable.Columns[lv_avaiable.Columns.Count - 1].Width = -2;
        }

        private void job_edit_services_Load(object sender, EventArgs e)
        {
            lv_on_job.Columns[lv_on_job.Columns.Count - 1].Width = -2;
            lv_avaiable.Columns[lv_avaiable.Columns.Count - 1].Width = -2;
        }

    }
}
