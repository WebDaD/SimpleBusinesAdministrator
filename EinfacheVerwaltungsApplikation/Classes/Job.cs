using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Export;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Helper;
using System.IO;

namespace ManageAdministerExalt.Classes
{
    /// <summary>
    /// A Job, the Main Object in this software
    /// </summary>
    public class Job : Module
    {
        private Dictionary<Service, int> services; // int is number of units, object may be alteres and must be saved into job_has_services
        private Dictionary<Reminder, DateTime> reminders;
        private List<Discount> discounts;
        private Bill bill;
        private Dictionary<Item, int> items; //int is count, must be saved into item_log and job_has_items
        private JobStatus status;
        private DateTime offer_sent;
        private DateTime offer_created;
        private Customer customer;
        private Worker worker;
        private string address; //split: street_nr|plz|city OR "C" <- Kunde

        public Dictionary<Service, int> Services { get { return services; } set { this.services = value; } }
        public Dictionary<Reminder, DateTime> Reminders { get { return reminders; } set { this.reminders = value; } }
        public List<Discount> Discounts { get { return discounts; } set { this.discounts = value; } }
        public Bill Bill { get { return bill; } set { this.bill = value; } }
        public Dictionary<Item, int> Items { get { return items; } set { this.items = value; } }
        public JobStatus Status { get { return status; } set { this.status = value; } }
        public DateTime Offer_Sent { get { return offer_sent; } set { this.offer_sent = value; } }
        public DateTime Offer_Created { get { return offer_created; } set { this.offer_created = value; } }
        public Customer Customer { get { return customer; } }
        public Worker Worker { get { return worker; } }

        public string Address_Text
        {
            get
            {
                if (this.address == "C") return "Kunde";
                else return "Eigen";
            }
        }
        public string Address_StreetNr
        {
            get
            {
                return address.Split('|')[0];
            }
            set
            {
                string[] ht = address.Split('|');
                address = value + "|" + ht[1] + "|" + ht[2];
            }
        }
        public string Address_PLZ
        {
            get
            {
                return address.Split('|')[1];
            }
            set
            {
                string[] ht = address.Split('|');
                address = ht[0] + "|" + value + "|" + ht[2];
            }
        }
        public string Address_City
        {
            get
            {
                return address.Split('|')[2];
            }
            set
            {
                string[] ht = address.Split('|');
                address = ht[0] + "|" + ht[1] + "|" + value;
            }
        }

        public string StatusText
        {
            get
            {
                switch (this.status)
                {
                    case JobStatus.S0_SAVED: return "Angebot gespeichert.";
                    case JobStatus.S1_CREATED: return "Angebot erstelle.";
                    case JobStatus.S2_SENT: return "Angebot verschickt.";
                    case JobStatus.S3_BILL_CREATED: return "Rechnung erstellt.";
                    case JobStatus.S4_BILL_SENT: return "Rechnung verschickt.";
                    case JobStatus.S5_REMINDER_SENT: return "Mahnung verschickt.";
                    case JobStatus.S6_PAYMENT_RECEIVED: return "Zahlungseingang";
                    default: return "Status unbekannt.";
                }
            }
        }

        public string Customer_ID { get { return this.customer.NiceID; } }

