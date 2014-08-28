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
    public partial class job_edit_discounts : Form
    {
        private List<Discount> discounts;
        private List<Discount> avaiableDiscounts;
        private Discount selectedDiscount;
        private Database db;
        private bool changed;

        public List<Discount> Discounts { get { return this.discounts; } }

        public job_edit_discounts(List<Discount> discounts,string id, Database db)
        {
            InitializeComponent();
            this.discounts = discounts;
            this.db = db;
            this.changed = false;
            this.Text = Config.Name + " :: " + "Rabatte :: " + id;
            this.Icon = Properties.Resources.simba;
            this.avaiableDiscounts = getAvaiableDiscounts();

            lv_avaiable.Items.Clear();
            foreach (Discount item in avaiableDiscounts)
            {
                ListViewItem t = new ListViewItem(item.NiceID);
                t.SubItems.Add(item.Name);
                t.Tag = item;
                lv_avaiable.Items.Add(t);
            }

            lv_on_job.Items.Clear();
            foreach (Discount item in this.discounts)
            {
                ListViewItem t = new ListViewItem(item.NiceID);
                t.SubItems.Add(item.Name);
                t.Tag = item;
                lv_on_job.Items.Add(t);
            } 
        }

        private List<Discount> getAvaiableDiscounts()
        {
            List<Discount> r = new List<Discount>();

            foreach (CRUDable item in new Discount(this.db).GetFullList())
            {
                r.Add((Discount)item);
            }

            foreach (Discount item in this.discounts)
            {
                r.RemoveAll(x => x.ID == item.ID);
            }
            return r;
        }

        private void job_edit_discounts_Load(object sender, EventArgs e)
        {
            lv_on_job.Columns[lv_on_job.Columns.Count - 1].Width = -2;
            lv_avaiable.Columns[lv_on_job.Columns.Count - 1].Width = -2;
        }

        private void lv_avaiable_Resize(object sender, EventArgs e)
        {
            lv_avaiable.Columns[lv_on_job.Columns.Count - 1].Width = -2;
        }

        private void lv_on_job_Resize(object sender, EventArgs e)
        {
            lv_on_job.Columns[lv_on_job.Columns.Count - 1].Width = -2;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (lv_avaiable.SelectedIndices.Count > 0)
            {
                selectedDiscount = (Discount)lv_avaiable.SelectedItems[0].Tag;

                this.avaiableDiscounts.Remove(selectedDiscount);
                ListViewItem tmp = null;
                foreach (ListViewItem item in lv_avaiable.Items)
                {
                    if ((Discount)item.Tag == selectedDiscount) { tmp = item; break; }
                }
                if (tmp != null) lv_avaiable.Items.Remove(tmp);

                this.discounts.Add(selectedDiscount);
                ListViewItem t = new ListViewItem(selectedDiscount.NiceID);
                t.SubItems.Add(selectedDiscount.Name);
                t.Tag = selectedDiscount;
                lv_on_job.Items.Add(t);

                this.changed = true;
                btn_save.Enabled = true;
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (lv_on_job.SelectedIndices.Count > 0)
            {
                selectedDiscount = (Discount)lv_on_job.SelectedItems[0].Tag;

                this.discounts.Remove(selectedDiscount);
                ListViewItem tmp = null;
                foreach (ListViewItem item in lv_on_job.Items)
                {
                    if ((Discount)item.Tag == selectedDiscount) { tmp = item; break; }
                }
                if (tmp != null) lv_on_job.Items.Remove(tmp);

                this.avaiableDiscounts.Add(selectedDiscount);
                ListViewItem t = new ListViewItem(selectedDiscount.NiceID);
                t.SubItems.Add(selectedDiscount.Name);
                t.Tag = selectedDiscount;
                lv_avaiable.Items.Add(t);

                this.changed = true;
                btn_save.Enabled = true;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            if (this.changed)
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
    }
}
