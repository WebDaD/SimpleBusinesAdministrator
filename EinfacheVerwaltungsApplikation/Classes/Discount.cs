using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Export;

namespace ManageAdministerExalt.Classes
{
    public class Discount : Module
    {
        private decimal value;
        public decimal Value { get { return value; } set { this.value = value; } }

        private int type;
        public int Type { get { return type; } set { type = value; } }


        public override Dictionary<string, string> FieldSet()
        {
            Dictionary<string, string> r = base.FieldSet();
            r.Add("value", this.value.ToString());
            r.Add("type", this.type.ToString());
            return r;
        }

        public static Dictionary<int, string> TYPES //TODO: into table and object
        {
            get
            {
                Dictionary<int, string> t = new Dictionary<int, string>();
                t.Add(0, "Prozent");
                t.Add(1, "Festbetrag");
                return t;
            }
        }

        public Discount(Database db, string id="")
            : base(db, "discounts", "Rabatte", id)
        {
            if (String.IsNullOrEmpty(id))
            {
                base.Name = "";
                this.value = 0;
                this.type = 0;
            }
            else
            {
                Result d = base.DB.getRow(base.Tablename, new string[] { "name", "dtype", "dvalue" }, "`id`='" + id + "'", "", 1);
                base.Name = d.FirstRow["name"];
                this.value = Decimal.Parse(d.FirstRow["dvalue"]);
                this.type = Convert.ToInt32(d.FirstRow["dvalue"]);
            }

        }
        public override CRUDable createObject(Database db, string id)
        {
            return new Discount(db, id);
        }

        public override Content ToContent(ExportCount c)
        {
            throw new NotImplementedException();
        }
    }
}
