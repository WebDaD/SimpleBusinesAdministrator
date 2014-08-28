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
    public partial class Discounts : Form
    {
        private Database db;
        private Discount discount;
        private Dictionary<string, string> discounts;
        public Discounts(Database db)
        {
            this.db = db;
            InitializeComponent();
            this.Text = Config.Name + " :: " + "Rabatte";
            this.Icon = Properties.Resources.simba;
            fillDiscounts();
            foreach (KeyValuePair<int,string> item in Discount.TYPES)
            {
                cb_type.Items.Add(item.Value);
            }
        }

        private void fillDiscounts()
        {
            discounts = new Discount(db).GetIDList();
            if (discounts != null)
            {
                lv_discounts.Items.Clear();
                foreach (KeyValuePair<string, string> item in discounts)
                {
                    ListViewItem t = new ListViewItem(item.Key);
                    t.SubItems.Add(item.Value);
                    lv_discounts.Items.Add(t);
                }
            }
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            btn_save.Enabled = true;
            btn_cancel.Enabled = true;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            tb_name.Text = discount.Name;
            nu_value.Value = discount.Value;
            cb_type.SelectedItem = cb_type.Items[discount.Type];
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            discount.Name = tb_name.Text;
            discount.Value = nu_value.Value;
            discount.Type = cb_type.SelectedIndex;
            if (!discount.Save())
            {
                MessageBox.Show("Konnte nicht speichern...", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Rabatt " + discount.Name + " gespeichert.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
            int sele = -1;
            if (lv_discounts.SelectedIndices.Count > 0)
            {
                sele = lv_discounts.SelectedIndices[0];
            }
            fillDiscounts();
            if (sele > 0)
            {
                lv_discounts.Items[sele].Selected = true;
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            if (btn_save.Enabled)
            {
                if (MessageBox.Show("Daten wurden nicht gespeichert\nWirklich beenden?", "Frage", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                }
                else { return; }
            }
            this.Close();
        }

        private void lv_discounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_discounts.SelectedIndices.Count > 0)
            {
                discount = new Discount(db, lv_discounts.SelectedItems[0].Text);

                lb_id.Text = discount.NiceID;
                tb_name.Text = discount.Name;
                nu_value.Value = discount.Value;
                cb_type.SelectedItem = cb_type.Items[discount.Type];


                btn_save.Enabled = false;
                btn_cancel.Enabled = false;

            }
        }

        private void lv_discounts_Resize(object sender, EventArgs e)
        {
            sizeLastColumn(lv_discounts);
        }

        private void cms_discounts_Opening(object sender, CancelEventArgs e)
        {
            löschenToolStripMenuItem.Enabled = lv_discounts.SelectedIndices.Count > 0;
        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lv_discounts.SelectedIndices.Clear();
            lb_id.Text = "";
            tb_name.Text = "";
            nu_value.Value = 0;
            cb_type.SelectedIndex = -1;
            discount = new Discount(db);
            btn_save.Enabled = true;
            tb_name.Focus();
        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Wollen Sie wirklick " + discount.NiceID + " (" + discount.Name + ") löschen?", "Bestätigung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                discount.Delete();
                fillDiscounts();
                lv_discounts.SelectedIndices.Clear();
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            neuToolStripMenuItem_Click(sender, e);
        }

        private void Discounts_Load(object sender, EventArgs e)
        {
            sizeLastColumn(lv_discounts);
        }

        private void Discounts_Resize(object sender, EventArgs e)
        {
            sizeLastColumn(lv_discounts);
        }
        private void sizeLastColumn(ListView lv)
        {
            lv.Columns[lv.Columns.Count - 1].Width = -2;
        }
    }
}