        public string ServiceCount
        {
            get
            {
                return this.services.Count.ToString();
            }
        }
        public string ItemCount
        {
            get
            {
                return this.items.Count.ToString();
            }
        }
        public string DiscountCount
        {
            get
            {
                return this.discounts.Count.ToString();
            }
        }
        private Reminder latestReminder()
        {
            if (this.reminders.Count > 0)
            {
                DateTime latest = new DateTime().Subtract(new TimeSpan(100, 100, 100, 100, 100));
                foreach (KeyValuePair<Reminder, DateTime> item in this.reminders)
                {
                    if (latest <= item.Value) latest = item.Value;
                }
                return (Reminder)this.reminders.FirstOrDefault(x => x.Value == latest).Key;
            }
            else { return null; }
        }
        public DateTime LatestReminderTarget
        {
            get
            {
                if (this.reminders.Count > 0)
                {
                    return this.reminders[latestReminder()].AddDays(Convert.ToInt32(latestReminder().Period));
                }
                else { return DateTime.MinValue; }
            }
        }
        public Job(Database db, string id = "")
            : base(db, "jobs", "Aufträge", id)
        {
            if (String.IsNullOrEmpty(id))
            {
                base.Name = "";
                this.services = new Dictionary<Service, int>();
                this.reminders = new Dictionary<Reminder, DateTime>();
                this.discounts = new List<Discount>();
                this.bill = new Bill(db);
                this.items = new Dictionary<Item, int>();
                this.status = JobStatus.S0_SAVED;
                this.offer_sent = DateTime.MinValue;
                this.offer_created = DateTime.Now;
                this.customer = new Customer(db);
                this.worker = new Worker(db);
                this.address = "C";
            }
            else
            {
                Result d = base.DB.getRow(base.Tablename, new string[] { "name","address", "jdate_sent", "worker_id", "jstatus", "customer_id", "jdate_created" }, "`id`='" + id + "'", "", 1);
                base.Name = d.FirstRow["name"];
                this.status = (JobStatus)Enum.Parse(typeof(JobStatus), d.FirstRow["jstatus"]);
                this.offer_sent = DateTime.Parse(d.FirstRow["jdate_sent"]);
                this.offer_created = DateTime.Parse(d.FirstRow["jdate_created"]);
                this.customer = new Customer(db, d.FirstRow["customer_id"]);
                this.worker = new Worker(db, d.FirstRow["worker_id"]);
                this.address = d.FirstRow["address"];

                this.services = loadServices();
                this.reminders = loadReminders();
                this.discounts = loadDiscounts();
                this.bill = loadBill();
                this.items = loadItems();
            }

        }

        public void setWorker(string id)
        {
            this.worker = new Worker(base.DB, id);
        }
        public void setCustomer(string id)
        {
            this.customer = new Customer(base.DB, id);
        }

        private Dictionary<Service, int> loadServices()
        {
            Dictionary<Service, int> r = new Dictionary<Service, int>();
            Result d = base.DB.getRow("job_has_services", new string[] { "service_id", "units", "value" }, "`job_id`='" + base.ID + "'");
            foreach (Row row in d.Rows)
            {
                Service tmp = new Service(base.DB, row.Cells["service_id"]);
                tmp.Value = Convert.ToDecimal(row.Cells["value"]); //Important: Do not call save on this!!!
                r.Add(tmp, Convert.ToInt32(row.Cells["units"]));
            }
            return r;
        }

        private Dictionary<Reminder, DateTime> loadReminders()
        {
            Dictionary<Reminder, DateTime> r = new Dictionary<Reminder, DateTime>();
            Result d = base.DB.getRow("job_has_reminders", new string[] { "reminder_id", "rdate_sent" }, "`job_id`='" + base.ID + "'");
            foreach (Row row in d.Rows)
            {

                r.Add(new Reminder(base.DB, d.FirstRow["reminder_id"]), DateTime.Parse(d.FirstRow["rdate_sent"]));
            }
            return r;
        }

        private List<Discount> loadDiscounts()
        {
            List<Discount> r = new List<Discount>();
            Result d = base.DB.getRow("job_has_discounts", new string[] { "discount_id" }, "`job_id`='" + base.ID + "'");
            foreach (Row row in d.Rows)
            {
                r.Add(new Discount(base.DB, d.FirstRow["discount_id"]));
            }
            return r;
        }

        private Classes.Bill loadBill()
        {
            return new Bill(base.DB, base.DB.getValue("bills", "id", "`job_id`='" + base.DB + "'"));
        }

        private Dictionary<Item, int> loadItems()
        {
            Dictionary<Item, int> r = new Dictionary<Item, int>();
            Result d = base.DB.getRow("job_has_items", new string[] { "item_id", "jcount" }, "`job_id`='" + base.ID + "'");
            foreach (Row row in d.Rows)
            {
                r.Add(new Item(base.DB, d.FirstRow["item_id"]), Convert.ToInt32((d.FirstRow["jcount"])));
            }
            return r;
        }

