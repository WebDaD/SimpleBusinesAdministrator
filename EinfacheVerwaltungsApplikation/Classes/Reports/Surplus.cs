using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using System.Data;

namespace ManageAdministerExalt.Classes.Reports
{
    public class Surplus : Report
    {
        public Surplus(Database db,string year="") : base(db, "Einnahmen-Überschuss-Rechnung",year) { }
        public override DataTable GetData()
        {
            DataTable table = new DataTable();

            //Einnahmen
            table.Columns.Add("E-Datum");
            table.Columns.Add("E-Punkt");
            table.Columns.Add("E-Wert");

            //Ausgaben
            table.Columns.Add("A-Datum");
            table.Columns.Add("A-Punkt");
            table.Columns.Add("A-Wert");

            //header Row
            table.Rows.Add(new string[] { "Einnahmen", "","","Ausgaben","","" });

            //get expenses
            decimal expense_sum = 0;
            Expense e = new Expense(base.DB);
            List<CRUDable> expenses = e.GetFullList();
            List<Expense> expense = new List<Expense>();
            foreach (Expense item in expenses)
            {
                if (!String.IsNullOrEmpty(base.Year) && base.Year != item.EDate.Year.ToString()) continue;
                expense.Add(item);
            }
            //sum
            foreach (Expense item in expense)
            {
                expense_sum += item.Value;
            }

            // get income from bills
            decimal income_sum = 0;
            Bill b = new Bill(base.DB);
            List<CRUDable> bills = b.GetFullList();
            List<Bill> income = new List<Bill>();
            foreach (Bill item in bills)
            {
                if (!String.IsNullOrEmpty(base.Year) && base.Year != item.Bill_Sent.Year.ToString()) continue;
                if (item.Payment_Received > item.Bill_Sent)
                {
                    income.Add(item);
                } 
                
            }
            foreach (Bill item in income)
            {
                income_sum += item.Value;
            }

            // get final sum
            decimal final_sum = income_sum - expense_sum;

            //write into table
            int max = income.Count > expense.Count ? income.Count : expense.Count;
            for (int i = 0; i < max; i++)
            {
                string[] line = new string[6];

                if (i < income.Count)
                {
                    line[0] = income[i].Payment_Received.ToString("dd.MM.yyyy");
                    line[1] = income[i].NiceID;
                    line[2] = income[i].Value.ToString("C");
                }
                else
                {
                    line[0] = "";
                    line[1] = "";
                    line[2] = "";
                }

                if (i < expense.Count)
                {
                    line[3] = expense[i].EDate.ToString("dd.MM.yyyy");
                    line[4] = expense[i].NiceID;
                    line[5] = expense[i].Value.ToString("C");
                }
                else
                {
                    line[3] = "";
                    line[4] = "";
                    line[5] = "";
                }

                table.Rows.Add(line);
            }

            //Add Sum lines
            table.Rows.Add(new string[] { "", "Summe:", income_sum.ToString("C"), "", "Summe", expense_sum.ToString("C") });
            table.Rows.Add(new string[]{"","","Gesamt:",final_sum.ToString("C"),"",""});

            return table;
        }
    }
}
