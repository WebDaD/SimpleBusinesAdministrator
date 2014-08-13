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
        public string Text { get { return this.reportname; } }

        private string year;
        public string Year { get { return year; } }

        public Report Value { get { return this; } }

        public List<Report> Reports
        {
            get
            {
                List<Report> r = new List<Report>();
                r.Add(new Surplus(this.db));
                //TODO: Every Report here
                return r;
            }
        }

        public Report SelectedReport(string reportname)
        {
            foreach (Report item in this.Reports)
            {
                if (reportname == item.reportname)
                {
                    return item;
                }
            }
            return null;
        }

        public Report(Database db, string reportname, string year)
        {
            this.db = db;
            this.reportname = reportname;
            this.year = year;
        }

        public abstract DataTable GetData();

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
                t.Columns.Add("Name");

                foreach (Report item in Reports)
                {
                    t.Rows.Add(new string[] { item.Text });
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
