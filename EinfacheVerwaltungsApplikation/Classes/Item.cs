using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Export;
using WebDaD.Toolkit.Database;
using System.Data;

namespace ManageAdministerExalt.Classes
{
    public class Item : Module
    {
        private decimal value;
        public decimal Value { get { return value; } set { this.value = value; } }

        private DataTable log;
        public DataTable Log { get { return log; } }

        public int Count
        {
            get
            {
                int sum = 0;
                foreach (DataRow r in this.log.Rows)
                {
                    sum += Convert.ToInt32(r[2].ToString());
                }
                return sum;
            }
        }

        public decimal Value_Sum
        {
            get
            {
                return Count * value;
            }
        }

        public Item(Database db, string id="") : base(db,"items","Lagerwerte",id)
        {
            if (String.IsNullOrEmpty(id))
            {
                base.Name = "";
                this.value = 0;
            }
            else
            {
                Result d = base.DB.getRow(base.Tablename, new string[] { "name", "value_per_unit" }, "`id`='" + id + "'", "", 1);
                base.Name = d.FirstRow["name"];
                this.value = Decimal.Parse(d.FirstRow["value_per_unit"]);
                this.log = getLog();
            }
            
        }
        public override void Unload()
        {
            base.ID = "";
            base.Name = "";
            this.value = 0;
            this.log = null;
        }
        public override bool Load(string id)
        {
            base.ID = id;
            Result d = base.DB.getRow(base.Tablename, new string[] { "name", "value_per_unit" }, "`id`='" + id + "'", "", 1);
            if (d.RowCount > 0)
            {
                base.Name = d.FirstRow["name"];
                this.value = Decimal.Parse(d.FirstRow["value_per_unit"]);
                this.log = getLog();
                return true;
            }
            else return false;
        }
        private DataTable getLog()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum");
            dt.Columns.Add("Typ");
            dt.Columns.Add("Anzahl");
            Result d = base.DB.getRow("item_log", new string[] { "log_date", "change_value" }, "`item_id`='" + base.ID + "'", "");

            foreach (Row row in d.Rows)
            {
                string type = "";
                int count = Convert.ToInt32(row.Cells["change_value"]);
                if (count > 0) type = "Eingang";
                else type = "Ausgang";
                count = Math.Abs(count);
                dt.Rows.Add(new string[]{row.Cells["log_date"],type,count.ToString()});
            }

            return dt;
        }

        public bool AddEntry(int count)
        {
            Dictionary<string, string> fs = new Dictionary<string, string>();
            fs.Add("item_id", base.ID);
            fs.Add("log_date", DateTime.Now.ToString("yyyy-MM-dd"));
            fs.Add("change_value", count.ToString());
            return base.DB.Insert("item_log", fs);
        }

        public override Dictionary<string, string> FieldSet()
        {
            Dictionary<string, string> r = base.FieldSet();
            r.Add("value_per_unit", this.value.ToString());
            return r;
        }

        public override CRUDable createObject(Database db, string id)
        {
            return new Item(db, id);
        }

        public override Content ToContent(ExportCount c)
        {
            throw new NotImplementedException();
        }

        public override string WorkerName
        {
            get { return ""; }
        }

        public override string Adress
        {
            get { throw new NotImplementedException(); }
        }

        public override string DateCreated
        {
            get { throw new NotImplementedException(); }
        }

        public override string DateSecond
        {
            get { throw new NotImplementedException(); }
        }
    }
}
