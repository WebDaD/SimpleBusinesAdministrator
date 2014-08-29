using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Export;
using System.Data;

namespace ManageAdministerExalt.Classes
{
public class Term : Module
    {

        private string description;
        public string Description { get { return description; } set { description = value; } }

        private int ordering;
        public int Ordering { get { return ordering; } set { ordering = value; } }

        public override Dictionary<string, string> FieldSet()
        {
                Dictionary<string, string> r = base.FieldSet();
                r.Add("description", this.description);
                r.Add("ordering", this.ordering.ToString());
                return r;
        }

        public Term(Database db, string id="") : base(db,"terms","Geschäftsbedingungen",id)
        {
            if (String.IsNullOrEmpty(id))
            {
                base.Name = "";
                this.description = "";
                this.ordering = 0;
            }
            else
            {
                Result d = base.DB.getRow(base.Tablename, new string[] {"name", "description", "ordering" }, "`id`='" + id + "'", "ordering ASC", 1);
                base.Name = d.FirstRow["name"];
                this.description = d.FirstRow["description"];
                this.ordering = Convert.ToInt32(d.FirstRow["ordering"]);
            }
        }

        public override CRUDable createObject(Database db, string id)
        {
            return new Term(db, id);
        }

        public override Content ToContent(ExportCount c)
        {
            if (c == ExportCount.MULTI)
            {
                Content ct = new ContentParagraphs(DataType.Paragraphs, this.GetIDList()); //TODO: this may not be in the right order...
                return ct;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override string WorkerName
        {
            get { return ""; }
        }

        public override string Adress
        {
            get { throw new NotImplementedException(); }
        }

        public override string DateCreated
        {
            get { throw new NotImplementedException(); }
        }

        public override string DateSecond
        {
            get { throw new NotImplementedException(); }
        }

        public bool Move(int direction)
        {
            bool ok = true;
            Dictionary<string, string> tmp = new Dictionary<string, string>();

            //get ordering target
            int order_now = this.ordering;
            int order_new = order_now + direction;

            //get id of target
            string target_id = base.DB.getValue(base.Tablename, "id", "`ordering`=" + order_new.ToString());


            //set ordering of this to order_new
            tmp.Add("ordering", order_new.ToString());
            ok = base.DB.Update(base.Tablename, tmp, "`id`='" + base.ID + "'");
            this.ordering = order_new;

            //set ordering of target to order_now
            tmp.Clear();
            tmp.Add("ordering", order_now.ToString());
            ok = base.DB.Update(base.Tablename, tmp, "`id`='" + target_id + "'");

            return ok;
        }

        public static int MOVE_UP = -1;
        public static int MOVE_DOWN = 1;
    }
}
