using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Export;

namespace ManageAdministerExalt.Classes
{
    /// <summary>
    /// Holds Information on one Term and has static classes to getTerms
    /// </summary>
    public class Term : Exportable
    {
        public static Dictionary<string, string> getTerms(Database db)
        {
            List<List<string>> d = new List<List<string>>();
            d = db.getRow(Term.table, new string[] { "ordering", "title" }, "`active`='1'","ordering ASC");

            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (List<string> item in d)
            {
                r.Add(item[0], item[1]);
            }

            if (d.Count > 0) return r;
            else return null;
        }



        private Database db;
        public static string table = "terms";
        public static readonly int MOVE_UP = -1;
        public static readonly int MOVE_DOWN = 1;

        private string id;
        public string ID { get { return id; } set { id = value; } }

        public string NiceID
        {
            get { return "§ " + this.ordering; }
        }

        private string title;
        public string Title { get { return title; } set { title = value; } }

        private string content;
        public string Content { get { return content; } set { content = value; } }

        private string ordering;
        public string Ordering { get { return ordering; } set { ordering = value; } }

        public Dictionary<string, string> FieldSet 
        {
            get 
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                r.Add("title", this.title);
                r.Add("content", this.content);
                if (this.ordering == "")
                {
                    string high_order = this.db.getValue(Term.table, "MAX(ordering)", "1=1");
                    int t_order = Int32.Parse(high_order) + 1;
                    r.Add("ordering", t_order.ToString());
                }
                else
                {
                    r.Add("ordering", this.ordering);
                }
                r.Add("active", "1");
                return r;
            } 
        }

        public Term(Database db, string ordering)
        {
            this.db = db;
            List<List<string>> d = new List<List<string>>();
            d = this.db.getRow(Term.table, new string[] { "id", "title", "content", "ordering" }, "`ordering`='" + ordering + "'", "", 1);
            this.id = d[0][0];
            this.title = d[0][1];
            this.content = d[0][2];
            this.ordering = d[0][3];
        }

        /// <summary>
        /// Empty Service for Saving purposes or Errors
        /// </summary>
        public Term(Database db)
        {
            this.db = db;
            this.id = "";
            this.title = "";
            this.content = "";
            this.ordering = "";
        }

        public bool Save()
        {
            bool ok = true;
            if (String.IsNullOrEmpty(this.id))
            {
                ok = db.Insert(Term.table, this.FieldSet);
            }
            else
            {
                ok = db.Update(Term.table, this.FieldSet, "`id`='" + this.id + "'");

            }
            return ok;
        }

        public bool Delete()
        {
            bool ok = true;
            string t_order = this.ordering;
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            tmp.Add("active", "0");
            tmp.Add("ordering", "0");

            ok = db.Update(Term.table, tmp, "`id`='" + this.id + "'");

            //Every item with an ordering above this ordering must have ordering--
            ok = db.Execute("UPDATE "+Term.table+" SET `ordering`=`ordering`-1 WHERE `ordering`>"+t_order);

            return ok;
        }

        public bool Move(int direction)
        {
            bool ok = true;
            Dictionary<string, string> tmp = new Dictionary<string, string>();

            //get ordering target
            int order_now = Int32.Parse(ordering);
            int order_new = order_now + direction;

            //get id of target
            string target_id = db.getValue(Term.table, "id", "`ordering`=" + order_new.ToString());


            //set ordering of this to order_new
            tmp.Add("ordering", order_new.ToString());
            ok = db.Update(Term.table, tmp, "`id`='" + this.id + "'");
            this.ordering = order_new.ToString();

            //set ordering of target to order_now
            tmp.Clear();
            tmp.Add("ordering", order_now.ToString());
            ok = db.Update(Term.table, tmp, "`id`='" + target_id + "'");

            return ok;
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
