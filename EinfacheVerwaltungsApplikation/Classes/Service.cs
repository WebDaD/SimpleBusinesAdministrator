using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Export;

namespace ManageAdministerExalt.Classes
{
    /// <summary>
    /// All Data for One Service. With Static Class to get Lists of services
    /// </summary>
    public class Service : Exportable, Joinable
    {
        public static Dictionary<string, string> getServices(Database db)
        {
            List<List<string>> d = new List<List<string>>();
            d = db.getRow(Service.table, new string[] { "id", "name" }, "`active`='1'");

            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (List<string> item in d)
            {
                r.Add(item[0], item[1]);
            }

            if (d.Count > 0) return r;
            else return null;
        }

        private Database db;
        private static string table = "services";

        private string id;
        public string ID { get { return id; } set { id = value; } }

        public static string makeServiceNummer(string id) //TODO: use Propertiy Ption!
        {
            return "S" + id.PadLeft(5, '0');
        }
        public string ServiceNummer
        {
            get
            {
                return "S" + id.PadLeft(5, '0');
            }
        }

        private string name;
        public string Name { get { return name; } set { name = value; } }

        private string description;
        public string Description { get { return description; } set { description = value; } }

        private string unit;
        public string Unit { get { return unit; } set { unit = value; } }

        private string price;
        public string Price { get { return price; } set { price = value; } }

         public Dictionary<string, string> FieldSet 
        {
            get 
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                r.Add("name", this.name);
                r.Add("description", this.description);
                r.Add("unit", this.unit);
                r.Add("value", this.price);
                r.Add("active", "1");
                return r;
            } 
        }

        public Service(Database db, string id)
        {
            this.db = db;
            List<List<string>> d = new List<List<string>>();
            d = this.db.getRow(Service.table, new string[] { "id", "name","description","unit","value" },"`id`='"+id+"'","",1);
            this.id = d[0][0];
            this.name = d[0][1];
            this.description = d[0][2];
            this.unit = d[0][3];
            this.price = d[0][4];
        }

        /// <summary>
        /// Empty Service for Saving purposes or Errors
        /// </summary>
        public Service(Database db)
        {
            this.db = db;
            this.id = "";
            this.name = "";
            this.description = "";
            this.unit = "";
            this.price = "";
        }

        public bool Save()
        {
            bool ok = true;
            if (String.IsNullOrEmpty(this.id))
            {
                ok = db.Insert(Service.table, this.FieldSet);
            }
            else
            {
                ok = db.Update(Service.table, this.FieldSet, "`id`='" + this.id + "'");

            }
            return ok;
        }

        public bool Delete()
        {
            bool ok = true;
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            tmp.Add("active", "0");

            ok = db.Update(Service.table, tmp, "`id`='" + this.id + "'");

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

        public string JoinOn(Joinable jointable)
        {
            throw new NotImplementedException();
        }

        public string GetTableName()
        {
            throw new NotImplementedException();
        }
    }
}
