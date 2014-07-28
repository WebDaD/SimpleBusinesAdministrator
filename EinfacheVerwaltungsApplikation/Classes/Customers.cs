using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Export;
using WebDaD.Toolkit.Database;
using System.Data;

namespace ManageAdministerExalt.Classes
{
    public class Customers : Exportable
    {
         private Database db;

        private List<Customer> customers;

        public Customers(Database db)
        {
            this.db = db;
            customers = new List<Customer>();
            List<List<string>> d = new List<List<string>>();
            d = db.getRow(Customer.TableName, new string[] { "id" }, "`active`='1'", "id ASC");
            foreach (List<string> item in d)
            {
                customers.Add(new Customer(db, item[0]));
            }
        }

        public Content ToContent()
        {
            Content c = new Content(DataType.Table);

            DataTable t = new DataTable();
            t.Columns.Add("ID");
            t.Columns.Add("Name");

            foreach (Customer item in customers)
            {
                t.Rows.Add(new string[] { item.KundenNummer, item.Name });
            }

            c.Table = t;
            return c;
        }

        public string DataName()
        {
            return "Liste der Kunden";
        }

        public string Filename()
        {
            return "customers";
        }
    }
}
