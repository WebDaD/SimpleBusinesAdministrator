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
        private ExportCount ec;
        private List<ExportType> supportedExportTypes;
        public Export(Exportable data, Database db, ExportCount ec)
        {
            InitializeComponent();
            this.data = data;
            this.db=db;
            this.ec = ec;
            if (!this.db.isOpen()) db.Open();
            lb_data_head.Text = data.DataName(ec);

            ExportType[] t =  (ExportType[])Enum.GetValues(typeof(ExportType));
            supportedExportTypes = t.ToList<ExportType>();
            foreach (ExportType not in WebDaD.Toolkit.Export.Export.NotYetSupported)
            {
                supportedExportTypes.Remove(not);
            }


            foreach (ExportType ex in supportedExportTypes)
            {
                cb_export_type.Items.Add(ex.ToString());
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
            Template t;
            if (cb_template.SelectedValue.ToString().Equals("0"))
            {
                t = new Template();
            }
            else
            {
                t = new Template(db, cb_template.SelectedValue.ToString());
            }

            string file = WebDaD.Toolkit.Export.Export.DataExport(et, data, t, Config.BasePath, ec, Config.WKHTMLTOPDFPath);

            if (cb_open_export.Checked)
            {
                Process.Start(file);
            }
            if (cb_Mail.Checked)
            {
                try
                {
                    Process.Start("mailto: ?subject=" + data.DataName(ec) + "&attachment=" + file);
                }
                catch (Exception)
                {
                    MessageBox.Show("Konnte Mail-Programm nicht starten", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
