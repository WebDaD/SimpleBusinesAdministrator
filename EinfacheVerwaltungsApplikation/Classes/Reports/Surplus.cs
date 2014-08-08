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
            //TODO: use year!
            //TODO: max rows, payed job-bills left, expenses right, sum lines each, full sumn line
            throw new NotImplementedException();
        }
    }
}
