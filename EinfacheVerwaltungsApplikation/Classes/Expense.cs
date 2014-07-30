using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Export;
using WebDaD.Toolkit.Database;
using System.IO;

namespace ManageAdministerExalt.Classes
{
    class Expense : Exportable, Joinable, CRUDable
    {
        public static readonly string table = "expenses";

        private Database db;
        private string id;

        public Expense(Database db, string id)
        {
            this.db = db;
            List<List<string>> d = new List<List<string>>();
            d = this.db.getRow(Expense.table, new string[] { "id", "name", "edate", "value", "attachment" }, "`id`='" + id + "'", "", 1);
            this.id = d[0][0];
            this.name = d[0][1];
            this.date = DateTime.Parse(d[0][2]);
            this.value = Convert.ToDecimal(d[0][3]);
            this.attachment = d[0][4];
        }

        public Expense(Database db)
        {
            this.db = db;
            this.id = "";
            this.name = "";
            this.date = DateTime.Now;
            this.value = 0;
            this.attachment = "";
        }
        private string name;
        public string Name { get { return name; } set { name = value; } }

        private DateTime date;
        public DateTime Date { get { return date; } set { date = value; } }

        private decimal value;
        public decimal Value { get { return value; } set { this.value = value; } }

        private string attachment;
        public string Attachment { get { return attachment; } set { attachment = value; } }
        private string insert_attachment="";

        public Dictionary<string, string> FieldSet
        {
            get
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                r.Add("name", this.name);
                r.Add("edate", this.date.ToString("yyyy-MM-dd HH:mm:ss"));
                r.Add("value", this.value.ToString());
                r.Add("attachment", this.insert_attachment);
                return r;
            }
        }

        internal bool Save()
        {
           

            this.insert_attachment = Path.GetFileName(this.attachment);

            bool ok = true;
            if (String.IsNullOrEmpty(this.id))
            {
                ok = db.Insert(Expense.table, this.FieldSet);
                this.id = db.GetLastInsertedID();
            }
            else
            {
                ok = db.Update(Expense.table, this.FieldSet, "`id`='" + this.id + "'");
            }
            if (ok)
            {
                
                string targetpath = Config.BasePath + Path.DirectorySeparatorChar + "expenses" + Path.DirectorySeparatorChar + this.NiceID + Path.DirectorySeparatorChar;
                if (this.attachment != Path.GetFileName(this.attachment))
                {
                    if (!Directory.Exists(targetpath)) Directory.CreateDirectory(targetpath);
                    File.Copy(this.attachment, targetpath + Path.GetFileName(this.attachment), true);
                }
            }
            return ok;
        }

        internal static Dictionary<string, string> getExpenses(Database db, string year)
        {
            List<List<string>> d = new List<List<string>>();
            if (year == "Alle")
            {
                d = db.getRow(Expense.table, new string[] { "id","date(edate)","name" }, "", "edate ASC");
            }
            else
            {
                d = db.getRow(Expense.table, new string[] { "id", "date(edate)", "name" }, "strftime('%Y',edate)='" + year + "'", "edate ASC");
            }

            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (List<string> item in d)
            {
                r.Add(item[0], item[1]+"|"+item[2]);
            }

            if (d.Count > 0) return r;
            else return null;
        }

        /// <summary>
        /// Gets the Years in the Database as List of Strings
        /// </summary>
        /// <param name="db">A Database Object</param>
        /// <returns>A List of Strings</returns>
        internal static List<string> GetExpenseYears(Database db)
        {
            List<List<string>> d = new List<List<string>>();
            List<string> r = new List<string>();
            d = db.getRow(Expense.table, new string[] { "strftime('%Y',edate)" }, "", "edate ASC");
            foreach (List<string> item in d)
            {
                r.Add(item[0]);
            }
            r = r.Distinct<string>().ToList<string>();
            if (d.Count > 0) return r;
            else return null;
        }

        public string NiceID
        {
            get
            {
                return "E" + id.PadLeft(5, '0');
            }
        }

        internal bool Delete()
        {
            bool ok = true;


            ok = db.Execute("DELETE FROM " + Expense.table + " WHERE `id`=" + this.id);
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
