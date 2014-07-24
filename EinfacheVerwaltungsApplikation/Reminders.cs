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
    public partial class Reminders : Form
    {
        public Reminders()
        {
            InitializeComponent();
            this.Text = Config.Name + " :: " + "Mahnungen";
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {

        }

        private void lv_reminders_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lv_reminders_Resize(object sender, EventArgs e)
        {

        }

        private void tb_TextChanged(object sender, EventArgs e)
        {

        }

        private void cb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO: edit nu:value! (decimals, max,min,etc)
        }

        private void cms_reminders_Opening(object sender, CancelEventArgs e)
        {

        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
