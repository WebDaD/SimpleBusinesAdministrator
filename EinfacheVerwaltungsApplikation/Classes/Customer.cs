using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Export;
using System.IO;

namespace ManageAdministerExalt.Classes
{
    /// <summary>
    /// Gets or Sets all Data for Customers
    /// </summary>
    public class Customer : Exportable
    {
        public static Dictionary<string, string> getCustomers(Database db)
        {
            List<List<string>> d = new List<List<string>>();
            d = db.getRow(Customer.table, new string[] { "id", "name" },"`active`='1'");

            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (List<string> item in d)
            {
                r.Add(item[0], item[1]);
            }

            if (d.Count > 0) return r;
            else return null;
        }

        private Database db;
        public static string table = "customers";

        private string id;
        public string ID { get { return id; } set { id = value; } }

        public static string makeKundenNummer(string id)
        {
            return "C" + id.PadLeft(5, '0');
        }
        public string KundenNummer
        {
            get
            {
                return "C" + id.PadLeft(5, '0');
            }
        }

        private string name;
        public string Name { get { return name; } set { name = value; } }

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

        public Dictionary<string, string> FieldSet 
        {
            get 
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                r.Add("name", this.name);
                r.Add("street", this.street);
                r.Add("plz", this.plz);
                r.Add("city", this.city);
                r.Add("mail", this.mail);
                r.Add("phone", this.phone);
                r.Add("mobile", this.mobile);
                r.Add("fax", this.fax);
                r.Add("contact", this.contact);
                r.Add("active", "1");
                return r;
            } 
        }

        public Customer(Database db, string id)
        {
            this.db = db;
            List<List<string>> d = new List<List<string>>();
            d = this.db.getRow(Customer.table, new string[] { "id", "name","street","plz","city","mail","phone","mobile","fax","contact" },"`id`='"+id+"'","",1);
            this.id = d[0][0];
            this.name = d[0][1];
            this.street = d[0][2];
            this.plz = d[0][3];
            this.city = d[0][4];
            this.mail = d[0][5];
            this.phone = d[0][6];
            this.mobile = d[0][7];
            this.fax = d[0][8];
            this.contact = d[0][9];
        }

        /// <summary>
        /// Empty Customer for Saving purposes or Errors
        /// </summary>
        public Customer(Database db)
        {
            this.db = db;
            this.id = "";
            this.name = "";
            this.street = "";
            this.plz = "";
            this.city = "";
            this.mail = "";
            this.phone = "";
            this.mobile = "";
            this.fax = "";
            this.contact = "";
        }

        public bool Save()
        {
            bool ok = true;
            if (String.IsNullOrEmpty(this.id))
            {
                ok = db.Insert(Customer.table, this.FieldSet);
            }
            else
            {
                ok = db.Update(Customer.table, this.FieldSet, "`id`='" + this.id + "'");

            }
            if (ok)
            {
                if(String.IsNullOrEmpty(this.id))this.id = db.GetLastInsertedID();
                string target = Config.BasePath + Path.DirectorySeparatorChar + "customers" + Path.DirectorySeparatorChar + this.KundenNummer;
                if (!Directory.Exists(target)) Directory.CreateDirectory(target);
            }

            return ok;
        }

        public bool Delete()
        {
            bool ok = true;
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            tmp.Add("active", "0");

            ok = db.Update(Customer.table, tmp, "`id`='" + this.id + "'");
            return ok;
        }

        public Content ToContent()
        {
            throw new NotImplementedException();
        }

        public string DataName()
        {
            throw new NotImplementedException();
        }

        public string Filename()
        {
            throw new NotImplementedException();
        }
    }
}
