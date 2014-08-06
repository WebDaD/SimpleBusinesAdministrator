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
using WebDaD.Toolkit.Helper;

namespace ManageAdministerExalt
{
    public partial class Items_Edit : Form
    {
        private Database db;
        private Item item;
        public Items_Edit(Database db,bool import)
        {
            this.db = db;
            this.item = new Item(this.db);
            
            InitializeComponent();
            setInitialMode(import);
            this.Text = Config.Name + " :: " + "Gegenstandsbearbeitung";
            loadItems();
        }
        public Items_Edit(Database db,bool import, Item item)
        {
            this.db = db;
            this.item = item;
            
            InitializeComponent();
            setInitialMode(import);
            this.Text = Config.Name + " :: " + "Gegenstandsbearbeitung";
            loadItems();
            foreach (object o in cb_items.Items)
            {
                if (o is ComboBoxItem)
                {
                    if (((ComboBoxItem)o).Value == item.ID)
                    {
                        cb_items.SelectedItem = o;
                    }
                }
            }
            cb_items.Enabled = false;
        }

        private void loadItems()
        {
            Dictionary<string, string> list = item.GetIDList();
            foreach (KeyValuePair<string,string> i in list)
            {
                cb_items.Items.Add(new ComboBoxItem(i.Key,i.Value));
            }
        }

        private void setInitialMode(bool import)
        {
               rb_import.Checked = import;
               rb_export.Checked=!import;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ComboBoxItem cb = (ComboBoxItem)cb_items.SelectedItem;
            if (item.ID == "")
            {
                item = new Item(db, cb.Value);
            }

            int c = (int)nu_count.Value;
            if (rb_export.Checked) c *= -1;
            if (item.AddEntry(c))
            {
                MessageBox.Show("Erfolgreich eingetragen!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Fehler beim Eintragen!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



    }
}
