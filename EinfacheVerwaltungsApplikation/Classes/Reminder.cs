using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Export;
using System.Data;

namespace ManageAdministerExalt.Classes
{
    public class Reminder : Joinable, Exportable, CRUDable
    {
        public static string TableName = "reminders";

        private Database db;
        private string id;

        private string name;
        public string Name { get { return name; } set { name = value; } }

        private decimal period;
        public decimal Period { get { return period; } set { period = value; } }

        private int type;
        public int Type { get { return type; } set { type = value; } }

        private decimal value;
        public decimal Value { get { return value; } set { this.value = value; } }

        public string NiceID
        {
            get
            {
                return Config.CreateNiceID(Config.IDFormating["reminder"], id);
            }
        }

        public static Dictionary<int, string> Types
        {
            get
            {
                Dictionary<int, string> t = new Dictionary<int, string>();
                t.Add(0, "Prozent");
                t.Add(1, "Festbetrag");
                return t;
            }
        }

        public Dictionary<string, string> FieldSet
        {
            get
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                r.Add("name", this.name);
                r.Add("period", this.period.ToString());
                r.Add("price_type", this.type.ToString());
                r.Add("value", this.value.ToString());
                r.Add("active", "1");
                return r;
            }
        }

        public Reminder(Database db)
        {
            this.db = db;
            this.id = "";
            this.name = "";
            this.period = 14;
            this.type = 0;
            this.value = 0;
        }

        public Reminder(Database db, string id)
        {
            this.db = db;
            Result d = this.db.getRow(Worker.TableName, new string[] { "id", "name", "period", "price_type", "value" }, "`id`='" + id + "'", "", 1);
            this.id = d.FirstRow["id"];
            this.name = d.FirstRow["name"];
            this.period = Convert.ToInt32(d.FirstRow["period"]);
            this.type = Convert.ToInt32(d.FirstRow["price_type"]);
            this.value = Decimal.Parse(d.FirstRow["value"]);

        }
        public string GetJoinOn(Joinable jointable)
        {
            return "id";
        }

        public string GetTableName()
        {
            return Reminder.TableName;
        }

        public List<string> GetFields()
        {
            List<string> f = new List<string>();
            foreach (KeyValuePair<string, string> item in this.FieldSet)
            {
                f.Add(Reminder.TableName + "." + Reminder.TableName + "_" + item.Key);
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
                return "Liste der Mahnungen";
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
                return "reminders";
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
                ok = db.Insert(Reminder.TableName, this.FieldSet);
            }
            else
            {
                ok = db.Update(Reminder.TableName, this.FieldSet, "`id`='" + this.id + "'");

            }

            return ok;
        }

        public bool Delete()
        {
            bool ok = true;
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            tmp.Add("active", "0");

            ok = db.Update(Reminder.TableName, tmp, "`id`='" + this.id + "'");
            return ok;
        }

        public CRUDable New()
        {
            return new Reminder(this.db);
        }

        public Dictionary<string, string> GetIDList()
        {
            Result d = this.db.getRow(Reminder.TableName, new string[] { "id", "name" }, "`active`='1'");
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
            return new Reminder(this.db, id);
        }

        public List<CRUDable> GetFullList()
        {
            List<CRUDable> reminders = new List<CRUDable>();
            Result d = this.db.getRow(Reminder.TableName, new string[] { "id" }, "`active`='1'", "id ASC");
            foreach (Row row in d.Rows)
            {
                reminders.Add(new Reminder(db, row.Cells["id"]));
            }
            return reminders;
        }
    }
}
