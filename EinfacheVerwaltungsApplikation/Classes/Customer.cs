using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Export;
using System.IO;
using System.Data;

namespace ManageAdministerExalt.Classes
{
    /// <summary>
    /// Gets or Sets all Data for Customers
    /// </summary>
    public class Customer : Module
    {
        private string street;
        public string Street { get { return street; } set { street = value; } }

        private string plz;
        public string PLZ { get { return plz; } set { plz = value; } }

        private string city;
        public string City { get { return city; } set { city = value; } }

        private string mail;
        public string Mail { get { return mail; } set { mail = value; } }

        private string phone;
        public string Phone { get { return phone; } set { phone = value; } }

        private string mobile;
        public string Mobile { get { return mobile; } set { mobile = value; } }

        private string fax;
        public string Fax { get { return fax; } set { fax = value; } }

        private string contact;
        public string Contact { get { return contact; } set { contact = value; } }

        public override Dictionary<string, string> FieldSet()
        {
            Dictionary<string, string> r = base.FieldSet();
            r.Add("street", this.street);
            r.Add("plz", this.plz);
            r.Add("city", this.city);
            r.Add("mail", this.mail);
            r.Add("phone", this.phone);
            r.Add("mobile", this.mobile);
            r.Add("fax", this.fax);
            r.Add("contact", this.contact);
            return r;
        }



        public Customer(Database db, string id=""):base(db,"customers","Kunden",id)
        {
            if (String.IsNullOrEmpty(id))
            {
                base.Name = "";
                this.street = "";
                this.plz = "";
                this.city = "";
                this.mail = "";
                this.phone = "";
                this.mobile = "";
                this.fax = "";
                this.contact = "";
            }
            else
            {
                Result d = base.DB.getRow(base.Tablename, new string[] { "name", "street", "plz", "city", "mail", "phone", "mobile", "fax", "contact" }, "`id`='" + id + "'", "", 1);
                base.Name = d.FirstRow["name"];
                this.street = d.FirstRow["street"];
                this.plz = d.FirstRow["plz"];
                this.city = d.FirstRow["city"];
                this.mail = d.FirstRow["mail"];
                this.phone = d.FirstRow["phone"];
                this.mobile = d.FirstRow["mobile"];
                this.fax = d.FirstRow["fax"];
                this.contact = d.FirstRow["contact"];
            }
           
        }

        public override bool Save()
        {
            bool ok = base.Save();
            if (ok)
            {
                if (String.IsNullOrEmpty(base.ID)) base.ID = base.DB.GetLastInsertedID();
                string target = base.Basepath + this.NiceID;
                if (!Directory.Exists(target)) Directory.CreateDirectory(target);
            }
            return ok;
        }

        public override Content ToContent(ExportCount c)
        {
            if (c == ExportCount.MULTI)
            {
                DataTable t = new DataTable();
                t.Columns.Add("ID");
                t.Columns.Add("Name");

                foreach (Customer item in this.GetFullList())
                {
                    t.Rows.Add(new string[] { item.NiceID, item.Name });
                }

                Content ct = new ContentTable(DataType.Table, t);
                return ct;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override string WorkerName
        {
            get { return ""; throw new NotImplementedException(); }
        }

        public override string Adress
        {
            get { return ""; throw new NotImplementedException(); }
        }

        public override string DateCreated
        {
            get { return ""; throw new NotImplementedException(); }
        }

        public override string DateSecond
        {
            get { return ""; throw new NotImplementedException(); }
        }

        public override CRUDable createObject(Database db, string id)
        {
            return new Customer(db, id);
        }

       //Report Properties



    }
}
