using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebDaD.Toolkit.Database;
using ManageAdministerExalt.Classes;
using System.Diagnostics;
using System.IO;
using WebDaD.Toolkit.Export;

namespace ManageAdministerExalt
{
    public partial class Main : Form
    {
        private Database db;
        private Customer customer;
        private Dictionary<string, string> customers;
        private Service service;
        private Dictionary<string, string> services;
        private Term term;
        private Dictionary<string, string> terms;
        private Expense expense;
        private Dictionary<string, string> expenses;
        public Main()
        {
            InitializeComponent();
            switch (Config.DatabaseType)
            {
                case DatabaseType.SQLite:
                    db = Database_SQLite.getDatabase(Config.DatabaseConnectionString);
                    break;
                default:
                    //TODO: ERROR
                    break;
            }
            if (db == null)
            {
                //TODO: ERROR
            }
            db.Open();
            fillFilters();
            fillLists();
            this.Text = Config.Name + " :: " + "Hauptansicht";

            checkOpenPoints();

            tabs.SelectedTab = tabs.TabPages[Config.DefaultTab];

            foreach (TabControl t  in tabs.TabPages)
            {
                ((Control)t).Enabled = false;
            }
            foreach (string active_tab in Config.ActiveTabs)
            {
                ((Control)tabs.TabPages[active_tab]).Enabled = true;
            }

        }

        private void checkOpenPoints()
        {
            //TODO: check for open bills, reminders with target date now
        }

        private void fillFilters()
        {
            //Expenses :: Year
            cb_ex_year.Items.Add("Alle");
            List<string> years = Expense.GetExpenseYears(db);
            if (years != null)
            {
                foreach (string year in years)
                {
                    cb_ex_year.Items.Add(year);
                }
            }
            cb_ex_year.SelectedItem = cb_ex_year.Items[0];

            //Jobs
            
        }

        private void fillLists()
        {
            fillCustomers();
            fillServices();
            fillTerms();
            fillExpenses(cb_ex_year.SelectedItem.ToString());
            //TODO: fill Jobs
        }

        private void fillExpenses(string year)
        {
            expenses = Expense.getExpenses(db,year);
            if (expenses != null)
            {
                lv_ex_expenses.Items.Clear();
                foreach (KeyValuePair<string, string> item in expenses)
                {
                    ListViewItem t = new ListViewItem(item.Key);
                    string[] tmp = item.Value.Split('|');
                    t.SubItems.Add(tmp[0]);
                    t.SubItems.Add(tmp[1]);
                    lv_ex_expenses.Items.Add(t);
                }
            }
        }

        private void fillCustomers()
        {
            customers = new Customer(db).getIDList();
            if (customers != null)
            {
                lv_cu_customers.Items.Clear();
                foreach (KeyValuePair<string, string> item in customers)
                {
                    ListViewItem t = new ListViewItem(item.Key);
                    t.SubItems.Add(item.Value);
                    lv_cu_customers.Items.Add(t);
                }
            }
        }

        private void fillServices()
        {
            services = Service.getServices(db);
            if (services != null)
            {
                lv_se_services.Items.Clear();
                foreach (KeyValuePair<string, string> item in services)
                {
                    ListViewItem t = new ListViewItem(item.Key);
                    t.SubItems.Add(item.Value);
                    lv_se_services.Items.Add(t);
                }
            }
        }

