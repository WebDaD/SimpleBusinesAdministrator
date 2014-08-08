using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Export;
using WebDaD.Toolkit.Database;
using System.Data;
using ManageAdministerExalt.Classes.Reports;

namespace ManageAdministerExalt.Classes
{
    public abstract class Report : Exportable
    {
        private Database db;
        public Database DB { get { return this.db; } }

        private string reportname;

        private string year;
        public string Year { get { return year; } }

        public static Dictionary<int, string> Reports
        {
            get
            {
                Dictionary<int, string> r = new Dictionary<int, string>();
                r.Add(1, "Einnahmen-Überschuss-Rechnung");
                //TODO: Every Report here
                return r;
            }
        }

        public Report(Database db, string reportname, string year)
        {
            this.db = db;
            this.reportname = reportname;
            this.year = year;
        }

        public abstract DataTable GetData();

        public static Report GetReport(Database db, int id)
        {
            switch (id)
            {
                case 1: return new Surplus(db);
                //TODO: reports also here
                default: return null;
            }
        }

        public DataTable ChangeYear(string year)
        {
            if (year == "Alle") this.year = "";
            else this.year = year;
            return GetData();
        }

        public Content ToContent(ExportCount c)
        {
            if (c == ExportCount.SINGLE)
            {
                return new ContentTable(DataType.Table, GetData());
            }
            else
            {
                DataTable t = new DataTable();
                t.Columns.Add("ID");
                t.Columns.Add("Name");

                foreach (KeyValuePair<int, string> item in Report.Reports)
                {
                    t.Rows.Add(new string[] { item.Key.ToString(), item.Value });
                }

                Content ct = new ContentTable(DataType.Table, t);
                return ct;
            }
        }

        public string DataName(ExportCount c)
        {
            if (c == ExportCount.SINGLE) return this.reportname;
            else return "Liste der Reports";
        }

        public string Filename(ExportCount c)
        {
            if (c == ExportCount.SINGLE) return this.reportname.ToLower();
            else return "reports";
        }
    }
}
