using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ManageAdministerExalt.Classes;
using WebDaD.Toolkit.Export;
using WebDaD.Toolkit.Database;
using System.Diagnostics;

namespace ManageAdministerExalt
{
    public partial class Export : Form
    {
        private Exportable data;
        private Database db;
        public Export(Exportable data, Database db)
        {
            InitializeComponent();
            this.data = data;
            this.db=db;
            if (!this.db.isOpen()) db.Open();
            lb_data_head.Text = data.DataName();

            foreach (ExportType t in (ExportType[])Enum.GetValues(typeof(ExportType)))
            {
                cb_export_type.Items.Add(t.ToString());
            }

            cb_template.DataSource = new BindingSource(Template.getTemplates(db), null);
            cb_template.DisplayMember = "Value";
            cb_template.ValueMember = "Key";

            this.Text = Config.Name + " :: " + "Export";
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            if (cb_export_type.SelectedIndex == -1) MessageBox.Show("Exporttyp wählen", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);

            ExportType et = (ExportType)Enum.Parse(typeof(ExportType), cb_export_type.SelectedItem.ToString());
            Template t = new Template(db,cb_template.SelectedValue.ToString());

            string file = WebDaD.Toolkit.Export.Export.DataExport(et, data, t, Config.BasePath);

            if (cb_open_export.Checked)
            {
                Process.Start(file);
            }
            //TODO: Checkbox Mail
        }
    }
}
