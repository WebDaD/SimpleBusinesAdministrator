using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Export;
using WebDaD.Toolkit.Database;
using System.IO;

namespace ManageAdministerExalt.Classes
{
    public class Expense : Joinable, Exportable, CRUDable
    {
        public static List<string> GetExpenseYears(Database db)
        {
            Result d = db.getRow(Expense.TableName, new string[] { "strftime('%Y',edate)" }, "`active`='1'");
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

        private Database db;

        private string id;

        private static string TableName = "expenses";

        private string name;
        public string Name { get { return name; } set { name = value; } }

        public string NiceID { get {  return Config.CreateNiceID(Config.IDFormating["expense"], id); } }

        private DateTime edate;
        public DateTime EDate { get{return edate;} set{edate=value;} }

        private decimal value;
        public decimal Value { get { return value; } set { this.value = value; } }

        private string attachment;
        public string Attachment { get { return attachment; } set { attachment = value; } }


        public Dictionary<string, string> FieldSet
        {
            get
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                r.Add("name", this.name);
                r.Add("edate", this.edate.ToString("yyyy-MM-dd HH:mm:ss"));
                r.Add("value", this.value.ToString());
                r.Add("attachment", this.attachment);
                r.Add("active", "1");
                return r;
            }
        }


        public Expense(Database db)
        {
            this.db = db;
            this.name = "";
            this.edate=DateTime.Now;
            this.value = 0;
            this.attachment = "";
        }

        public Expense(Database db, string id)
        {
            this.db = db;
            this.id = id;
            Result d = this.db.getRow(Expense.TableName, new string[] { "id", "name", "edate", "value", "attachment" }, "`id`='" + id + "'", "", 1);
            this.id = d.FirstRow["id"];
            this.name = d.FirstRow["name"];
            this.edate = DateTime.Parse(d.FirstRow["edate"]);
            this.value = Decimal.Parse(d.FirstRow["dvalue"]);
            this.attachment = d.FirstRow["attachment"];
        }
        public Dictionary<string, string> GetIDList()
        {
            Result d = this.db.getRow(Expense.TableName, new string[] { "id", "name" }, "`active`='1'","edate ASC");
            if (d.RowCount < 1) return null;

            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (Row item in d.Rows)
            {
                r.Add(item.Cells["id"], item.Cells["name"]);
            }

            if (r.Count > 0) return r;
            else return null;
        }

        public Dictionary<string, string> GetIDList(string year)
        {
            Result d;
            if (year == "Alle")
            {
                d = this.db.getRow(Expense.TableName, new string[] { "id", "name" }, "`active`='1'", "edate ASC");
            }
            else
            {
                d = this.db.getRow(Expense.TableName, new string[] { "id", "name" }, "`active`='1' AND strftime('%Y',edate)='" + year + "'", "edate ASC");
            }
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
            return Expense.TableName;
        }

        public List<string> GetFields()
        {
            List<string> f = new List<string>();
            foreach (KeyValuePair<string, string> item in this.FieldSet)
            {
                f.Add(Expense.TableName + "." + Expense.TableName + "_" + item.Key);
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

            string file="", filename = "";
            if (this.attachment.Contains(Path.DirectorySeparatorChar))
            {
                file = this.attachment;
                filename = Path.GetFileName(this.attachment);
                this.FieldSet["attachment"] = filename;
            }
            if (String.IsNullOrEmpty(this.id))
            {
                ok = db.Insert(Expense.TableName, this.FieldSet);
            }
            else
            {
                ok = db.Update(Expense.TableName, this.FieldSet, "`id`='" + this.id + "'");

            }
            if (ok)
            {
                if (String.IsNullOrEmpty(this.id)) this.id = db.GetLastInsertedID();
                string target = Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["expense"] + Path.DirectorySeparatorChar + this.NiceID;
                if (!Directory.Exists(target)) Directory.CreateDirectory(target);
                if (this.attachment.Contains(Path.DirectorySeparatorChar))
                {
                    File.Copy(file, target + Path.DirectorySeparatorChar + filename, true);
                }
            }
            return ok;
        }

        public bool Delete()
        {
            bool ok = true;
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            tmp.Add("active", "0");

            ok = db.Update(Expense.TableName, tmp, "`id`='" + this.id + "'");
            return ok;
        }

        public CRUDable New()
        {
            return new Expense(this.db);
        }

        public CRUDable GetSingleInstance(string id)
        {
            return new Expense(this.db, id);
        }

        public List<CRUDable> GetFullList()
        {
            List<CRUDable> expenses = new List<CRUDable>();
            Result d = this.db.getRow(Expense.TableName, new string[] { "id" }, "`active`='1'", "id ASC");
            foreach (Row row in d.Rows)
            {
                expenses.Add(new Expense(db, row.Cells["id"]));
            }
            return expenses;
        }
    }
}
