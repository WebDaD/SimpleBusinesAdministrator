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
    public class Customer : Exportable, Joinable, CRUDable
    {
        public Dictionary<string, string> getIDList()
        {
            Result d = this.db.getRow(Customer.TableName, new string[] { "id", "name" },"`active`='1'");
            if (d.RowCount < 1) return null;

            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (Row item in d.Rows)
            {
                r.Add(item.Cells["id"], item.Cells["name"]);
            }

            if (r.Count > 0) return r;
            else return null;
        }


        public List<CRUDable> GetFullList()
        {
            List<CRUDable> customers = new List<CRUDable>();
            Result d = this.db.getRow(Customer.TableName, new string[] { "id" }, "`active`='1'", "id ASC");
            foreach (Row row in d.Rows)
            {
                customers.Add(new Customer(db, row.Cells["id"]));
            }
            return customers;
        }

        private Database db;
        public static string TableName = "customers";

        private string id;
        public string ID { get { return id; } set { id = value; } }

        public static string makeNiceID(string id)
        {
            return "C" + id.PadLeft(5, '0');
        }
        public string NiceID
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

        public Customer GetSingleInstance(string id)
        {
            return new Customer(this.db, id);
        }

        public Customer(Database db, string id)
        {
            this.db = db;
            Result d = this.db.getRow(Customer.TableName, new string[] { "id", "name", "street", "plz", "city", "mail", "phone", "mobile", "fax", "contact" }, "`id`='" + id + "'", "", 1);
            this.id = d.FirstRow["id"];
            this.name = d.FirstRow["name"];
            this.street = d.FirstRow["street"];
            this.plz = d.FirstRow["plz"];
            this.city = d.FirstRow["city"];
            this.mail = d.FirstRow["mail"];
            this.phone = d.FirstRow["phone"];
            this.mobile = d.FirstRow["mobile"];
            this.fax = d.FirstRow["fax"];
            this.contact = d.FirstRow["contact"];
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

        public Customer New()
        {
            return new Customer(this.db);
        }

        public bool Save()
        {
            bool ok = true;
            if (String.IsNullOrEmpty(this.id))
            {
                ok = db.Insert(Customer.TableName, this.FieldSet);
            }
            else
            {
                ok = db.Update(Customer.TableName, this.FieldSet, "`id`='" + this.id + "'");

            }
            if (ok)
            {
                if(String.IsNullOrEmpty(this.id))this.id = db.GetLastInsertedID();
                string target = Config.BasePath + Path.DirectorySeparatorChar + "customers" + Path.DirectorySeparatorChar + this.NiceID;
                if (!Directory.Exists(target)) Directory.CreateDirectory(target);
            }

            return ok;
        }

        public bool Delete()
        {
            bool ok = true;
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            tmp.Add("active", "0");

            ok = db.Update(Customer.TableName, tmp, "`id`='" + this.id + "'");
            return ok;
        }

        public Content ToContent(ExportCount c)
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

        public string DataName(ExportCount c)
        {
            if (c == ExportCount.MULTI)
            {
                return "Liste der Kunden";
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public string Filename(ExportCount c)
        {
            if (c == ExportCount.MULTI)
            {
                return "customers";
            }
            else
            {
                throw new NotImplementedException();
            }
        }


        public string GetTableName()
        {
            return Customer.TableName;
        }


        public string GetJoinOn(Joinable jointable)
        {
            return "id";
        }

        public List<string> GetFields()
        {
            List<string> f = new List<string>();
            foreach (KeyValuePair<string,string> item in this.FieldSet)
            {
                f.Add(Customer.TableName + "_" + item.Key);
            }
            return f;
            
        }
    }
}