        private void fillTerms()
        {
            terms = Term.getTerms(db);
            if (terms != null)
            {
                lv_tc_terms.Items.Clear();
                foreach (KeyValuePair<string, string> item in terms)
                {
                    ListViewItem t = new ListViewItem(item.Key);
                    t.SubItems.Add(item.Value);
                    lv_tc_terms.Items.Add(t);
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            sizeLastColumn(lv_cu_customers);
            sizeLastColumn(lv_se_services);
            sizeLastColumn(lv_tc_terms);
            sizeLastColumn(lv_ex_expenses);
            sizeLastColumn(lv_re_reports);
            sizeLastColumn(lv_jo_jobs);
        }
        private void Main_Resize(object sender, EventArgs e)
        {
            sizeLastColumn(lv_cu_customers);
            sizeLastColumn(lv_se_services);
            sizeLastColumn(lv_tc_terms);
            sizeLastColumn(lv_ex_expenses);
            sizeLastColumn(lv_re_reports);
            sizeLastColumn(lv_jo_jobs);

        }

        private void cms_cu_customers_Opening(object sender, CancelEventArgs e)
        {
            löschenToolStripMenuItem.Enabled = lv_cu_customers.SelectedIndices.Count > 0;
            neuerAuftragToolStripMenuItem.Enabled = lv_cu_customers.SelectedIndices.Count > 0;
        }

        private void cms_se_services_Opening(object sender, CancelEventArgs e)
        {
            se_löschenToolStripMenuItem1.Enabled = lv_se_services.SelectedIndices.Count > 0;
        }

        /// <summary>
        /// cms_cu_customers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lv_cu_customers.SelectedIndices.Clear();
            lb_cu_id.Text = "";
            tb_cu_name.Text = "";
            tb_cu_street.Text = "";
            tb_cu_plz.Text = "";
            tb_cu_city.Text = "";
            tb_cu_mail.Text = "";
            tb_cu_phone.Text = "";
            tb_cu_mobile.Text = "";
            tb_cu_fax.Text = "";
            tb_cu_contact.Text = "";
            customer = new Customer(db);
            btn_cu_save.Enabled = true;
            tb_cu_name.Focus();
        }

        /// <summary>
        /// cms_cu_customers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Wollen Sie wirklick den Kunden "+customer.KundenNummer+" ("+customer.Name+") löschen?", "Bestätigung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                customer.Delete();
                fillCustomers();
                lv_cu_customers.SelectedIndices.Clear();
            }
        }

