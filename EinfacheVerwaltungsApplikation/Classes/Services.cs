using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Export;
using WebDaD.Toolkit.Database;

namespace ManageAdministerExalt.Classes
{
    public class Services : Exportable
    {
         private Database db;

        private List<Service> services;

        public Services(Database db)
        {
            this.db = db;
            services = new List<Service>();
            List<List<string>> d = new List<List<string>>();
            d = db.getRow(Term.table, new string[] { "id" }, "`active`='1'", "id ASC");
            foreach (List<string> item in d)
            {
                services.Add(new Service(db, item[0]));
            }
        }

        public Content ToContent()
        {
            throw new NotImplementedException();
        }

        public string DataName()
        {
            return "Liste der Dienste";
        }

        public string Filename()
        {
            return "services";
        }
    }
}
