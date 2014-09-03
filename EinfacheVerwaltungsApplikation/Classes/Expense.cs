using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Export;
using WebDaD.Toolkit.Database;
using System.IO;
using System.Globalization;

namespace ManageAdministerExalt.Classes
{
    public class Expense : Module
    {
        public List<string> GetExpenseYears()
        {
            Result d = base.DB.getRow(base.Tablename, new string[] { "strftime('%Y',edate)" }, "`active`='1'");
            if (d.RowCount < 1) return null;

            List<string> r = new List<string>();
            foreach (Row item in d.Rows)
            {
                r.Add(item.Cells["strftime('%Y',edate)"]);
            }

            r = r.Distinct<string>().ToList<string>();

            if (r.Count > 0) return r;
            else return null;
        }


        private DateTime edate;
        public DateTime EDate { get { return edate; } set { edate = value; } }

        private decimal value;
        public decimal Value { get { return value; } set { this.value = value; } }

        private string attachment;
        public string Attachment { get { return attachment; } set { attachment = value; } }


        public override Dictionary<string, string> FieldSet()
        {
            Dictionary<string, string> r = base.FieldSet();
            r.Add("edate", this.edate.ToString("yyyy-MM-dd"));
            r.Add("value", this.value.ToString().Replace(",","."));
            r.Add("attachment", this.attachment);
            return r;
        }

        public Expense(Database db, string id="") : base(db,"expenses","Ausgaben",id)
        {
            if (String.IsNullOrEmpty(id))
            {
                base.Name = "";
                this.edate = DateTime.Now;
                this.value = 0;
                this.attachment = "";
            }
            else
            {
                Result d = base.DB.getRow(base.Tablename, new string[] { "name", "edate", "value", "attachment" }, "`id`='" + id + "'", "", 1);
                base.Name = d.FirstRow["name"];
                this.edate = DateTime.Parse(d.FirstRow["edate"]);
                this.value = Decimal.Parse(d.FirstRow["value"], NumberStyles.AllowDecimalPoint);
                this.attachment = d.FirstRow["attachment"];
            }
        }
        public override void Unload()
        {
            base.ID = "";
            base.Name = "";
            this.edate = DateTime.Now;
            this.value = 0;
            this.attachment = "";
        }
        public override bool Load(string id)
        {
            base.ID = id;
            Result d = base.DB.getRow(base.Tablename, new string[] { "name", "edate", "value", "attachment" }, "`id`='" + id + "'", "", 1);
            if (d.RowCount > 0)
            {
                base.Name = d.FirstRow["name"];
                this.edate = DateTime.Parse(d.FirstRow["edate"]);
                this.value = Decimal.Parse(d.FirstRow["value"]);
                this.attachment = d.FirstRow["attachment"];
                return true;
            }
            else return false;
        }
        public Dictionary<string, string> GetIDList(string year)
        {
            Result d;
            if (year == "Alle")
            {
                d = base.DB.getRow(base.Tablename, new string[] { "id","edate", "name" }, "`active`='1'", "edate ASC");
            }
            else
            {
                d = base.DB.getRow(base.Tablename, new string[] { "id", "edate","name" }, "`active`='1' AND strftime('%Y',edate)='" + year + "'", "edate ASC");
            }
            if (d.RowCount < 1) return null;

            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (Row item in d.Rows)
            {
                r.Add(item.Cells["id"], item.Cells["edate"]+"|"+item.Cells["name"]);
            }

            if (r.Count > 0) return r;
            else return null;
        }
        public override CRUDable createObject(Database db, string id)
        {
            return new Expense(db, id);
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
      

        public override bool Save()
        {
            string file = "", filename = "";
            if (this.attachment.Contains(Path.DirectorySeparatorChar))
            {
                file = this.attachment;
                filename = Path.GetFileName(this.attachment);
                this.attachment = filename;
            }

            bool ok = base.Save();       
            
            if (ok)
            {
                if (String.IsNullOrEmpty(base.ID)) base.ID = base.DB.GetLastInsertedID();
                string target = Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["expenses"] + Path.DirectorySeparatorChar + this.NiceID;
                if (!Directory.Exists(target)) Directory.CreateDirectory(target);
                if (this.attachment.Contains(Path.DirectorySeparatorChar))
                {
                    File.Copy(file, target + Path.DirectorySeparatorChar + filename, true);
                }
            }
            return ok;
        }

    }
}
