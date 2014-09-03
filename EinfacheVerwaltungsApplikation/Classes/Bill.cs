using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Export;

namespace ManageAdministerExalt.Classes
{
    public class Bill : Module
    {
        private bool taxes;
        private DateTime payment_received;
        private DateTime bill_sent;
        private string job_id;

        private Dictionary<Service, int> services; // int is number of units, object may be alteres and must be saved into job_has_services
        private List<Discount> discounts;
        private List<PaymentCondition> conditions;

        

        //TODO: public properties

        public Bill(Database db, string id = "")
            : base(db, "bills", "Rechnungen", id)
        {
            if (String.IsNullOrEmpty(id))
            {
                base.Name = "";
                this.taxes = true;
                this.payment_received = DateTime.MinValue;
                this.bill_sent = DateTime.MinValue;
                this.job_id = "";
                this.services = new Dictionary<Service, int>();
                this.discounts = new List<Discount>();
                this.conditions = new List<PaymentCondition>();
            }
            else
            {
                Result d = base.DB.getRow(base.Tablename, new string[] { "taxes", "bdate_received", "bdate_sent", "job_id" }, "`id`='" + id + "'", "", 1);
                base.Name = "";
                this.payment_received = DateTime.Parse(d.FirstRow["bdate_received"]);
                this.bill_sent = DateTime.Parse(d.FirstRow["bdate_sent"]);
                this.job_id = d.FirstRow["job_id"];
                this.services = loadServices();
                this.conditions = loadConditions();
                this.discounts = loadDiscounts();
                this.taxes = d.FirstRow["taxes"] == "0" ? false : true;
            }

        }
        public override void Unload()
        {
            base.ID = "";
            base.Name = "";
            this.taxes = true;
            this.payment_received = DateTime.MinValue;
            this.bill_sent = DateTime.MinValue;
            this.job_id = "";
            this.services = new Dictionary<Service, int>();
            this.discounts = new List<Discount>();
            this.conditions = new List<PaymentCondition>();
        }
        public override bool Load(string id)
        {
            base.ID = id;
            Result d = base.DB.getRow(base.Tablename, new string[] { "taxes", "bdate_received", "bdate_sent", "job_id" }, "`id`='" + id + "'", "", 1);
            if (d.RowCount > 0)
            {
                base.Name = "";
                this.payment_received = DateTime.Parse(d.FirstRow["bdate_received"]);
                this.bill_sent = DateTime.Parse(d.FirstRow["bdate_sent"]);
                this.job_id = d.FirstRow["job_id"];
                this.services = loadServices();
                this.conditions = loadConditions();
                this.discounts = loadDiscounts();
                this.taxes = d.FirstRow["taxes"] == "0" ? false : true;
                return true;
            }
            else return false;
        }
        private Dictionary<Service, int> loadServices()
        {
            Dictionary<Service, int> r = new Dictionary<Service, int>();
            Result d = base.DB.getRow("bill_has_services", new string[] { "service_id", "units", "value" }, "`bill_id`='" + base.ID + "'");
            foreach (Row row in d.Rows)
            {
                Service tmp = new Service(base.DB, row.Cells["service_id"]);
                tmp.Value = Convert.ToDecimal(row.Cells["value"]); //Important: Do not call save on this!!!
                r.Add(tmp, Convert.ToInt32(row.Cells["units"]));
            }
            return r;
        }

        private List<PaymentCondition> loadConditions()
        {
            List<PaymentCondition> r = new List<PaymentCondition>();
            Result d = base.DB.getRow("bill_has_payment_conditions", new string[] { "id_condition" }, "`id_bill`='" + base.ID + "'");
            foreach (Row row in d.Rows)
            {
                r.Add(new PaymentCondition(base.DB, d.FirstRow["id_condition"]));
            }
            return r;
        }

        private List<Discount> loadDiscounts()
        {
            List<Discount> r = new List<Discount>();
            Result d = base.DB.getRow("bill_has_discounts", new string[] { "discount_id" }, "`bill_id`='" + base.ID + "'");
            foreach (Row row in d.Rows)
            {
                r.Add(new Discount(base.DB, d.FirstRow["discount_id"]));
            }
            return r;
        }

        public override Dictionary<string, string> FieldSet()
        {
            Dictionary<string, string> r = base.FieldSet();
            r.Remove("name");
            r.Add("taxes", (this.taxes?"1":"0"));
            r.Add("job_id", this.job_id);
            r.Add("bdate_received", this.payment_received.ToString("yyyy-MM-dd"));
            r.Add("bdate_sent", this.bill_sent.ToString("yyyy-MM-dd"));
            return r;
        }

        public override bool Save()
        {
            bool ok = base.Save();
            if (ok) ok = SaveServices();
            if (ok) ok = SaveDiscounts();
            if (ok) ok = SaveConditions();
            return ok;
        }

        public bool SaveServices()
        {
            bool ok = true;
            foreach (KeyValuePair<Service,int> item in services)
            {
                Dictionary<string, string> fs = new Dictionary<string, string>();
                fs.Add("bill_id", base.ID);
                fs.Add("service_id", item.Key.ID);
                fs.Add("value", item.Key.Value.ToString());
                fs.Add("units", item.Value.ToString());
                string check = base.DB.getValue("bill_has_services", "id", "`bill_id`='"+base.ID+"' AND `service_id`='"+item.Key.ID+"'");
                if (String.IsNullOrEmpty(check))
                {
                    base.DB.Insert("bill_has_services", fs);
                }
                else
                {
                    base.DB.Update("bill_has_services", fs,"`id`='"+check+"'");
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
                fs.Add("bill_id", base.ID);
                fs.Add("discount_id", item.ID);
                string check = base.DB.getValue("bill_has_discounts", "id", "`bill_id`='" + base.ID + "' AND `discount_id`='" + item.ID + "'");
                if (String.IsNullOrEmpty(check))
                {
                    base.DB.Insert("bill_has_discounts", fs);
                }
                else
                {
                    base.DB.Update("bill_has_discounts", fs, "`id`='" + check + "'");
                }
            }
            return ok;
        }
        public bool SaveConditions()
        {
            bool ok = true;
            foreach (PaymentCondition item in conditions)
            {
                Dictionary<string, string> fs = new Dictionary<string, string>();
                fs.Add("id_bill", base.ID);
                fs.Add("id_condition", item.ID);
                string check = base.DB.getValue("bill_has_payment_confitions", "id", "`id_bill`='" + base.ID + "' AND `id_condition`='" + item.ID + "'");
                if (String.IsNullOrEmpty(check))
                {
                    base.DB.Insert("bill_has_payment_confitions", fs);
                }
                else
                {
                    base.DB.Update("bill_has_payment_confitions", fs, "`id`='" + check + "'");
                }
            }
            return ok;
        }

        public override CRUDable createObject(Database db, string id)
        {
            return new Bill(db, id);
        }

        public override Content ToContent(ExportCount c)
        {
            //TODO: Here will be the BILL! (so calculate!)
            throw new NotImplementedException();
        }
        public override string WorkerName
        {
            get { return ""; }
        }

        public override string Adress
        {
            get { throw new NotImplementedException(); }
        }

        public override string DateCreated
        {
            get { throw new NotImplementedException(); }
        }

        public override string DateSecond
        {
            get { throw new NotImplementedException(); }
        }
    }
}
