using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManageAdministerExalt.Classes
{
    class Discount
    {
        public static Dictionary<int, string> TYPES{
         get{
             Dictionary<int, string> t = new Dictionary<int, string>();
             t.Add(0, "Prozent");
             t.Add(1, "Festbetrag");
             return t;
         }
        }

        private WebDaD.Toolkit.Database.Database db;
        private string id;

        public Discount(WebDaD.Toolkit.Database.Database db)
        {
            // TODO: Complete member initialization
            this.db = db;
        }

        public Discount(WebDaD.Toolkit.Database.Database db, string id)
        {
            // TODO: Complete member initialization
            this.db = db;
            this.id = id;
        }
        internal static Dictionary<string, string> getDiscounts(WebDaD.Toolkit.Database.Database db)
        {
            throw new NotImplementedException();
        }

        public string Name { get; set; }

        public string NiceID { get; set; }

        internal void Delete()
        {
            throw new NotImplementedException();
        }

        public decimal Value { get; set; }

        public object TypeName { get; set; }

        internal bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
