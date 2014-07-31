using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Export;
using System.Data;

namespace ManageAdministerExalt.Classes
{
public class Term : Joinable, Exportable, CRUDable
    {
        public static string TableName = "terms";

        private Database db;
        private string id;

        private string name;
        public string Name { get { return name; } set { name = value; } }

        private string description;
        public string Description { get { return description; } set { description = value; } }

        private int ordering;
        public int Ordering { get { return ordering; } set { ordering = value; } }

        public string NiceID
        {
            get
            {
                return Config.CreateNiceID(Config.IDFormating["term"], ordering.ToString());
            }
        }

        public Dictionary<string, string> FieldSet
        {
            get
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                r.Add("name", this.name);
                r.Add("description", this.description);
                r.Add("ordering", this.ordering.ToString());
                r.Add("active", "1");
                return r;
            }
        }

        public Term(Database db)
        {
            this.db = db;
            this.id = "";
            this.name = "";
            this.description = "";
            this.ordering = 0;
        }

        public Term(Database db, string ordering)
        {
            this.db = db;
            Result d = this.db.getRow(Term.TableName, new string[] { "id", "name", "description", "ordering" }, "`ordering`='" + id + "'", "ordering ASC", 1);
            this.id = d.FirstRow["id"];
            this.name = d.FirstRow["name"];
            this.description = d.FirstRow["description"];
            this.ordering = Convert.ToInt32(d.FirstRow["ordering"]);

        }
        public string GetJoinOn(Joinable jointable)
        {
            return "id";
        }

        public string GetTableName()
        {
            return Term.TableName;
        }

        public List<string> GetFields()
        {
            List<string> f = new List<string>();
            foreach (KeyValuePair<string, string> item in this.FieldSet)
            {
                f.Add(Term.TableName + "." + Term.TableName + "_" + item.Key);
            }
            return f;
        }

        public Content ToContent(ExportCount c)
        {
            if (c == ExportCount.MULTI)
            {
                Content ct = new ContentParagraphs(DataType.Paragraphs, this.GetIDList());
                return ct;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public string DataName(ExportCount c)
        {
            if (c == ExportCount.MULTI)
            {
                return "Allgemeine Geschäftsbedingungen";
            }
            else
            {
                return this.NiceID + " - " + this.Name;
            }
        }

        public string Filename(ExportCount c)
        {
            if (c == ExportCount.MULTI)
            {
                return "agb";
            }
            else
            {
                return this.NiceID;
            }
        }

        public bool Save()
        {
            bool ok = true;
            if (String.IsNullOrEmpty(this.id))
            {
                ok = db.Insert(Term.TableName, this.FieldSet);
            }
            else
            {
                ok = db.Update(Term.TableName, this.FieldSet, "`id`='" + this.id + "'");

            }

            return ok;
        }

        public bool Delete()
        {
            bool ok = true;
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            tmp.Add("active", "0");

            ok = db.Update(Term.TableName, tmp, "`id`='" + this.id + "'");
            return ok;
        }

        public CRUDable New()
        {
            return new Term(this.db);
        }

        public Dictionary<string, string> GetIDList()
        {
            Result d = this.db.getRow(Term.TableName, new string[] { "ordering", "name" }, "`active`='1'","ordering ASC");
            if (d.RowCount < 1) return null;

            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (Row item in d.Rows)
            {
                r.Add(item.Cells["ordering"], item.Cells["name"]);
            }

            if (r.Count > 0) return r;
            else return null;
        }

        public CRUDable GetSingleInstance(string id)
        {
            return new Term(this.db, id);
        }

        public List<CRUDable> GetFullList()
        {
            List<CRUDable> terms = new List<CRUDable>();
            Result d = this.db.getRow(Term.TableName, new string[] { "ordering" }, "`active`='1'", "ordering ASC");
            foreach (Row row in d.Rows)
            {
                terms.Add(new Term(db, row.Cells["ordering"]));
            }
            return terms;
        }

        public bool Move(int direction)
        {
            bool ok = true;
            Dictionary<string, string> tmp = new Dictionary<string, string>();

            //get ordering target
            int order_now = this.ordering;
            int order_new = order_now + direction;

            //get id of target
            string target_id = db.getValue(Term.TableName, "id", "`ordering`=" + order_new.ToString());


            //set ordering of this to order_new
            tmp.Add("ordering", order_new.ToString());
            ok = db.Update(Term.TableName, tmp, "`id`='" + this.id + "'");
            this.ordering = order_new;

            //set ordering of target to order_now
            tmp.Clear();
            tmp.Add("ordering", order_now.ToString());
            ok = db.Update(Term.TableName, tmp, "`id`='" + target_id + "'");

            return ok;
        }

        public static int MOVE_UP = -1;
        public static int MOVE_DOWN = 1;
    }
}
