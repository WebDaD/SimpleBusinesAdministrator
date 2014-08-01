using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Export;
using System.Data;

namespace ManageAdministerExalt.Classes
{
    public class Service : Module
    {

        private string description;
        public string Description { get { return description; } set { description = value; } }

        private string unit;
        public string Unit { get { return unit; } set { unit = value; } }

        private decimal value;
        public decimal Value { get { return value; } set { this.value = value; } }


        public override Dictionary<string, string> FieldSet()
        {
            Dictionary<string, string> r = base.FieldSet();
            r.Add("description", this.description);
            r.Add("unit", this.unit);
            r.Add("value", this.value.ToString());
            return r;
        }

        public Service(Database db, string id="")
            : base(db, "services", "Dienstleistungen", id)
        {
            if (String.IsNullOrEmpty(id))
            {
                base.Name = "";
                this.description = "";
                this.unit = "";
                this.value = 0;
            }
            else
            {
                Result d = base.DB.getRow(base.Tablename, new string[] { "name", "description", "unit", "value" }, "`id`='" + id + "'", "", 1);
                base.Name = d.FirstRow["name"];
                this.description = d.FirstRow["description"];
                this.unit = d.FirstRow["unit"];
                this.value = Decimal.Parse(d.FirstRow["value"]);
            }
        }
        public override CRUDable createObject(Database db, string id)
        {
            return new Service(db, id);
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
