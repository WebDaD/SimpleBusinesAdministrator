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
    public partial class job_new_get_Customer : Form
    {
        public string customer_id;
        public job_new_get_Customer()
        {
            InitializeComponent();
            //TODO: fill combobox
            this.Text = Config.Name + " :: " + "Kundenauswahl";
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {

        }
    }
}
