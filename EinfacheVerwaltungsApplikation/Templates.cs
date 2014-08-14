using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebDaD.Toolkit.Database;

namespace ManageAdministerExalt
{
    public partial class Templates : Form
    {
        private bool editmode;
        private Database db;
        public Templates(Database db)
        {
            InitializeComponent();
            this.editmode = false;
            this.db = db;
            //TODO: FIll Template List
        }

        private void lv_templates_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lv_templates_Resize(object sender, EventArgs e)
        {

        }

        private void Templates_Load(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

        }

        private void btn_apply_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {

        }

        private void rb_header_full_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rb_header_split_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rb_footer_full_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rb_footer_split_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void bildEinfügenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tb_changed(object sender, EventArgs e)
        {
            setEditMode(true);
        }
        private void setEditMode(bool edit)
        {
            this.editmode = edit;
            //Enable, disable buttons
        }

    }
}
