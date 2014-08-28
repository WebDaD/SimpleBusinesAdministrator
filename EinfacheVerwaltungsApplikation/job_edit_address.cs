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
    public partial class job_edit_address : Form
    {
        private Job job;
        public Job Job { get { return this.job; } }

        private bool changes;
        public job_edit_address(Job job)
        {
            InitializeComponent();
            this.job = job;
            this.Text = Config.Name + " :: " + "Adresse :: " + job.NiceID;
            this.Icon = Properties.Resources.simba;
            this.changes = false;

            if (this.job.Address_Text == "Kunde")
            {
                rb_customer.Checked = true;
                rb_custom.Checked = false;
                tb_streetnr.Enabled = false;
                tb_plz.Enabled = false;
                tb_city.Enabled = false;
            }
            else
            {
                rb_customer.Checked = false;
                rb_custom.Checked = true;
                tb_streetnr.Enabled = true;
                tb_plz.Enabled = true;
                tb_city.Enabled = true;
                tb_streetnr.Text = job.Address_StreetNr;
                tb_plz.Text = job.Address_PLZ;
                tb_city.Text = job.Address_City;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            if (rb_custom.Checked)
            {
                if (tb_streetnr.Text == "")
                {
                    MessageBox.Show("Straße, Nr darf nicht leer sein!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    this.job.Address_StreetNr = tb_streetnr.Text;
                }
                if (tb_plz.Text == "")
                {
                    MessageBox.Show("PLZ darf nicht leer sein!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    this.job.Address_PLZ = tb_plz.Text;
                }
                if (tb_city.Text == "")
                {
                    MessageBox.Show("Ort darf nicht leer sein!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    this.job.Address_City = tb_city.Text;
                }
            }
            else
            {
                this.job.SetAddressToCustomer();
            }
            this.Close();
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

        private void rb_custom_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_custom.Checked)
            {
                rb_customer.Checked = false;
                rb_custom.Checked = true;
                tb_streetnr.Enabled = true;
                tb_plz.Enabled = true;
                tb_city.Enabled = true;
            }
            else
            {
                rb_customer.Checked = true;
                rb_custom.Checked = false;
                tb_streetnr.Enabled = false;
                tb_plz.Enabled = false;
                tb_city.Enabled = false;
            }
            this.changes = true;
            btn_save.Enabled = true;
        }

        private void rb_customer_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_custom.Checked)
            {
                rb_customer.Checked = false;
                rb_custom.Checked = true;
                tb_streetnr.Enabled = true;
                tb_plz.Enabled = true;
                tb_city.Enabled = true;
            }
            else
            {
                rb_customer.Checked = true;
                rb_custom.Checked = false;
                tb_streetnr.Enabled = false;
                tb_plz.Enabled = false;
                tb_city.Enabled = false;
            }
            this.changes = true;
            btn_save.Enabled = true;
        }

        private void tb_changed(object sender, EventArgs e)
        {
            this.changes = true;
            btn_save.Enabled = true;
        }
    }
}
