using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Export;

namespace ManageAdministerExalt.Classes
{
    public class Discount : Joinable,Exportable,CRUDable
    {
        
        private Database db;

        private string id;

        private static string TableName = "discounts";

        private string name;
        public string Name { get { return name; } set { name = value; } }

        public string NiceID { get { return Config.CreateNiceID(Config.IDFormating["discount"], id); } }

        private decimal value;
        public decimal Value { get { return value; } set { this.value = value; } }

        private int type;
        public int Type { get { return type; } set { type = value; } }


        public Dictionary<string, string> FieldSet
        {
            get
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                r.Add("name", this.name);
                r.Add("value", this.value.ToString());
                r.Add("type", this.type.ToString());
                r.Add("active", "1");
                return r;
            }
        }

        public static Dictionary<int, string> TYPES
        {
            get
            {
                Dictionary<int, string> t = new Dictionary<int, string>();
                t.Add(0, "Prozent");
                t.Add(1, "Festbetrag");
                return t;
            }
        }

        public Discount(Database db)
        {
            this.db = db;
            this.name = "";
            this.value = 0;
            this.type = 0;
        }

        public Discount(Database db, string id)
        {
            this.db = db;
            this.id = id;
            Result d = this.db.getRow(Discount.TableName, new string[] { "id", "name", "dtype", "dvalue" }, "`id`='" + id + "'", "", 1);
            this.id = d.FirstRow["id"];
            this.name = d.FirstRow["name"];
            this.value = Decimal.Parse(d.FirstRow["dvalue"]);
            this.type = Convert.ToInt32(d.FirstRow["dvalue"]);
        }
        public Dictionary<string, string> GetIDList()
        {
            Result d = this.db.getRow(Discount.TableName, new string[] { "id", "name" },"`active`='1'");
            if (d.RowCount < 1) return null;

            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (Row item in d.Rows)
            {
                r.Add(item.Cells["id"], item.Cells["name"]);
            }

            if (r.Count > 0) return r;
            else return null;
        }


        public string GetJoinOn(Joinable jointable)
        {
            return "id";
        }

        public string GetTableName()
        {
            return Discount.TableName;
        }

        public List<string> GetFields()
        {
            List<string> f = new List<string>();
            foreach (KeyValuePair<string, string> item in this.FieldSet)
            {
                f.Add(Discount.TableName + "." + Discount.TableName + "_" + item.Key);
            }
            return f;
        }

        public Content ToContent(ExportCount c)
        {
            throw new NotImplementedException();
        }

        public string DataName(ExportCount c)
        {
            throw new NotImplementedException();
        }

        public string Filename(ExportCount c)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            bool ok = true;
            if (String.IsNullOrEmpty(this.id))
            {
                ok = db.Insert(Discount.TableName, this.FieldSet);
            }
            else
            {
                ok = db.Update(Discount.TableName, this.FieldSet, "`id`='" + this.id + "'");

            }
            return ok;
        }

        public bool Delete()
        {
            bool ok = true;
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            tmp.Add("active", "0");

            ok = db.Update(Discount.TableName, tmp, "`id`='" + this.id + "'");
            return ok;
        }

        public CRUDable New()
        {
            return new Discount(this.db);
        }

        public CRUDable GetSingleInstance(string id)
        {
            return new Discount(this.db, id);
        }

        public List<CRUDable> GetFullList()
        {
            List<CRUDable> discounts = new List<CRUDable>();
            Result d = this.db.getRow(Discount.TableName, new string[] { "id" }, "`active`='1'", "id ASC");
            foreach (Row row in d.Rows)
            {
                discounts.Add(new Customer(db, row.Cells["id"]));
            }
            return discounts;
        }
    }
}