        /// <summary>
        /// cms_cu_customers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuerAuftragToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_cu_new_job_Click(sender, e);
        }

        private void btn_cu_save_Click(object sender, EventArgs e)
        {
            customer.Name = tb_cu_name.Text;
            customer.Street = tb_cu_street.Text;
            customer.PLZ = tb_cu_plz.Text;
            customer.City = tb_cu_city.Text;
            customer.Mail = tb_cu_mail.Text;
            customer.Phone = tb_cu_phone.Text;
            customer.Mobile = tb_cu_mobile.Text;
            customer.Fax = tb_cu_fax.Text;
            customer.Contact = tb_cu_contact.Text;
            if (!customer.Save())
            {
                MessageBox.Show("Konnte nicht speichern...", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Kunde " + customer.Name + " gespeichert.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            btn_cu_cancel.Enabled = false;
            btn_cu_save.Enabled = false;
            int sele = -1;
            if (lv_cu_customers.SelectedIndices.Count > 0)
            {
                sele = lv_cu_customers.SelectedIndices[0];
            }
            fillCustomers();
            if (sele > 0)
            {
                lv_cu_customers.Items[sele].Selected = true;
            }
        }

        private void btn_cu_cancel_Click(object sender, EventArgs e)
        {
            tb_cu_name.Text = customer.Name;
            tb_cu_street.Text = customer.Street;
            tb_cu_plz.Text = customer.PLZ;
            tb_cu_city.Text = customer.City;
            tb_cu_mail.Text = customer.Mail;
            tb_cu_phone.Text = customer.Phone;
            tb_cu_mobile.Text = customer.Mobile;
            tb_cu_fax.Text = customer.Fax;
            tb_cu_contact.Text = customer.Contact;
            btn_cu_cancel.Enabled = false;
            btn_cu_save.Enabled = false;
        }

        private void lv_cu_customers_Resize(object sender, EventArgs e)
        {
            sizeLastColumn(lv_cu_customers);
        }

        private void sizeLastColumn(ListView lv)
        {
            lv.Columns[lv.Columns.Count - 1].Width = -2;
        }

        private void lv_cu_customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_cu_customers.SelectedIndices.Count > 0)
            {
                customer = new Customer(db, lv_cu_customers.SelectedItems[0].Text);

                lb_cu_id.Text = customer.NiceID;
                tb_cu_name.Text = customer.Name;
                tb_cu_street.Text = customer.Street;
                tb_cu_plz.Text = customer.PLZ;
                tb_cu_city.Text = customer.City;
                tb_cu_mail.Text = customer.Mail;
                tb_cu_phone.Text = customer.Phone;
                tb_cu_mobile.Text = customer.Mobile;
                tb_cu_fax.Text = customer.Fax;
                tb_cu_contact.Text = customer.Contact;

                //TODO load report

                btn_cu_cancel.Enabled = false;
                btn_cu_save.Enabled = false;

                btn_cu_new_job.Enabled = true;
                btn_cu_open_folder.Enabled = true;
                btn_cu_export.Enabled = true;
            }
            else
            {
                btn_cu_new_job.Enabled = false;
                btn_cu_open_folder.Enabled = false;
                btn_cu_export.Enabled = false;
            }
        }

        private void tb_cu_TextChanged(object sender, EventArgs e)
        {
            btn_cu_cancel.Enabled = true;
            btn_cu_save.Enabled = true;
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Option: really?
            Application.Exit();
        }

        private void btn_cu_new_job_Click(object sender, EventArgs e)
        {
            //TODO: Create neu Job
        }

        private void btn_cu_open_folder_Click(object sender, EventArgs e)
        {
            Process.Start(Config.BasePath + Path.DirectorySeparatorChar + "customers" + Path.DirectorySeparatorChar + customer.NiceID);
        }

        private void btn_cu_export_Click(object sender, EventArgs e)
        {
            new Export(customer,db, ExportCount.SINGLE).ShowDialog();
        }

        private void btn_cu_export_all_Click(object sender, EventArgs e)
        {

            new Export(customer, db, ExportCount.MULTI).ShowDialog();
        }

        private void se_neuToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            lv_se_services.SelectedIndices.Clear();
            lb_se_ID.Text = "";
            tb_se_name.Text = "";
            tb_se_description.Text = "";
            tb_se_unit.Text = "";
            tb_se_price.Text = "";
            service = new Service(db);
            btn_se_save.Enabled = true;
            tb_se_name.Focus();
        }

        private void se_löschenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Wollen Sie wirklick die Leistung " + service.ServiceNummer + " (" + service.Name + ") löschen?", "Bestätigung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                service.Delete();
                fillServices();
                lv_se_services.SelectedIndices.Clear();
            }
        }

        private void lv_se_services_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_se_services.SelectedIndices.Count > 0)
            {
                service = new Service(db, lv_se_services.SelectedItems[0].Text);

                lb_se_ID.Text = service.ServiceNummer;
                tb_se_name.Text = service.Name;
                tb_se_description.Text = service.Description;
                tb_se_unit.Text = service.Unit;
                tb_se_price.Text = service.Price;

                //TODO load report

                btn_se_cancel.Enabled = false;
                btn_se_save.Enabled = false;

                btn_se_export.Enabled = true;
            }
            else
            {
                btn_se_export_all.Enabled = true;
            }
        }

        private void tb_se_TextChanged(object sender, EventArgs e)
        {
            btn_se_cancel.Enabled = true;
            btn_se_save.Enabled = true;
        }

        private void btn_se_cancel_Click(object sender, EventArgs e)
        {
            tb_se_name.Text = service.Name;
            tb_se_description.Text = service.Description;
            tb_se_unit.Text = service.Unit;
            tb_se_price.Text = service.Price;
            btn_se_cancel.Enabled = false;
            btn_se_save.Enabled = false;
        }

        private void btn_se_save_Click(object sender, EventArgs e)
        {
            service.Name = tb_se_name.Text;
            service.Description = tb_se_description.Text;
            service.Unit = tb_se_unit.Text;
            service.Price = tb_se_price.Text;
            if (!service.Save())
            {
                MessageBox.Show("Konnte nicht speichern...", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Leistung " + service.Name + " gespeichert.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            btn_se_cancel.Enabled = false;
            btn_se_save.Enabled = false;
            int sele = -1;
            if (lv_se_services.SelectedIndices.Count > 0)
            {
                sele = lv_se_services.SelectedIndices[0];
            }
            fillServices();
            if (sele > 0)
            {
                lv_se_services.Items[sele].Selected = true;
            }
        }

        private void btn_se_export_Click(object sender, EventArgs e)
        {
            new Export(service,db).ShowDialog();
        }

        private void btn_se_export_all_Click(object sender, EventArgs e)
        {
            new Export(new Services(db),db).ShowDialog();
        }

        private void lv_se_services_Resize(object sender, EventArgs e)
        {
            sizeLastColumn(lv_se_services);
        }

        private void btn_tc_export_Click(object sender, EventArgs e)
        {
            new Export(new Terms(db), db).ShowDialog();
        }

        private void btn_tc_save_Click(object sender, EventArgs e)
        {
            term.Title = tb_tc_title.Text;
            term.Content = tb_tc_content.Text;
            if (!term.Save())
            {
                MessageBox.Show("Konnte nicht speichern...", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("AGB " + term.Title + " gespeichert.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            btn_tc_cancel.Enabled = false;
            btn_tc_save.Enabled = false;
            int sele = -1;
            if (lv_tc_terms.SelectedIndices.Count > 0)
            {
                sele = lv_tc_terms.SelectedIndices[0];
            }
            fillTerms();
            if (sele > 0)
            {
                lv_tc_terms.Items[sele].Selected = true;
            }
        }

        private void btn_tc_cancel_Click(object sender, EventArgs e)
        {
            tb_tc_title.Text = term.Title;
            tb_tc_content.Text = term.Content;
            btn_tc_cancel.Enabled = false;
            btn_tc_save.Enabled = false;
        }

        private void lv_tc_terms_Resize(object sender, EventArgs e)
        {
            sizeLastColumn(lv_tc_terms);
        }

        private void lv_tc_terms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_tc_terms.SelectedIndices.Count > 0)
            {
                term = new Term(db, lv_tc_terms.SelectedItems[0].Text);

                lb_tc_id.Text = term.NiceID;
                tb_tc_title.Text = term.Title;
                tb_tc_content.Text = term.Content;

                btn_tc_cancel.Enabled = false;
                btn_tc_save.Enabled = false;

                btn_tc_export.Enabled = true;
            }
        }

        private void tc_TextChanged(object sender, EventArgs e)
        {
            btn_tc_cancel.Enabled = true;
            btn_tc_save.Enabled = true;
        }

        /// <summary>
        /// tc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            lv_tc_terms.SelectedIndices.Clear();
            lb_tc_id.Text = "";
            tb_tc_title.Text = "";
            tb_tc_content.Text = "";
            term = new Term(db);
            btn_tc_save.Enabled = true;
            tb_tc_title.Focus();
        }

        /// <summary>
        /// tc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void löschenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Wollen Sie wirklick " + term.NiceID + " (" + term.Title + ") löschen?", "Bestätigung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                term.Delete();
                fillTerms();
                lv_tc_terms.SelectedIndices.Clear();
            }
        }

        /// <summary>
        /// tc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nachObenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            term.Move(Term.MOVE_UP);
            fillTerms();
            lb_tc_id.Text = term.NiceID;
        }

        /// <summary>
        /// tc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nachUntenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            term.Move(Term.MOVE_DOWN);
            fillTerms();
            lb_tc_id.Text = term.NiceID;
        }

        private void cms_tc_terms_Opening(object sender, CancelEventArgs e)
        {
            löschenToolStripMenuItem1.Enabled = lv_tc_terms.SelectedIndices.Count > 0;
            nachObenToolStripMenuItem.Enabled = lv_tc_terms.SelectedIndices.Count > 0;
            nachUntenToolStripMenuItem.Enabled = lv_tc_terms.SelectedIndices.Count > 0;
            if (lv_tc_terms.SelectedIndices.Count > 0)
            {
                if (term.Ordering == "1") nachObenToolStripMenuItem.Enabled = false;
                if (lv_tc_terms.SelectedIndices[0] == lv_tc_terms.Items.Count -1) nachUntenToolStripMenuItem.Enabled = false;
            }
        }

        private void optionenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Options().ShowDialog();
        }

        private void aktualisierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fillLists();
            checkOpenPoints();
        }

        private void rabatteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Discounts(db).ShowDialog();
        }

        private void mahnungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Reminders(db).ShowDialog();
        }

        private void zahlungsbedigungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PaymentConditions(db).ShowDialog();
        }

        private void tb_ex_attachment_Click(object sender, EventArgs e)
        {
            DialogResult dr = ofd_ex_attachement.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                tb_ex_attachment.Text = ofd_ex_attachement.FileName;
            }
        }

        private void btn_ex_cancel_Click(object sender, EventArgs e)
        {
            tb_ex_name.Text = expense.Name;
            dt_ex_edate.Value = expense.Date;
            nu_ex_value.Value = expense.Value;
            tb_ex_attachment.Text = expense.Attachment;
            btn_ex_cancel.Enabled = false;
            btn_ex_save.Enabled = false;
        }

        private void btn_ex_save_Click(object sender, EventArgs e)
        {
            expense.Name = tb_ex_name.Text;
            expense.Date = dt_ex_edate.Value;
            expense.Value = nu_ex_value.Value;
            expense.Attachment = tb_ex_attachment.Text;
            if (!expense.Save())
            {
                MessageBox.Show("Konnte nicht speichern...", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Ausgabe " + expense.Name + " gespeichert.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            btn_ex_cancel.Enabled = false;
            btn_ex_save.Enabled = false;
            int sele = -1;
            if (lv_ex_expenses.SelectedIndices.Count > 0)
            {
                sele = lv_ex_expenses.SelectedIndices[0];
            }
            fillExpenses(cb_ex_year.SelectedItem.ToString());
            if (sele > 0)
            {
                lv_ex_expenses.Items[sele].Selected = true;
            }
        }

        private void tb_ex_TextChanged(object sender, EventArgs e)
        {
            btn_ex_cancel.Enabled = true;
            btn_ex_save.Enabled = true;
        }

        private void cb_ex_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillExpenses(cb_ex_year.SelectedItem.ToString());
        }

        private void lv_ex_expenses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_ex_expenses.SelectedIndices.Count > 0)
            {
                expense = new Expense(db, lv_ex_expenses.SelectedItems[0].Text);

                lb_ex_id.Text = expense.NiceID;
                tb_ex_name.Text = expense.Name;
                dt_ex_edate.Value = expense.Date;
                nu_ex_value.Value = expense.Value;
                tb_ex_attachment.Text = expense.Attachment;

                btn_ex_cancel.Enabled = false;
                btn_ex_save.Enabled = false;

                btn_ex_export.Enabled = true;
                btn_ex_open_attachment.Enabled = true;
            }
        }

        private void cms_ex_expenses_Opening(object sender, CancelEventArgs e)
        {
            löschenToolStripMenuItem2.Enabled = lv_ex_expenses.SelectedIndices.Count > 0;
        }

        private void neuToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //expenses
            lv_ex_expenses.SelectedIndices.Clear();
            lb_ex_id.Text = "";
            tb_ex_name.Text = "";
            dt_ex_edate.Value = DateTime.Now;
            nu_ex_value.Value = 0;
            tb_ex_attachment.Text = "";

            expense = new Expense(db);
            btn_ex_save.Enabled = true;
            tb_ex_name.Focus();
        }

        private void löschenToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //expenses
            if (MessageBox.Show("Wollen Sie wirklick " + expense.NiceID + " (" + expense.Name + ") löschen?", "Bestätigung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                expense.Delete();
                fillExpenses(cb_ex_year.SelectedItem.ToString());
                lv_ex_expenses.SelectedIndices.Clear();
            }
        }

        private void btn_ex_open_attachment_Click(object sender, EventArgs e)
        {
            string file = Config.BasePath + Path.DirectorySeparatorChar + "expenses" + Path.DirectorySeparatorChar + expense.NiceID + Path.DirectorySeparatorChar + expense.Attachment;
            Process.Start(file);
        }

        private void btn_ex_export_Click(object sender, EventArgs e)
        {
            new Export(expense, db).ShowDialog();
        }

        private void btn_ex_export_all_Click(object sender, EventArgs e)
        {
            new Export(new Expenses(db,cb_ex_year.SelectedItem.ToString()), db).ShowDialog();
        }

        private void lv_jo_jobs_Resize(object sender, EventArgs e)
        {
            sizeLastColumn(lv_jo_jobs);
        }

        private void lv_ex_expenses_Resize(object sender, EventArgs e)
        {
            sizeLastColumn(lv_ex_expenses);
        }

        private void lv_re_reports_Resize(object sender, EventArgs e)
        {
            sizeLastColumn(lv_re_reports);
        }

        private void cb_jo_filter_years_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cb_jo_filter_months_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cb_jo_filter_customers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lv_jo_jobs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dt_jo_jdate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_jo_services_edit_Click(object sender, EventArgs e)
        {

        }

        private void btn_jo_discounts_edit_Click(object sender, EventArgs e)
        {

        }

        private void btn_jo_edit_items_Click(object sender, EventArgs e)
        {

        }


        private void btn_jo_cancel_Click(object sender, EventArgs e)
        {

        }

        private void btn_jo_save_Click(object sender, EventArgs e)
        {

        }

        private void btn_jo_export_job_Click(object sender, EventArgs e)
        {

        }

        private void btn_jo_jsent_Click(object sender, EventArgs e)
        {

        }

        private void btn_jo_status_ok_Click(object sender, EventArgs e)
        {

        }

        private void btn_jo_export_bill_Click(object sender, EventArgs e)
        {

        }

        private void btn_jo_bill_send_Click(object sender, EventArgs e)
        {

        }

        private void btn_jo_bill_ok_Click(object sender, EventArgs e)
        {

        }

        private void cb_jo_reminders_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_jo_reminders_new_Click(object sender, EventArgs e)
        {

        }

        private void btn_jo_reminder_sent_Click(object sender, EventArgs e)
        {

        }

        private void btn_jo_reminder_ok_Click(object sender, EventArgs e)
        {

        }

        private void cmd_jo_jobs_Opening(object sender, CancelEventArgs e)
        {

        }

        private void neuToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //jobs
        }

        private void löschenToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //jobs
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.Close();
        }

        
        

        

       
    }
}
