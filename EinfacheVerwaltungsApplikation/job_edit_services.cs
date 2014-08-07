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
        private Job job;
        public job_edit_services(Job job)
        {
            InitializeComponent();
            this.job = job;
            //TODO: Load Services, fill left list and listview
            this.Text = Config.Name + " :: " + "Leistungen :: "+job.NiceID; 
        }

        private void btn_add_Click(object sender, EventArgs e)
        {

        }

        private void btn_remove_Click(object sender, EventArgs e)
        {

        }

        private void btn_plus_Click(object sender, EventArgs e)
        {

        }

        private void btn_minus_Click(object sender, EventArgs e)
        {

        }

        private void cms_Services_Opening(object sender, CancelEventArgs e)
        {

        }

        private void lv_on_job_Resize(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            //TODO: save all changes and Exit
        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void plus1EinheitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void minus1EinheitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
