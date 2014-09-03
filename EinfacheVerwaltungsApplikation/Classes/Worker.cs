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
    public class Worker : Module
    {
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



        public override Dictionary<string, string> FieldSet()
        {

            Dictionary<string, string> r = base.FieldSet();
            r.Add("dateofbirth", this.dateofbirth.ToString("yyyy-MM-dd"));
            r.Add("works_since", this.works_since.ToString("yyyy-MM-dd"));
            r.Add("salary", this.salary.ToString().Replace(",", "."));
            r.Add("hoursperweek", this.hoursperweek.ToString().Replace(",", "."));
            r.Add("streetnr", this.streetnr);
            r.Add("plz", this.plz);
            r.Add("city", this.city);
            r.Add("email", this.email);
            r.Add("phone", this.phone);
            r.Add("mobile", this.mobile);
            return r;
        }

        public Worker(Database db, string id="") : base(db,"workers","Mitarbeiter",id)
        {
            if (String.IsNullOrEmpty(id))
            {
                base.Name = "";
                this.dateofbirth = new DateTime();
                this.works_since = new DateTime();
                this.salary = 0;
                this.hoursperweek = 0;
                this.streetnr = "";
                this.plz = "";
                this.city = "";
                this.email = "";
                this.phone = "";
                this.mobile = "";
            }
            else
            {
                Result d = base.DB.getRow(base.Tablename, new string[] { "name","dateofbirth", "works_since", "salary", "streetnr", "hoursperweek", "plz", "city", "email", "phone", "mobile" }, "`id`='" + id + "'", "", 1);
                base.Name = d.FirstRow["name"];
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
            
        }
        public override void Unload()
        {
            base.ID = "";
            base.Name = "";
            this.dateofbirth = new DateTime();
            this.works_since = new DateTime();
            this.salary = 0;
            this.hoursperweek = 0;
            this.streetnr = "";
            this.plz = "";
            this.city = "";
            this.email = "";
            this.phone = "";
            this.mobile = "";
        }
        public override bool Load(string id)
        {
            base.ID = id;
            Result d = base.DB.getRow(base.Tablename, new string[] { "name", "dateofbirth", "works_since", "salary", "streetnr", "hoursperweek", "plz", "city", "email", "phone", "mobile" }, "`id`='" + id + "'", "", 1);
            if (d.RowCount > 0)
            {
                base.Name = d.FirstRow["name"];
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
                return true;
            }
            else return false;
        }

        public override CRUDable createObject(Database db, string id)
        {
            return new Worker(db, id);
        }

        public override Content ToContent(ExportCount c)
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
            bool ok = base.Save();
           
            if (ok)
            {
                if (String.IsNullOrEmpty(base.ID)) base.ID = base.DB.GetLastInsertedID();
                string target = base.Basepath + this.NiceID;
                if (!Directory.Exists(target)) Directory.CreateDirectory(target);
            }
            return ok;
        }

        //Report Properties
        public decimal R_Jobs
        {
            get
            {
                return 0; //TODO: From Database
            }
        }
        public decimal R_Jobs_Avg
        {
            get
            {
                return 400.32M; //TODO: From Database
            }
        }

        public decimal R_Jobs_Sum
        {
            get
            {
                return 50000; //TODO: From Database
            }
        }
        public int R_Work_Months
        {
            get
            {
                return DateTime.Now.AddMonths(1).Month - WorksSince.Month;
            }
        }
        public decimal R_Salary_Sum
        {
            get
            {
                return R_Work_Months * salary;
            }
        }
        public decimal R_Value
        {
            get
            {
                decimal r = R_Jobs_Sum - R_Salary_Sum;
                return r;
            }
        }
        
    }
}
