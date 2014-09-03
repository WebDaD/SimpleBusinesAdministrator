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
    public partial class Reminders : Form
    {
        private bool editmode = false;
        private Database db;
        private Reminder reminder;
        private Dictionary<string, string> reminders;

        public Reminders(Database db)
        {
            InitializeComponent();
            this.Text = Config.Name + " :: " + "Mahnungen";
            this.Icon = Properties.Resources.simba;
            this.db = db;
            reminder = new Reminder(db);
            fillReminders();
            foreach (KeyValuePair<int, string> item in Reminder.Types)
            {
                cb_type.Items.Add(item.Value);
            }
        }
        private void fillReminders()
        {
            reminders = reminder.GetDictionary();
            if (reminders != null)
            {
                lv_reminders.Items.Clear();
                foreach (KeyValuePair<string, string> item in reminders)
                {
                    ListViewItem t = new ListViewItem(item.Key);
                    string[] tmp = item.Value.Split('|');
                    t.SubItems.Add(tmp[0]);
                    t.SubItems.Add(tmp[1]);
                    lv_reminders.Items.Add(t);
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            setReminderFields();
            setEditMode(false);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            getReminderFields();
            if (!reminder.Save())
            {
                MessageBox.Show("Konnte nicht speichern...", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Mahnung " + reminder.Name + " gespeichert.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            int sele = -1;
            if (lv_reminders.SelectedIndices.Count > 0)
            {
                sele = lv_reminders.SelectedIndices[0];
            }
            fillReminders();
            if (sele > 0)
            {
                lv_reminders.Items[sele].Selected = true;
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

        private void lv_reminders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_reminders.SelectedIndices.Count > 0)
            {
                reminder = new Reminder(db, lv_reminders.SelectedItems[0].Text);

                setReminderFields();

                setEditMode(false);

            }
        }

        private void lv_reminders_Resize(object sender, EventArgs e)
        {
            sizeLastColumn(lv_reminders);
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
            löschenToolStripMenuItem.Enabled = lv_reminders.SelectedIndices.Count > 0;
        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lv_reminders.SelectedIndices.Clear();
            reminder = new Reminder(db);
            setReminderFields();
            setEditMode(true);
            tb_name.Focus();
        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Wollen Sie wirklick " + reminder.NiceID + " (" + reminder.Name + ") löschen?", "Bestätigung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                reminder.Delete();
                fillReminders();
                lv_reminders.SelectedIndices.Clear();
            }
        }

        private void Reminders_Load(object sender, EventArgs e)
        {
            sizeLastColumn(lv_reminders);
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

        private void setReminderFields()
        {
            lb_id.Text = reminder.NiceID;
            tb_name.Text = reminder.Name;
            nu_period.Value = reminder.Period;
            nu_value.Value = reminder.Value;
            cb_type.SelectedIndex = reminder.Type;
        }
        private void getReminderFields()
        {
            reminder.Name = tb_name.Text;
            reminder.Period = nu_period.Value;
            reminder.Value = nu_value.Value;
            reminder.Type = cb_type.SelectedIndex;
        }
    }
}