        public override Dictionary<string, string> FieldSet()
        {
            Dictionary<string, string> r = base.FieldSet();
            r.Add("address", this.address);
            r.Add("jdate_sent", this.offer_sent.ToString("yyyy-MM-dd"));
            r.Add("jdate_created", this.offer_created.ToString("yyyy-MM-dd"));
            r.Add("worker_id", this.worker.ID.ToString());
            r.Add("jstatus", this.status.ToString());
            r.Add("customer_id", this.customer.ID.ToString());
            return r;
        }

        public override bool Save()
        {
            bool ok = base.Save();
            if (ok) ok = SaveServices();
            if (ok) ok = SaveDiscounts();
            if (ok) ok = SaveItems();
            if (ok) ok = SaveReminders();
            if (ok) ok = this.bill.Save();
            if (ok)
            {
                if (String.IsNullOrEmpty(base.ID)) base.ID = base.DB.GetLastInsertedID();
                string target = base.Basepath + this.NiceID;
                if (!Directory.Exists(target)) Directory.CreateDirectory(target);
            }
            return ok;
        }

        public bool SaveServices()
        {
            bool ok = true;
            foreach (KeyValuePair<Service, int> item in services)
            {
                Dictionary<string, string> fs = new Dictionary<string, string>();
                fs.Add("job_id", base.ID);
                fs.Add("service_id", item.Key.ID);
                fs.Add("value", item.Key.Value.ToString());
                fs.Add("units", item.Value.ToString());
                string check = base.DB.getValue("job_has_services", "id", "`job_id`='" + base.ID + "' AND `service_id`='" + item.Key.ID + "'");
                if (String.IsNullOrEmpty(check))
                {
                    base.DB.Insert("job_has_services", fs);
                }
                else
                {
                    base.DB.Update("job_has_services", fs, "`id`='" + check + "'");
                }
            }
            return ok;
        }

        public bool SaveDiscounts()
        {
            bool ok = true;
            foreach (Discount item in discounts)
            {
                Dictionary<string, string> fs = new Dictionary<string, string>();
                fs.Add("job_id", base.ID);
                fs.Add("discount_id", item.ID);
                string check = base.DB.getValue("job_has_discounts", "id", "`job_id`='" + base.ID + "' AND `discount_id`='" + item.ID + "'");
                if (String.IsNullOrEmpty(check))
                {
                    base.DB.Insert("job_has_discounts", fs);
                }
                else
                {
                    base.DB.Update("job_has_discounts", fs, "`id`='" + check + "'");
                }
            }
            return ok;
        }

        public bool SaveItems()
        {
            bool ok = true;
            foreach (KeyValuePair<Item, int> item in items)
            {
                Dictionary<string, string> fs = new Dictionary<string, string>();
                fs.Add("job_id", base.ID);
                fs.Add("item_id", item.Key.ID);
                fs.Add("jcount", item.Value.ToString());
                string check = base.DB.getValue("job_has_items", "id", "`job_id`='" + base.ID + "' AND `item_id`='" + item.Key.ID + "'");
                if (String.IsNullOrEmpty(check))
                {
                    base.DB.Insert("job_has_items", fs);
                    item.Key.AddEntry(item.Value);
                }
                else
                {
                    string old_item_count = base.DB.getValue("job_has_items", "jcount", "`job_id`='" + base.ID + "' AND `item_id`='" + item.Key.ID + "'");
                    base.DB.Update("job_has_items", fs, "`id`='" + check + "'");

                    int item_diff = item.Value - Convert.ToInt32(old_item_count);
                    if (item_diff != 0) item.Key.AddEntry(item_diff);

                }
            }
            return ok;
        }

        public bool SaveReminders()
        {
            bool ok = true;
            foreach (KeyValuePair<Reminder, DateTime> item in reminders)
            {
                Dictionary<string, string> fs = new Dictionary<string, string>();
                fs.Add("job_id", base.ID);
                fs.Add("reminder_id", item.Key.ID);
                fs.Add("rdate_sent", item.Key.Value.ToString("yyyy-MM-dd"));
                string check = base.DB.getValue("job_has_reminders", "id", "`job_id`='" + base.ID + "' AND `reminder_id`='" + item.Key.ID + "'");
                if (String.IsNullOrEmpty(check))
                {
                    base.DB.Insert("job_has_reminders", fs);
                }
                else
                {
                    base.DB.Update("job_has_reminders", fs, "`id`='" + check + "'");
                }
            }
            return ok;
        }

