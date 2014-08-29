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
using System.IO;
using ManageAdministerExalt.Classes;

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


            this.Icon = Properties.Resources.simba;
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

                lb_id.Text = template.ID;
                tb_name.Text = template.Name;
                if (template.Header != null)
                {
                    rb_header_full.Checked = true;
                    tb_header_full.Text = template.Header.Replace(Template.FULLSTARTER, "").Replace(Template.LINEBREAK, "\n");
                    tb_header_left.Text ="";
                    tb_header_center.Text = "";
                    tb_header_right.Text ="";
                }
                else
                {
                    rb_header_split.Checked = true;
                    tb_header_full.Text = "";
                    tb_header_left.Text = template.Header_Left;
                    tb_header_center.Text = template.Header_Center;
                    tb_header_right.Text = template.Header_Right;
                }

                cb_textBefore_Left.Checked = template.TextBefore_Left.Equals(Template.ADDR);
                cb_textBefore_right_id.Checked = template.TextBefore_Right.Contains(Template.OBJECT_ID);
                cb_textBefore_right_worker.Checked = template.TextBefore_Right.Contains(Template.WORKER);
                cb_textBefore_right_creationdate.Checked = template.TextBefore_Right.Contains(Template.DATE_CREATE);
                cb_textBefore_right_second_date.Checked = template.TextBefore_Right.Contains(Template.DATE_SECOND);

                tb_beforeContent.Text = template.BeforeContent.Replace(Template.LINEBREAK, "\n");
                tb_afterContent.Text = template.AfterContent.Replace(Template.LINEBREAK, "\n");
                if (template.Footer != null)
                {
                    rb_footer_full.Checked = true;
                    tb_footer_full.Text = template.Footer.Replace(Template.FULLSTARTER, "").Replace(Template.LINEBREAK, "\n");
                    tb_footer_left.Text = "";
                    tb_footer_center.Text = "";
                    tb_footer_right.Text = "";
                }
                else
                {
                    rb_footer_split.Checked = true;
                    tb_footer_full.Text = "";
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
            template.Name = tb_name.Text;
            if (!rb_header_full.Checked)
            {
                template.Header = " | | ";
                template.Header_Left = tb_header_left.Text;
                template.Header_Center = tb_header_center.Text;
                template.Header_Right = tb_header_right.Text;
            }
            else
            {
                template.Header = tb_header_full.Text.Replace("\n", Template.LINEBREAK);
            }
            if (cb_textBefore_Left.Checked) template.TextBefore_Left = Template.ADDR;
            else template.TextBefore_Left = "";

            template.TextBefore_Right = "";
            if (cb_textBefore_right_id.Checked) template.TextBefore_Right += Template.OBJECT_ID + Template.LINEBREAK;
            if (cb_textBefore_right_worker.Checked) template.TextBefore_Right += Template.WORKER + Template.LINEBREAK;
            if (cb_textBefore_right_creationdate.Checked) template.TextBefore_Right += Template.DATE_CREATE + Template.LINEBREAK;
            if (cb_textBefore_right_second_date.Checked) template.TextBefore_Right += Template.DATE_SECOND + Template.LINEBREAK;

            template.BeforeContent = tb_beforeContent.Text.Replace("\n", Template.LINEBREAK);
            template.AfterContent = tb_afterContent.Text.Replace("\n", Template.LINEBREAK);

            if (!rb_footer_full.Checked)
            {
                template.Footer = " | | ";
                template.Footer_Left = tb_footer_left.Text;
                template.Footer_Center = tb_footer_center.Text;
                template.Footer_Right = tb_footer_right.Text;
            }
            else
            {
                template.Footer = tb_footer_full.Text.Replace("\n",Template.LINEBREAK);
            }
            template.Save(images, Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["template"] + Path.DirectorySeparatorChar + Config.IDFormating["template"] + Path.DirectorySeparatorChar + "images" + Path.DirectorySeparatorChar);
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
            lv_templates.SelectedIndices.Clear();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            btn_apply_Click(sender, e);
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

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lv_templates.SelectedIndices.Clear();

            lb_id.Text = "";
            tb_name.Text = "";

            rb_header_full.Checked = true;
            tb_header_full.Text = "";
            tb_header_left.Text = "";
            tb_header_center.Text = "";
            tb_header_right.Text = "";

            cb_textBefore_Left.Checked = false;
            cb_textBefore_right_id.Checked = false;
            cb_textBefore_right_worker.Checked = false;
            cb_textBefore_right_creationdate.Checked = false;
            cb_textBefore_right_second_date.Checked = false;

            tb_beforeContent.Text = "";
            tb_afterContent.Text = "";

            rb_footer_full.Checked = true;
            tb_footer_full.Text = "";


            tb_footer_left.Text = "";
            tb_footer_center.Text = "";
            tb_footer_right.Text = "";


            setEditMode(true);


            template = new Template(db);
            btn_apply.Enabled = true;
            tb_name.Focus();
        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Wollen Sie wirklick " + template.ID + " (" + template.Name + ") löschen?", "Bestätigung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                template.Delete();
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
                lv_templates.SelectedIndices.Clear();
            }
        }

        private void cmd_neu_Opening(object sender, CancelEventArgs e)
        {
            löschenToolStripMenuItem.Enabled = lv_templates.SelectedIndices.Count > 0;
        }
        private void bildEinfügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                images.Add(openFileDialog.FileName);
                string tag = Template.IMAGE_TAG + Path.GetFileName(openFileDialog.FileName) + Template.IMAGE_END;
                ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
                if (tsmi != null)
                {
                    ContextMenuStrip cms = tsmi.Owner as ContextMenuStrip;
                    if (cms != null)
                    {
                        if (cms.SourceControl is RichTextBox)
                        {
                            RichTextBox t = (RichTextBox)cms.SourceControl as RichTextBox;
                            int cp = t.SelectionStart;
                            t.Text = t.Text.Insert(cp, tag);
                        }
                        else if (cms.SourceControl is TextBox)
                        {
                            TextBox t = (TextBox)cms.SourceControl as TextBox;
                            int cp = t.SelectionStart;
                            t.Text = t.Text.Insert(cp, tag);
                        }

                    }
                }
            }
        }
        private void zeilenumbruchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi != null)
            {
                ContextMenuStrip cms = tsmi.Owner as ContextMenuStrip;
                if (cms != null)
                {
                    if (cms.SourceControl is RichTextBox)
                    {
                        RichTextBox t = (RichTextBox)cms.SourceControl as RichTextBox;
                        int cp = t.SelectionStart;
                        t.Text = t.Text.Insert(cp, Template.LINEBREAK);
                    }
                    else if (cms.SourceControl is TextBox)
                    {
                        TextBox t = (TextBox)cms.SourceControl as TextBox;
                        int cp = t.SelectionStart;
                        t.Text = t.Text.Insert(cp, Template.LINEBREAK);
                    }
                    
                }
            }
        }

    }
}
