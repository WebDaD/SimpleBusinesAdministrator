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
using WebDaD.Toolkit.Helper;
using ManageAdministerExalt.Classes.Reports;

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
        private Worker worker;
        private Dictionary<string, string> workers;
        private Item item;
        private Dictionary<string, string> items;
        private Job job;
        private Dictionary<string, string> jobs;
        private Report report;
        private List<Report> reports;

        private bool editmode;
        private bool filterstart = true;

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

            List<TabPage> remove = new List<TabPage>();
            foreach (TabPage tp in tabs.TabPages)
            {
                if (!Config.ActiveTabs.Contains(tp.Name))
                {
                    remove.Add(tp);
                }
            }
            foreach (TabPage t in remove)
            {
                tabs.TabPages.Remove(t);
            }

            editmode = false;

            if (Config.Timer)
            {
                timer.Interval = Config.TimerInterval * 60 * 1000;
                timer.Enabled = true;
                timer.Start();
            }
            else { timer.Enabled = false; }
        }

        private void setEditmode(bool edit)
        {
            this.editmode = edit;
            btn_cu_cancel.Enabled = edit;
            btn_cu_save.Enabled = edit;
            btn_se_cancel.Enabled = edit;
            btn_se_save.Enabled = edit;
            btn_tc_cancel.Enabled = edit;
            btn_tc_save.Enabled = edit;
            btn_ex_cancel.Enabled = edit;
            btn_ex_save.Enabled = edit;
            btn_jo_cancel.Enabled = edit;
            btn_jo_save.Enabled = edit;
            btn_wo_cancel.Enabled = edit;
            btn_wo_save.Enabled = edit;
            btn_it_cancel.Enabled = edit;
            btn_it_save.Enabled = edit;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!editmode)
            {
                fillFilters();
                fillLists();

            }
            //TODO: backup
            checkOpenPoints();
        }

        private void tabs_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (this.editmode) e.Cancel = true;
        }

        private void checkOpenPoints()
        {
            //TODO: check for open bills, reminders with target date now
        }

        private void fillFilters()
        {
            //Expenses :: Year
            cb_ex_year.Items.Add("Alle");
            List<string> years = new Expense(db).GetExpenseYears();
            if (years != null)
            {
                foreach (string year in years)
                {
                    cb_ex_year.Items.Add(year);
                }
            }
            cb_ex_year.SelectedItem = cb_ex_year.Items[0];

            //Jobs :: Years, Months, Customers
            cb_jo_filter_years.Items.Add("Alle");
            List<string> jo_years = new Job(db).GetYears();
            if (jo_years != null)
            {
                foreach (string year in jo_years)
                {
                    cb_ex_year.Items.Add(year);
                }
            }


            cb_jo_filter_months.Items.Add("Alle");
            List<string> jo_months = new Job(db).GetMonths();
            if (jo_months != null)
            {
                foreach (string month in jo_months)
                {
                    cb_jo_filter_months.Items.Add(month);
                }
            }


            cb_jo_filter_customers.Items.Add(new ComboBoxItem("0", "Alle"));
            List<ComboBoxItem> jo_customers = new Job(db).GetCustomers();
            if (jo_customers != null)
            {
                foreach (ComboBoxItem customer in jo_customers)
                {
                    cb_jo_filter_customers.Items.Add(customer);
                }
            }
            filterstart = true;
            cb_jo_filter_years.SelectedItem = cb_jo_filter_years.Items[0];
            cb_jo_filter_customers.SelectedItem = cb_jo_filter_customers.Items[0];
            cb_jo_filter_months.SelectedItem = cb_jo_filter_months.Items[0];
            filterstart = false;

            //TODO Reports (take jobs!)
        }

        private void fillLists()
        {
            fillCustomers();
            fillServices();
            fillTerms();
            fillExpenses(cb_ex_year.SelectedItem.ToString());
            fillJobs(false);
            fillWorkers();
            fillItems();
            fillReports();
        }
        private void fillReports()
        {
            reports = new Surplus(db).Reports;
            if (reports.Count > 0)
            {
                lv_re_reports.Items.Clear();
                foreach (Report item in reports)
                {
                    ListViewItem t = new ListViewItem(item.Text);
                    lv_re_reports.Items.Add(t);
                }
            }

        }

        private void fillItems()
        {
            items = new Item(db).GetIDList();
            if (items != null)
            {
                lv_it_items.Items.Clear();
                foreach (KeyValuePair<string, string> item in items)
                {
                    ListViewItem t = new ListViewItem(item.Key);
                    t.SubItems.Add(item.Value);
                    lv_it_items.Items.Add(t);
                }
                btn_it_export.Enabled = true;
                btn_it_import.Enabled = true;
            }
        }

        private void fillJobs(bool filtered)
        {
            if (filtered)
            {
                jobs = job.GetFilteredIDList(cb_jo_filter_years.SelectedItem.ToString(), cb_jo_filter_months.SelectedItem.ToString(), ((ComboBoxItem)cb_jo_filter_customers.SelectedItem).Value);
            }
            else
            {
                jobs = new Job(db).GetIDList();
            }
            if (jobs != null)
            {
                lv_jo_jobs.Items.Clear();
                foreach (KeyValuePair<string, string> item in jobs)
                {
                    ListViewItem t = new ListViewItem(item.Key);
                    string[] tmp = item.Value.Split('|');
                    t.SubItems.Add(tmp[0]);
                    t.SubItems.Add(tmp[1]);
                    lv_jo_jobs.Items.Add(t);
                }
            }

            cb_jo_worker.Items.Clear();
            List<ComboBoxItem> jo_workers = new Job(db).GetWorkers();
            if (jo_workers != null)
            {
                foreach (ComboBoxItem worker in jo_workers)
                {
                    cb_jo_worker.Items.Add(worker);
                }
            }
            cb_jo_worker.SelectedIndex = -1;
        }
        private void fillWorkers()
        {
            workers = new Worker(db).GetIDList();
            if (workers != null)
            {
                lv_wo_worker.Items.Clear();
                foreach (KeyValuePair<string, string> item in workers)
                {
                    ListViewItem t = new ListViewItem(item.Key);
                    t.SubItems.Add(item.Value);
                    lv_wo_worker.Items.Add(t);
                }
            }
        }

        private void fillExpenses(string year)
        {
            expenses = new Expense(db).GetIDList(year);
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
            customers = new Customer(db).GetIDList();
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
            services = new Service(db).GetIDList();
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
            terms = new Term(db).GetIDList();
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

        private void sizeLVs()
        {
            sizeLastColumn(lv_cu_customers);
            sizeLastColumn(lv_se_services);
            sizeLastColumn(lv_tc_terms);
            sizeLastColumn(lv_ex_expenses);
            sizeLastColumn(lv_re_reports);
            sizeLastColumn(lv_jo_jobs);
            sizeLastColumn(lv_wo_worker);
            sizeLastColumn(lv_it_items);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            sizeLVs();
        }
        private void Main_Resize(object sender, EventArgs e)
        {
            sizeLVs();
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
            if (MessageBox.Show("Wollen Sie wirklick den Kunden " + customer.NiceID + " (" + customer.Name + ") löschen?", "Bestätigung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            setEditmode(false);
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
            setEditmode(false);
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

                setEditmode(false);

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
            setEditmode(true);
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editmode)
            {
                if (MessageBox.Show("Sie bearbeiten gerade einen Datensatz.\nWollen Sie wirklick beenden?", "Frage", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            if (Config.AskForExit)
            {

                if (MessageBox.Show("Wollen Sie wirklick beenden?", "Frage", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }

        private void btn_cu_new_job_Click(object sender, EventArgs e)
        {
            if (lv_cu_customers.SelectedIndices.Count > 0)
            {
                lb_jo_id.Text = "";
                tb_jo_name.Text = "";
                dt_jo_jdate.Value = DateTime.Now;
                lb_jo_services_sum.Text = "";
                lb_jo_discounts_sum.Text = "";
                lb_jo_items.Text = "";
                cb_jo_worker.SelectedIndex = -1;
                lb_jo_adress.Text = "";

                job = new Job(db);
                job.setCustomer(lv_cu_customers.SelectedItems[0].Text);
                lb_jo_customer_id.Text = job.Customer_ID;
                btn_jo_save.Enabled = true;
                tabs.SelectedIndex = tabs.TabPages.IndexOf(tabs.TabPages["tab_jobs"]);

                tb_jo_name.Focus();

                btn_jo_discounts_edit.Enabled = false;
                btn_jo_edit_address.Enabled = false;
                btn_jo_edit_items.Enabled = false;
                btn_jo_services_edit.Enabled = false;
            }
        }

        private void btn_cu_open_folder_Click(object sender, EventArgs e)
        {
            Process.Start(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["customers"] + Path.DirectorySeparatorChar + customer.NiceID);
        }

        private void btn_cu_export_Click(object sender, EventArgs e)
        {
            new Export(customer, db, ExportCount.SINGLE).ShowDialog();
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
            nu_se_value.Value = 0;
            service = new Service(db);
            btn_se_save.Enabled = true;
            tb_se_name.Focus();
        }

        private void se_löschenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Wollen Sie wirklick die Leistung " + service.NiceID + " (" + service.Name + ") löschen?", "Bestätigung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

                lb_se_ID.Text = service.NiceID;
                tb_se_name.Text = service.Name;
                tb_se_description.Text = service.Description;
                tb_se_unit.Text = service.Unit;
                nu_se_value.Value = service.Value;

                //TODO load report

                setEditmode(false);
                btn_se_export.Enabled = true;
            }
            else
            {
                btn_se_export_all.Enabled = true;
            }
        }

        private void tb_se_TextChanged(object sender, EventArgs e)
        {
            setEditmode(true);
        }

        private void btn_se_cancel_Click(object sender, EventArgs e)
        {
            tb_se_name.Text = service.Name;
            tb_se_description.Text = service.Description;
            tb_se_unit.Text = service.Unit;
            nu_se_value.Value = service.Value;
            setEditmode(false);
        }

        private void btn_se_save_Click(object sender, EventArgs e)
        {
            service.Name = tb_se_name.Text;
            service.Description = tb_se_description.Text;
            service.Unit = tb_se_unit.Text;
            service.Value = nu_se_value.Value;
            if (!service.Save())
            {
                MessageBox.Show("Konnte nicht speichern...", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Leistung " + service.Name + " gespeichert.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            setEditmode(false);
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
            new Export(service, db, ExportCount.SINGLE).ShowDialog();
        }

        private void btn_se_export_all_Click(object sender, EventArgs e)
        {
            new Export(service, db, ExportCount.MULTI).ShowDialog();
        }

        private void lv_se_services_Resize(object sender, EventArgs e)
        {
            sizeLastColumn(lv_se_services);
        }

        private void btn_tc_export_Click(object sender, EventArgs e)
        {
            new Export(term, db, ExportCount.MULTI).ShowDialog();
        }

        private void btn_tc_save_Click(object sender, EventArgs e)
        {
            term.Name = tb_tc_title.Text;
            term.Description = tb_tc_content.Text;
            if (!term.Save())
            {
                MessageBox.Show("Konnte nicht speichern...", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("AGB " + term.Name + " gespeichert.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            setEditmode(false);
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
            tb_tc_title.Text = term.Name;
            tb_tc_content.Text = term.Description;
            setEditmode(false);
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
                tb_tc_title.Text = term.Name;
                tb_tc_content.Text = term.Description;

                setEditmode(false);

                btn_tc_export.Enabled = true;
            }
        }

        private void tc_TextChanged(object sender, EventArgs e)
        {
            setEditmode(true);
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
            if (MessageBox.Show("Wollen Sie wirklick " + term.NiceID + " (" + term.Name + ") löschen?", "Bestätigung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                if (term.Ordering == 1) nachObenToolStripMenuItem.Enabled = false;
                if (lv_tc_terms.SelectedIndices[0] == lv_tc_terms.Items.Count - 1) nachUntenToolStripMenuItem.Enabled = false;
            }
        }

        private void optionenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Options().ShowDialog();
        }

        private void aktualisierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fillLists();
            fillFilters();
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
            new Payment_Conditions(db).ShowDialog();
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
            dt_ex_edate.Value = expense.EDate;
            nu_ex_value.Value = expense.Value;
            tb_ex_attachment.Text = expense.Attachment;
            setEditmode(false);
        }

        private void btn_ex_save_Click(object sender, EventArgs e)
        {
            expense.Name = tb_ex_name.Text;
            expense.EDate = dt_ex_edate.Value;
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
            setEditmode(false);
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
            setEditmode(true);
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
                dt_ex_edate.Value = expense.EDate;
                nu_ex_value.Value = expense.Value;
                tb_ex_attachment.Text = expense.Attachment;

                setEditmode(false);

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
            string file = Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["expenses"] + Path.DirectorySeparatorChar + expense.NiceID + Path.DirectorySeparatorChar + expense.Attachment;
            Process.Start(file);
        }

        private void btn_ex_export_Click(object sender, EventArgs e)
        {
            new Export(expense, db, ExportCount.SINGLE).ShowDialog();
        }

        private void btn_ex_export_all_Click(object sender, EventArgs e)
        {
            new Export(worker, db, ExportCount.MULTI).ShowDialog();
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
            if (!filterstart) fillJobs(true);
        }

        private void cb_jo_filter_months_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!filterstart) fillJobs(true);
        }

        private void cb_jo_filter_customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!filterstart) fillJobs(true);
        }

        private void dt_jo_jdate_ValueChanged(object sender, EventArgs e)
        {
            if (tabs.SelectedTab == tabs.TabPages["tab_jobs"]) setEditmode(true);
        }

        private void cb_jo_worker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabs.SelectedTab == tabs.TabPages["tab_jobs"]) setEditmode(true);
        }

        private void tb_jo_name_TextChanged(object sender, EventArgs e)
        {
            if (tabs.SelectedTab == tabs.TabPages["tab_jobs"]) setEditmode(true);
        }

        private void btn_jo_services_edit_Click(object sender, EventArgs e)
        {
            job_edit_services j = new job_edit_services(job.Services, job.NiceID, this.db);
            if (j.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                job.Services = j.Services;
                lb_jo_services_sum.Text = job.ServiceCount;
                j.Dispose();
                setEditmode(true);
            }
        }

        private void btn_jo_discounts_edit_Click(object sender, EventArgs e)
        {
            new job_edit_discounts(job).ShowDialog();
        }

        private void btn_jo_edit_items_Click(object sender, EventArgs e)
        {
            new job_edit_items(job).ShowDialog();
        }
        private void btn_jo_edit_address_Click(object sender, EventArgs e)
        {
            new job_edit_address(job).ShowDialog();
        }

        private void lv_jo_jobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_jo_jobs.SelectedIndices.Count > 0)
            {
                job = new Job(db, lv_jo_jobs.SelectedItems[0].Text);


                lb_jo_id.Text = job.NiceID;
                lb_jo_customer_id.Text = job.Customer_ID;
                tb_jo_name.Text = job.Name;
                dt_jo_jdate.Value = job.Offer_Created;
                lb_jo_services_sum.Text = job.ServiceCount;
                lb_jo_discounts_sum.Text = job.DiscountCount;
                lb_jo_items.Text = job.ItemCount;
                foreach (object o in cb_jo_worker.Items)
                {
                    if (o is ComboBoxItem)
                    {
                        if (((ComboBoxItem)o).Value == job.Worker.ID)
                        {
                            cb_jo_worker.SelectedItem = o;
                        }
                    }
                }
                lb_jo_adress.Text = job.Address_Text;

                //TODO: report

                setEditmode(false);

                btn_jo_openFolder.Enabled = true;
                btn_jo_next.Enabled = true;
                btn_jo_actions.Enabled = true;
                //TODO: Load Text for Btn_Next
                //TODO: Load Text fot btn_Action (and make it visible if needed!)
                btn_jo_discounts_edit.Enabled = true;
                btn_jo_edit_address.Enabled = true;
                btn_jo_edit_items.Enabled = true;
                btn_jo_services_edit.Enabled = true;
            }
        }

        private void btn_jo_cancel_Click(object sender, EventArgs e)
        {
            lb_jo_id.Text = job.NiceID;
            lb_jo_customer_id.Text = job.Customer_ID;
            tb_jo_name.Text = job.Name;
            dt_jo_jdate.Value = job.Offer_Created;
            lb_jo_services_sum.Text = job.ServiceCount;
            lb_jo_discounts_sum.Text = job.DiscountCount;
            lb_jo_items.Text = job.ItemCount;
            if (job.Worker.Name != "")
            {
                foreach (object o in cb_jo_worker.Items)
                {
                    if (o is ComboBoxItem)
                    {
                        if (((ComboBoxItem)o).Value == job.Worker.ID)
                        {
                            cb_jo_worker.SelectedItem = o;
                        }
                    }
                }
            }
            else cb_jo_worker.SelectedIndex = -1;
            lb_jo_adress.Text = job.Address_Text;
            setEditmode(false);
        }

        private void btn_jo_save_Click(object sender, EventArgs e)
        {
            job.Name = tb_jo_name.Text;
            job.Offer_Created = dt_jo_jdate.Value;
            job.setWorker(((ComboBoxItem)cb_jo_worker.SelectedItem).Value);
            if (!job.Save())
            {
                MessageBox.Show("Konnte nicht speichern...", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Auftrag " + job.Name + " gespeichert.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            setEditmode(false);
            int sele = -1;
            if (lv_ex_expenses.SelectedIndices.Count > 0)
            {
                sele = lv_jo_jobs.SelectedIndices[0];
            }
            fillJobs(true);
            if (sele > 0)
            {
                lv_jo_jobs.Items[sele].Selected = true;
            }
            btn_jo_discounts_edit.Enabled = true;
            btn_jo_edit_address.Enabled = true;
            btn_jo_edit_items.Enabled = true;
            btn_jo_services_edit.Enabled = true;
        }

        private void btn_jo_next_Click(object sender, EventArgs e)
        {
            //TODO: Next (filled by Status!)

        }

        private void btn_jo_actions_Click(object sender, EventArgs e)
        {
            //TODO: will have additional actions: export confirmation of order, add reminder... and will be activated on certain Stati
        }

        private void cmd_jo_jobs_Opening(object sender, CancelEventArgs e)
        {
            löschenToolStripMenuItem3.Enabled = lv_jo_jobs.SelectedIndices.Count > 0;
        }

        private void neuToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //jobs
            job_new_get_Customer j = new job_new_get_Customer();
            if (j.ShowDialog() == DialogResult.OK)
            {
                lb_jo_id.Text = "";
                tb_jo_name.Text = "";
                dt_jo_jdate.Value = DateTime.Now;
                lb_jo_services_sum.Text = "";
                lb_jo_discounts_sum.Text = "";
                lb_jo_items.Text = "";
                cb_jo_worker.SelectedIndex = -1;
                lb_jo_adress.Text = "";

                job = new Job(db);
                job.setCustomer(j.customer_id);
                lb_jo_customer_id.Text = job.Customer_ID;
                btn_jo_save.Enabled = true;
                tb_jo_name.Focus();

                btn_jo_discounts_edit.Enabled = false;
                btn_jo_edit_address.Enabled = false;
                btn_jo_edit_items.Enabled = false;
                btn_jo_services_edit.Enabled = false;
            }
        }

        private void löschenToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //jobs
            if (MessageBox.Show("Wollen Sie wirklick " + job.NiceID + " (" + job.Name + ") löschen?", "Bestätigung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                job.Delete();
                fillJobs(true);
                lv_jo_jobs.SelectedIndices.Clear();
            }
        }

        private void btn_jo_export_all_Click(object sender, EventArgs e)
        {
            new Export(job, db, ExportCount.MULTI).ShowDialog();
        }

        private void btn_jo_openFolder_Click(object sender, EventArgs e)
        {
            Process.Start(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["jobs"] + Path.DirectorySeparatorChar + job.NiceID);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.Close();
        }

        private void lv_wo_worker_Resize(object sender, EventArgs e)
        {
            sizeLastColumn(lv_wo_worker);
        }

        private void lv_wo_worker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_wo_worker.SelectedIndices.Count > 0)
            {
                worker = new Worker(db, lv_wo_worker.SelectedItems[0].Text);

                lb_wo_id.Text = worker.NiceID;
                tb_wo_name.Text = worker.Name;
                dt_wo_dayofbirth.Value = worker.DateOfBirth;
                dt_wo_workssince.Value = worker.WorksSince;
                nu_wo_salary.Value = worker.Salary;
                nu_wo_hoursperweek.Value = worker.HoursPerWeek;
                tb_wo_streetnr.Text = worker.StreetNr;
                tb_wo_plz.Text = worker.PLZ;
                tb_wo_city.Text = worker.City;
                tb_wo_phone.Text = worker.Phone;
                tb_wo_mail.Text = worker.EMail;
                tb_wo_mobile.Text = worker.Mobile;

                lb_wo_report_jobs.Text = worker.R_Jobs.ToString();
                lb_wo_report_job_avg.Text = worker.R_Jobs_Avg.ToString("C");
                lb_wo_report_job_sum.Text = worker.R_Jobs_Sum.ToString("C");
                lb_wo_report_work_months.Text = worker.R_Work_Months.ToString() +" "+ ((worker.R_Work_Months>1)?"Monaten":"Monat");
                lb_wo_reports_salary_sum.Text = worker.R_Salary_Sum.ToString("C");
                lb_wo_report_value.Text = worker.R_Value.ToString();

                setEditmode(false);

                btn_wo_export.Enabled = true;
                btn_wo_openfolder.Enabled = true;

            }
        }

        private void neuToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //worker
            lv_wo_worker.SelectedIndices.Clear();
            lb_wo_id.Text = "";
            tb_wo_name.Text = "";
            dt_wo_dayofbirth.Value = DateTime.Now;
            dt_wo_workssince.Value = DateTime.Now;
            nu_wo_salary.Value = 0;
            nu_wo_hoursperweek.Value = 0;
            tb_wo_streetnr.Text = "";
            tb_wo_plz.Text = "";
            tb_wo_city.Text = "";
            tb_wo_phone.Text = "";
            tb_wo_mail.Text = "";
            tb_wo_mobile.Text = "";

            lb_wo_report_jobs.Text = "";
            lb_wo_report_job_avg.Text = "";
            lb_wo_report_job_sum.Text = "";
            lb_wo_report_work_months.Text = "";
            lb_wo_reports_salary_sum.Text = "";
            lb_wo_report_value.Text = "";

            worker = new Worker(db);
            btn_wo_save.Enabled = true;
            btn_wo_export.Enabled = false;
            btn_wo_openfolder.Enabled = false;
            tb_wo_name.Focus();
        }

        private void löschenToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //worker
            if (MessageBox.Show("Wollen Sie wirklick " + worker.NiceID + " (" + worker.Name + ") löschen?", "Bestätigung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                worker.Delete();
                fillWorkers();
                lv_wo_worker.SelectedIndices.Clear();
            }
        }

        private void btn_wo_export_Click(object sender, EventArgs e)
        {
            new Export(worker, db, ExportCount.SINGLE).ShowDialog();
        }

        private void btn_wo_export_all_Click(object sender, EventArgs e)
        {
            new Export(worker, db, ExportCount.MULTI).ShowDialog();
        }

        private void wo_changed(object sender, EventArgs e)
        {
            setEditmode(true);
        }

        private void btn_wo_cancel_Click(object sender, EventArgs e)
        {
            tb_wo_name.Text = worker.Name;
            dt_wo_dayofbirth.Value = worker.DateOfBirth;
            dt_wo_workssince.Value = worker.WorksSince;
            nu_wo_salary.Value = worker.Salary;
            nu_wo_hoursperweek.Value = worker.HoursPerWeek;
            tb_wo_streetnr.Text = worker.StreetNr;
            tb_wo_plz.Text = worker.PLZ;
            tb_wo_city.Text = worker.City;
            tb_wo_phone.Text = worker.Phone;
            tb_wo_mail.Text = worker.EMail;
            tb_wo_mobile.Text = worker.Mobile;
            setEditmode(false);
        }

        private void btn_wo_save_Click(object sender, EventArgs e)
        {
            worker.Name = tb_wo_name.Text;
            worker.DateOfBirth = dt_wo_dayofbirth.Value;
            worker.WorksSince = dt_wo_workssince.Value;
            worker.Salary = nu_wo_salary.Value;
            worker.HoursPerWeek = nu_wo_hoursperweek.Value;
            worker.StreetNr = tb_wo_streetnr.Text;
            worker.PLZ = tb_wo_plz.Text;
            worker.City = tb_wo_city.Text;
            worker.Phone = tb_wo_phone.Text;
            worker.EMail = tb_wo_mail.Text;
            worker.Mobile = tb_wo_mobile.Text;
            if (!worker.Save())
            {
                MessageBox.Show("Konnte nicht speichern...", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Mitarbeiter " + worker.Name + " gespeichert.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            setEditmode(false);
            int sele = -1;
            if (lv_wo_worker.SelectedIndices.Count > 0)
            {
                sele = lv_wo_worker.SelectedIndices[0];
            }
            fillWorkers();
            if (sele > 0)
            {
                lv_wo_worker.Items[sele].Selected = true;
            }
        }

        private void cms_wo_worker_Opening(object sender, CancelEventArgs e)
        {
            löschenToolStripMenuItem4.Enabled = lv_wo_worker.SelectedIndices.Count > 0;
        }

        private void btn_wo_openfolder_Click(object sender, EventArgs e)
        {
            Process.Start(Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["workers"] + Path.DirectorySeparatorChar + worker.NiceID);
        }

        private void lv_it_items_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_it_items.SelectedIndices.Count > 0)
            {
                loadItem(lv_it_items.SelectedItems[0].Text);
            }
        }

        private void loadItem(string id)
        {
            item = new Item(db, id);

            lb_it_id.Text = item.NiceID;
            tb_it_name.Text = item.Name;
            nu_it_value.Value = item.Value;

            lb_it_count.Text = item.Count.ToString();
            lb_it_sum.Text = item.Value_Sum.ToString("C");

            setEditmode(false);

            grid_it_log.DataSource = item.Log;
        }

        private void btn_it_import_Click(object sender, EventArgs e)
        {
            new Items_Edit(db, true).ShowDialog();
            loadItem(item.ID);
        }

        private void btn_it_export_Click(object sender, EventArgs e)
        {
            new Items_Edit(db, false).ShowDialog();
            loadItem(item.ID);
        }

        private void cms_it_items_Opening(object sender, CancelEventArgs e)
        {
            eingangToolStripMenuItem.Enabled = lv_it_items.SelectedIndices.Count > 0;
            ausgangToolStripMenuItem.Enabled = lv_it_items.SelectedIndices.Count > 0;

        }

        private void eingangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Items_Edit(db, true, item).ShowDialog();
            loadItem(item.ID);

        }

        private void ausgangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Items_Edit(db, false, item).ShowDialog();
            loadItem(item.ID);
        }


        private void lv_it_items_Resize(object sender, EventArgs e)
        {
            sizeLastColumn(lv_it_items);
        }

        private void btn_it_save_Click(object sender, EventArgs e)
        {
            item.Name = tb_it_name.Text;
            item.Value = nu_it_value.Value;

            if (!item.Save())
            {
                MessageBox.Show("Konnte nicht speichern...", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Gegenstand " + item.Name + " gespeichert.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            setEditmode(false);
            int sele = -1;
            if (lv_it_items.SelectedIndices.Count > 0)
            {
                sele = lv_it_items.SelectedIndices[0];
            }
            fillItems();
            if (sele > 0)
            {
                lv_it_items.Items[sele].Selected = true;
            }
        }

        private void btn_it_cancel_Click(object sender, EventArgs e)
        {
            tb_it_name.Text = item.Name;
            nu_it_value.Value = item.Value;
            setEditmode(false);
        }

        private void tb_it_changed(object sender, EventArgs e)
        {
            setEditmode(true);
        }

        private void neuToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            //it
            lv_it_items.SelectedIndices.Clear();
            lb_it_id.Text = "";
            tb_it_name.Text = "";
            nu_it_value.Value = 0;

            item = new Item(db);
            btn_it_save.Enabled = true;
            tb_it_name.Focus();
        }

        private void lv_re_reports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_re_reports.SelectedIndices.Count > 0)
            {
                if (report == null) report = new Surplus(db);
                report = report.SelectedReport(lv_re_reports.SelectedItems[0].Text);
                grid_reports.DataSource = report.GetData();
                grid_reports.Refresh();
            }
        }

        private void cb_re_filter_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_reports.DataSource = report.ChangeYear(cb_re_filter_year.SelectedItem.ToString());
            grid_reports.Refresh();
        }

        private void btn_re_Export_Click(object sender, EventArgs e)
        {
            new Export(report, db, ExportCount.SINGLE).ShowDialog();
        }

    }
}
