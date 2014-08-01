using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Export;
using System.Data;

namespace ManageAdministerExalt.Classes
{
    public class Reminder : Module
    {
        private decimal period;
        public decimal Period { get { return period; } set { period = value; } }

        private int type;
        public int Type { get { return type; } set { type = value; } }

        private decimal value;
        public decimal Value { get { return value; } set { this.value = value; } }

        public static Dictionary<int, string> Types //TODO into table
        {
            get
            {
                Dictionary<int, string> t = new Dictionary<int, string>();
                t.Add(0, "Prozent");
                t.Add(1, "Festbetrag");
                return t;
            }
        }

        public override Dictionary<string, string> FieldSet()
        {
                Dictionary<string, string> r = base.FieldSet();
                r.Add("period", this.period.ToString());
                r.Add("price_type", this.type.ToString());
                r.Add("value", this.value.ToString());
                return r;
        }

        public Reminder(Database db, string id="") : base(db,"reminders","Mahnungen",id)
        {
            if (String.IsNullOrEmpty(id))
            {
                base.Name = "";
                this.period = 14;
                this.type = 0;
                this.value = 0;
            }
            else
            {
                Result d = base.DB.getRow(base.Tablename, new string[] { "name", "period", "price_type", "value" }, "`id`='" + id + "'", "", 1);
                base.Name = d.FirstRow["name"];
                this.period = Convert.ToInt32(d.FirstRow["period"]);
                this.type = Convert.ToInt32(d.FirstRow["price_type"]);
                this.value = Decimal.Parse(d.FirstRow["value"]);
            }
        }
        public override CRUDable createObject(Database db, string id)
        {
            return new Reminder(db, id);
        }

        public override Content ToContent(ExportCount c)
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

    }
}
