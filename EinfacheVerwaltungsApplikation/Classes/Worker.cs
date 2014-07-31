using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Export;
using WebDaD.Toolkit.Database;
using System.IO;
using System.Data;

namespace ManageAdministerExalt.Classes
{
    public class Worker : Exportable, Joinable, CRUDable
    {
        

        private Database db;
        public static string TableName = "workers";

        private string id;

        private string name;
        public string Name { get { return name; } set { name = value; } }

        private DateTime dateofbirth;
        public DateTime DateOfBirth { get { return dateofbirth; } set { dateofbirth = value; } }

        private DateTime works_since;
        public DateTime WorksSince { get { return works_since; } set { works_since = value; } }

        private decimal salary;
        public decimal Salary { get { return salary; } set { salary = value; } }

        private decimal hoursperweek;
        public decimal HoursPerWeek { get { return hoursperweek; } set { hoursperweek = value; } }

        private string streetnr;
        public string StreetNr { get { return streetnr; } set { streetnr = value; } }

        private string plz;
        public string PLZ { get { return plz; } set { plz = value; } }

        private string city;
        public string City { get { return city; } set { city = value; } }

        private string email;
        public string EMail { get { return email; } set { email = value; } }

        private string phone;
        public string Phone { get { return phone; } set { phone = value; } }

        private string mobile;
        public string Mobile { get { return mobile; } set { mobile = value; } }

        public string NiceID
        {
            get
            {
                return Config.CreateNiceID(Config.IDFormating["worker"], id);
            }
        }

        public Dictionary<string, string> FieldSet
        {
            get
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                r.Add("name", this.name);
                r.Add("dateofbirth", this.dateofbirth.ToString("yyyy-MM-dd HH:mm:ss"));
                r.Add("works_since", this.works_since.ToString("yyyy-MM-dd HH:mm:ss"));
                r.Add("salary", this.salary.ToString());
                r.Add("hoursperweek", this.hoursperweek.ToString());
                r.Add("streetnr", this.streetnr);
                r.Add("plz", this.plz);
                r.Add("city", this.city);
                r.Add("email", this.email);
                r.Add("phone", this.phone);
                r.Add("mobile", this.mobile);
                r.Add("active", "1");
                return r;
            }
        }

        public Worker(Database db)
        {
            this.db = db;
            this.id = "";
            this.name = "";
            this.dateofbirth = new DateTime();
            this.works_since = new DateTime();
            this.salary = 0;
            this.hoursperweek = 0;
            this.streetnr ="";
            this.plz = "";
            this.city = "";
            this.email = "";
            this.phone = "";
            this.mobile = "";
        }

        public Worker(Database db, string id)
        {
            this.db = db;
            Result d = this.db.getRow(Worker.TableName, new string[] { "id", "name", "dateofbirth", "works_since", "salary", "streetnr", "hoursperweek", "plz", "city", "email", "phone", "mobile" }, "`id`='" + id + "'", "", 1);
            this.id = d.FirstRow["id"];
            this.name = d.FirstRow["name"];
            this.dateofbirth = DateTime.Parse(d.FirstRow["dateofbirth"]);
            this.works_since = DateTime.Parse(d.FirstRow["works_since"]);
            this.salary = Decimal.Parse(d.FirstRow["salary"]);
            this.hoursperweek = Decimal.Parse(d.FirstRow["hoursperweek"]);
            this.streetnr = d.FirstRow["streetnr"];
            this.plz = d.FirstRow["plz"];
            this.city = d.FirstRow["city"];
            this.email = d.FirstRow["email"];
            this.phone = d.FirstRow["phone"];
            this.mobile = d.FirstRow["mobile"];
        }
        public Content ToContent(ExportCount c)
        {
            if (c == ExportCount.MULTI)
            {
                DataTable t = new DataTable();
                t.Columns.Add("ID");
                t.Columns.Add("Name");

                foreach (Worker item in this.GetFullList())
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
                return "Liste der Kunden";
            }
            else
            {
                return this.NiceID+" - "+this.Name;
            }
        }

        public string Filename(ExportCount c)
        {
            if (c == ExportCount.MULTI)
            {
                return "workers";
            }
            else
            {
                return this.NiceID;
            }
        }

        public string GetJoinOn(Joinable jointable)
        {
            return "id";
        }

        public string GetTableName()
        {
            return Worker.TableName;
        }

        public List<string> GetFields()
        {
            List<string> f = new List<string>();
            foreach (KeyValuePair<string, string> item in this.FieldSet)
            {
                f.Add(Worker.TableName + "." + Worker.TableName + "_" + item.Key);
            }
            return f;
        }

        public bool Save()
        {
            bool ok = true;
            if (String.IsNullOrEmpty(this.id))
            {
                ok = db.Insert(Worker.TableName, this.FieldSet);
            }
            else
            {
                ok = db.Update(Worker.TableName, this.FieldSet, "`id`='" + this.id + "'");

            }
            if (ok)
            {
                if (String.IsNullOrEmpty(this.id)) this.id = db.GetLastInsertedID();
                string target = Config.BasePath + Path.DirectorySeparatorChar + Config.Paths["worker"] + Path.DirectorySeparatorChar + this.NiceID;
                if (!Directory.Exists(target)) Directory.CreateDirectory(target);
            }

            return ok;
        }

        public bool Delete()
        {
            bool ok = true;
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            tmp.Add("active", "0");

            ok = db.Update(Worker.TableName, tmp, "`id`='" + this.id + "'");
            return ok;
        }

        public CRUDable New()
        {
            return new Worker(this.db);
        }

        public Dictionary<string, string> GetIDList()
        {
            Result d = this.db.getRow(Worker.TableName, new string[] { "id", "name" },"`active`='1'");
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
            return new Worker(this.db, id);
        }

        public List<CRUDable> GetFullList()
        {
            List<CRUDable> workers = new List<CRUDable>();
            Result d = this.db.getRow(Worker.TableName, new string[] { "id" }, "`active`='1'", "id ASC");
            foreach (Row row in d.Rows)
            {
                workers.Add(new Worker(db, row.Cells["id"]));
            }
            return workers;
        }

        public Dictionary<string, string> CreateReport()
        {
            Dictionary<string, string> r = new Dictionary<string, string>();

            //TODO: Write some Reports (jobs,jobs_avg,jobs_sum,work_months,salary_sum,value)
            r.Add("jobs", "0");

            r.Add("jobs_avg", "400,32");

            decimal job_sum = 50000;
            r.Add("jobs_sum", job_sum.ToString());

            int work_months = DateTime.Now.AddMonths(1).Month - WorksSince.Month;
            r.Add("work_months", work_months.ToString());

            decimal sal_sum = work_months * salary;
            r.Add("salary_sum", sal_sum.ToString());

            decimal value = job_sum/sal_sum;
            r.Add("value", value.ToString());
            return r;
        }
    }
}
