using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Export;
using WebDaD.Toolkit.Database;

namespace ManageAdministerExalt.Classes
{
    public class Terms : Exportable
    {
        private Database db;

        private List<Term> terms;

        public Terms(Database db)
        {
            this.db = db;
            terms = new List<Term>();
            List<List<string>> d = new List<List<string>>();
            d = db.getRow(Term.table, new string[] { "ordering" }, "`active`='1'", "ordering ASC");
            foreach (List<string> item in d)
            {
                terms.Add(new Term(db, item[0]));
            }
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
    }
}
