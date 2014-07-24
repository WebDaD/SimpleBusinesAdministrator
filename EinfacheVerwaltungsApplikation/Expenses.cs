using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Export;
using WebDaD.Toolkit.Database;
using System.Data;

namespace ManageAdministerExalt.Classes
{
    class Expenses : Exportable
    {
        private Database db;

        private List<Expense> expenses;

        public Expenses(Database db,string year)
        {
            this.db = db;
            expenses = new List<Expense>();
            List<List<string>> d = new List<List<string>>();
            if (year == "Alle")
            {
                d = db.getRow(Expense.table, new string[] { "id" }, "", "edate ASC");
            }
            else
            {
                d = db.getRow(Expense.table, new string[] { "id" }, "strftime('%Y',edate)='" + year + "'", "edate ASC");
            }
            
            foreach (List<string> item in d)
            {
                expenses.Add(new Expense(db, item[0]));
            }
        }

        public Content ToContent()
        {
            Content c = new Content(DataType.Table);

            DataTable t = new DataTable();
            t.Columns.Add("ID");
            t.Columns.Add("Name");
            t.Columns.Add("Datum");
            t.Columns.Add("Wert");

            Decimal sum = 0;
            foreach (Expense item in expenses)
            {
                t.Rows.Add(new string[] { item.NiceID, item.Name,item.Date.ToString("dd.MM.yyyy"),"€ "+item.Value });
                sum += item.Value;
            }

            t.Rows.Add(new string[] { "---", "---", "---", "---" });
            t.Rows.Add(new string[] { "", "", "Summe", "€ "+sum });

            c.Table = t;
            return c;
        }

        public string DataName()
        {
            return "Liste der Ausgaben";
        }

        public string Filename()
        {
            return "expenses";
        }
    }
}
