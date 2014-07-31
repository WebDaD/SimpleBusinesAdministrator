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
    public partial class Payment_Conditions : Form
    {
        private bool editmode = false;
        private Database db;
        private PaymentCondition payment_condition;
        private Dictionary<string, string> payment_conditions;

        public Payment_Conditions(Database db)
        {
            InitializeComponent();
            this.Text = Config.Name + " :: " + "Zahlungsbedingungen";
            this.db = db;
            fillPaymentConditions();
            foreach (KeyValuePair<int, string> item in Reminder.Types)
            {
                cb_type.Items.Add(item.Value);
            }
        }
        private void fillPaymentConditions()
        {
            payment_conditions = new PaymentCondition(db).GetIDList();
            if (payment_conditions != null)
            {
                lv_payment_conditions.Items.Clear();
                foreach (KeyValuePair<string, string> item in payment_conditions)
                {
                    ListViewItem t = new ListViewItem(item.Key);
                    string[] tmp = item.Value.Split('|');
                    t.SubItems.Add(tmp[0]);
                    t.SubItems.Add(tmp[1]);
                    lv_payment_conditions.Items.Add(t);
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            setPaymentConditionFields();
            setEditMode(false);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            getPaymentConditionFields();
            if (!payment_condition.Save())
            {
                MessageBox.Show("Konnte nicht speichern...", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Zahlungsbedingung " + payment_condition.Name + " gespeichert.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            int sele = -1;
            if (lv_payment_conditions.SelectedIndices.Count > 0)
            {
                sele = lv_payment_conditions.SelectedIndices[0];
            }
            fillPaymentConditions();
            if (sele > 0)
            {
                lv_payment_conditions.Items[sele].Selected = true;
            }
            setEditMode(false);
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            if (editmode)
            {
                if (MessageBox.Show("Wirklich schließen?\nÄnderungen gehen verloren!", "Frage", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else { this.Close(); }
        }

        private void lv_payment_conditions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_payment_conditions.SelectedIndices.Count > 0)
            {
                payment_condition = new PaymentCondition(db, lv_payment_conditions.SelectedItems[0].Text);

                setPaymentConditionFields();
                
                setEditMode(false);

            }
        }

        private void lv_payment_conditions_Resize(object sender, EventArgs e)
        {
            sizeLastColumn(lv_payment_conditions);
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            setEditMode(true);
        }

        private void cb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_type.SelectedIndex == 0) //prozent
            {
                nu_value.Maximum = 100;
            }
            else //festbetrag
            {
                nu_value.Maximum = 100000;
            }
        }

        private void cms_reminders_Opening(object sender, CancelEventArgs e)
        {
            löschenToolStripMenuItem.Enabled = lv_payment_conditions.SelectedIndices.Count > 0;
        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lv_payment_conditions.SelectedIndices.Clear();
            payment_condition = new PaymentCondition(db);
            setPaymentConditionFields();
            setEditMode(true);
            tb_name.Focus();
        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Wollen Sie wirklick " + payment_condition.NiceID + " (" + payment_condition.Name + ") löschen?", "Bestätigung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                payment_condition.Delete();
                fillPaymentConditions();
                lv_payment_conditions.SelectedIndices.Clear();
            }
        }

        private void Payment_Conditions_Load(object sender, EventArgs e)
        {
            sizeLastColumn(lv_payment_conditions);
        }

        private void sizeLastColumn(ListView lv)
        {
            lv.Columns[lv.Columns.Count - 1].Width = -2;
        }
        private void setEditMode(bool p)
        {
            btn_cancel.Enabled = p;
            btn_save.Enabled = p;
            editmode = p;
        }

        private void setPaymentConditionFields()
        {
            lb_id.Text = payment_condition.NiceID;
            tb_name.Text = payment_condition.Name;
            nu_period.Value = payment_condition.Period;
            nu_value.Value = payment_condition.Value;
            cb_type.SelectedIndex = payment_condition.Type;
        }
        private void getPaymentConditionFields()
        {
            payment_condition.Name = tb_name.Text;
            payment_condition.Period = nu_period.Value;
            payment_condition.Value = nu_value.Value;
            payment_condition.Type = cb_type.SelectedIndex;
        }
    }
}
