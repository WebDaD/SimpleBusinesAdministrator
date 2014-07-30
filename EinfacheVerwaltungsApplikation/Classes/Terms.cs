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
            Result d = db.getRow(Term.table, new string[] { "ordering" }, "`active`='1'", "ordering ASC");
            foreach (Row row in d.Rows)
            {
                terms.Add(new Term(db, row.Cells[0].Content));
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
