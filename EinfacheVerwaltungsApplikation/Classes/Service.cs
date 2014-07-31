using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Export;
using System.Data;

namespace ManageAdministerExalt.Classes
{
    public class Service : Joinable, Exportable, CRUDable
    {
        public static string TableName = "services";

        private Database db;
        private string id;

        private string name;
        public string Name { get { return name; } set { name = value; } }

        private string description;
        public string Description { get { return description; } set { description = value; } }

        private string unit;
        public string Unit { get { return unit; } set { unit = value; } }

        private decimal value;
        public decimal Value { get { return value; } set { this.value = value; } }

        public string NiceID
        {
            get
            {
                return Config.CreateNiceID(Config.IDFormating["service"], id);
            }
        }

        public Dictionary<string, string> FieldSet
        {
            get
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                r.Add("name", this.name);
                r.Add("description", this.description);
                r.Add("unit", this.unit);
                r.Add("value", this.value.ToString());
                r.Add("active", "1");
                return r;
            }
        }

        public Service(Database db)
        {
            this.db = db;
            this.id = "";
            this.name = "";
            this.description = "";
            this.unit = "";
            this.value = 0;
        }

        public Service(Database db, string id)
        {
            this.db = db;
            Result d = this.db.getRow(Service.TableName, new string[] { "id", "name", "description", "unit", "value" }, "`id`='" + id + "'", "", 1);
            this.id = d.FirstRow["id"];
            this.name = d.FirstRow["name"];
            this.description = d.FirstRow["description"];
            this.unit = d.FirstRow["unit"];
            this.value = Decimal.Parse(d.FirstRow["value"]);

        }
        public string GetJoinOn(Joinable jointable)
        {
            return "id";
        }

        public string GetTableName()
        {
            return Service.TableName;
        }

        public List<string> GetFields()
        {
            List<string> f = new List<string>();
            foreach (KeyValuePair<string, string> item in this.FieldSet)
            {
                f.Add(Service.TableName + "." + Service.TableName + "_" + item.Key);
            }
            return f;
        }

        public Content ToContent(ExportCount c)
        {
            if (c == ExportCount.MULTI)
            {
                DataTable t = new DataTable();
                t.Columns.Add("ID");
                t.Columns.Add("Name");

                foreach (Reminder item in this.GetFullList())
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
                return "Liste der Dienstleistungen";
            }
            else
            {
                return this.NiceID + " - " + this.Name;
            }
        }

        public string Filename(ExportCount c)
        {
            if (c == ExportCount.MULTI)
            {
                return "services";
            }
            else
            {
                return this.NiceID;
            }
        }

        public bool Save()
        {
            bool ok = true;
            if (String.IsNullOrEmpty(this.id))
            {
                ok = db.Insert(Service.TableName, this.FieldSet);
            }
            else
            {
                ok = db.Update(Service.TableName, this.FieldSet, "`id`='" + this.id + "'");

            }

            return ok;
        }

        public bool Delete()
        {
            bool ok = true;
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            tmp.Add("active", "0");

            ok = db.Update(Service.TableName, tmp, "`id`='" + this.id + "'");
            return ok;
        }

        public CRUDable New()
        {
            return new Service(this.db);
        }

        public Dictionary<string, string> GetIDList()
        {
            Result d = this.db.getRow(Service.TableName, new string[] { "id", "name" }, "`active`='1'");
            if (d.RowCount < 1) return null;

            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (Row item in d.Rows)
            {
                r.Add(item.Cells["id"], item.Cells["name"]);
            }

            if (r.Count > 0) return r;
            else return null;
        }

        public CRUDable GetSingleInstance(string id)
        {
            return new Service(this.db, id);
        }

        public List<CRUDable> GetFullList()
        {
            List<CRUDable> services = new List<CRUDable>();
            Result d = this.db.getRow(Service.TableName, new string[] { "id" }, "`active`='1'", "id ASC");
            foreach (Row row in d.Rows)
            {
                services.Add(new Service(db, row.Cells["id"]));
            }
            return services;
        }
    }
}
