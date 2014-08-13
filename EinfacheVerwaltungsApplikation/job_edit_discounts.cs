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
        private Database db;
        public List<Discount> Discounts;

        public job_edit_discounts(List<Discount> discounts,string id, Database db)
        {
            InitializeComponent();
            this.discounts = discounts;
            this.db = db;
            this.Text = Config.Name + " :: " + "Rabatte :: " + id; 
        }

        private void job_edit_discounts_Load(object sender, EventArgs e)
        {

        }

        private void lv_avaiable_Resize(object sender, EventArgs e)
        {

        }

        private void lv_on_job_Resize(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {

        }

        private void btn_remove_Click(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {

        }
    }
}