        public override CRUDable createObject(Database db, string id)
        {
            return new Job(db, id);
        }

        public override Content ToContent(ExportCount c)
        {
            //TODO: Here the Offer will be created in SIngle Mode!
            throw new NotImplementedException();
        }

        internal List<string> GetYears()
        {
            Result d = base.DB.getRow(base.Tablename, new string[] { "strftime('%Y',jdate_created)" }, "`active`='1'");
            if (d.RowCount < 1) return null;

            List<string> r = new List<string>();
            foreach (Row item in d.Rows)
            {
                r.Add(item.Cells["strftime('%Y',jdate_created)"]);
            }

            r = r.Distinct<string>().ToList<string>();

            if (r.Count > 0) return r;
            else return null;
        }

        internal List<string> GetMonths()
        {
            Result d = base.DB.getRow(base.Tablename, new string[] { "strftime('%M',jdate_created)" }, "`active`='1'");
            if (d.RowCount < 1) return null;

            List<string> r = new List<string>();
            foreach (Row item in d.Rows)
            {
                r.Add(item.Cells["strftime('%M',jdate_created)"]);
            }

            r = r.Distinct<string>().ToList<string>();

            if (r.Count > 0) return r;
            else return null;
        }

        internal List<ComboBoxItem> GetCustomers()
        {
            Result d = base.DB.getRow(base.Tablename, new string[] { "customer_id" }, "`active`='1'");
            if (d.RowCount < 1) return null;

            List<ComboBoxItem> r = new List<ComboBoxItem>();
            foreach (Row item in d.Rows)
            {
                r.Add(new ComboBoxItem(item.Cells["customer_id"],new Customer(base.DB, item.Cells["customer_id"]).Name));
            }

            r = r.Distinct<ComboBoxItem>().ToList<ComboBoxItem>();

            if (r.Count > 0) return r;
            else return null;
        }
        internal List<ComboBoxItem> GetWorkers()
        {
            Result d = base.DB.getRow("workers", new string[] { "id","name" }, "`active`='1'");
            if (d.RowCount < 1) return null;

            List<ComboBoxItem> r = new List<ComboBoxItem>();
            foreach (Row item in d.Rows)
            {
                r.Add(new ComboBoxItem(item.Cells["id"],item.Cells["name"]));
            }

            r = r.Distinct<ComboBoxItem>().ToList<ComboBoxItem>();

            if (r.Count > 0) return r;
            else return null;
        }

        public override Dictionary<string, string> GetIDList()
        {
            Result d = base.DB.getRow(base.Tablename, new string[] { "id", "jdate_created","customer_id" }, "`active`='1'");
            if (d.RowCount < 1) return null;

            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (Row item in d.Rows)
            {
                r.Add(item.Cells["id"], item.Cells["jdate_created"] + "|" + Config.CreateNiceID(Config.IDFormating["customers"],item.Cells["customer_id"]));
            }

            if (r.Count > 0) return r;
            else return null;
        }

        internal Dictionary<string, string> GetFilteredIDList(string year, string month, string customer_id)
        {
            string filter = "`active`='1' ";
            if (year != "Alle") filter += "AND strftime('%Y',jdate_created)='" + year + "' ";
            if (month != "Alle") filter += "AND strftime('%M',jdate_created)='" + month + "' ";
            if (customer_id != "0") filter += "AND `customer_id`='" + customer_id + "' ";

            Result d = base.DB.getRow(base.Tablename, new string[] { "id", "jdate_created", "customer_id" }, filter, "jdate_created ASC");
            if (d.RowCount < 1) return null;

            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (Row item in d.Rows)
            {
                r.Add(item.Cells["id"], item.Cells["jdate_created"] + "|" + Config.CreateNiceID(Config.IDFormating["customers"], item.Cells["customer_id"]));
            }

            if (r.Count > 0) return r;
            else return null;
        }
    }
}
