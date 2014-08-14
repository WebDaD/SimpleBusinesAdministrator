using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Export;

namespace ManageAdministerExalt
{
    public partial class Templates : Form
    {
        private bool editmode;
        private Database db;
        private Template template;
        private Dictionary<string, string> templates;
        private List<string> images;

        public Templates(Database db)
        {
            InitializeComponent();
            this.editmode = false;
            this.db = db;
            images = new List<string>();
            template = new Template(this.db);

            templates = Template.getTemplates(this.db);
            if (templates != null)
            {
                templates.Remove("0");
                lv_templates.Items.Clear();
                foreach (KeyValuePair<string, string> item in templates)
                {
                    ListViewItem t = new ListViewItem(item.Key);
                    t.SubItems.Add(item.Value);
                    lv_templates.Items.Add(t);
                }
            }
        }

        private void lv_templates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_templates.SelectedIndices.Count > 0)
            {
                template = new Template(db, lv_templates.SelectedItems[0].Text);

                //TODO: Fill fields
                lb_id.Text = template.ID;
                tb_name.Text = template.Name;
                if (template.Header != null)
                {
                    rb_header_full.Checked =true;
                    tb_header_full.Text = template.Header;
                }
                else
                {
                    rb_header_split.Checked = true;
                    tb_header_left.Text = template.Header_Left;
                    tb_header_center.Text = template.Header_Center;
                    tb_header_right.Text = template.Header_Right;
                }
                //TODO ANgaben
                tb_beforeContent.Text = template.BeforeContent;
                tb_afterContent.Text = template.AfterContent;
                if (template.Footer != null)
                {
                    rb_footer_full.Checked = true;
                    tb_footer_full.Text = template.Footer;
                }
                else
                {
                    rb_footer_split.Checked = true;
                    tb_footer_left.Text = template.Footer_Left;
                    tb_footer_center.Text = template.Footer_Center;
                    tb_footer_right.Text = template.Footer_Right;
                }

                setEditMode(false);
            }
        }

        private void lv_templates_Resize(object sender, EventArgs e)
        {
            lv_templates.Columns[lv_templates.Columns.Count - 1].Width = -2;
        }

        private void Templates_Load(object sender, EventArgs e)
        {
            lv_templates.Columns[lv_templates.Columns.Count - 1].Width = -2;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            if (editmode)
            {
                if (MessageBox.Show("Sie haben Änderungen vorgenommen.\nWirklich Schließen?", "Frage", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                }
                else { return; }
            }
            else { this.Close(); }
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            //TODO: write fields to object (reverse of selcted_index_Changed)
            template.Save(); //TODO: save images to basepath/template/id/images/
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            btn_apply_Click(sender,e);
            this.Close();
        }

        private void rb_header_full_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_header_full.Checked)
            {
                tb_header_full.Enabled = true;
                tb_header_left.Enabled = false;
                tb_header_center.Enabled = false;
                tb_header_right.Enabled = false;
            }
            else
            {
                tb_header_full.Enabled = false;
                tb_header_left.Enabled = true;
                tb_header_center.Enabled = true;
                tb_header_right.Enabled = true;
            }
        }

        private void rb_header_split_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_header_full.Checked)
            {
                tb_header_full.Enabled = true;
                tb_header_left.Enabled = false;
                tb_header_center.Enabled = false;
                tb_header_right.Enabled = false;
            }
            else
            {
                tb_header_full.Enabled = false;
                tb_header_left.Enabled = true;
                tb_header_center.Enabled = true;
                tb_header_right.Enabled = true;
            }
        }

        private void rb_footer_full_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_footer_full.Checked)
            {
                tb_footer_full.Enabled = true;
                tb_footer_left.Enabled = false;
                tb_footer_center.Enabled = false;
                tb_footer_right.Enabled = false;
            }
            else
            {
                tb_footer_full.Enabled = false;
                tb_footer_left.Enabled = true;
                tb_footer_center.Enabled = true;
                tb_footer_right.Enabled = true;
            }
        }

        private void rb_footer_split_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_footer_full.Checked)
            {
                tb_footer_full.Enabled = true;
                tb_footer_left.Enabled = false;
                tb_footer_center.Enabled = false;
                tb_footer_right.Enabled = false;
            }
            else
            {
                tb_footer_full.Enabled = false;
                tb_footer_left.Enabled = true;
                tb_footer_center.Enabled = true;
                tb_footer_right.Enabled = true;
            }
        }

        private void bildEinfügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: open image selector.
            //TODO: save image into LIST
            //TODO: set TAG into SENDER field (after cursor)
        }

        private void tb_changed(object sender, EventArgs e)
        {
            setEditMode(true);
        }
        private void setEditMode(bool edit)
        {
            this.editmode = edit;
            btn_apply.Enabled = edit;
            btn_save.Enabled = edit;
        }

    }
}
