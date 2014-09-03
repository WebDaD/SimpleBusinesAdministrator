using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using WebDaD.Toolkit.Export;
using System.IO;

namespace ManageAdministerExalt.Classes
{
    public abstract class Module : Joinable,CRUDable,Exportable
    {
        private Database db;
        public Database DB { get { return db; } }

        private string tablename;
        public string Tablename { get { return tablename; } }
        private string idformating;
        private string path;
        public string Basepath { get { return path; } }

        private string id;
        public string ID { get { return id; } set { this.id = value; } }
        public string NiceID
        {
            get
            {
                return Config.CreateNiceID(this.idformating, id);
            }
        }

        private string dataname;

        private string name;
        public string Name { get { return name; } set { name = value; } }

        public virtual Dictionary<string, string> FieldSet()
        {
            Dictionary<string,string> r = new Dictionary<string,string>();
            r.Add("name",this.name);
            r.Add("active","1");
            return r;
        }

        public Module(Database db,string tablename,string dataname, string id="")
        {
            this.db = db;
            this.tablename = tablename;
            this.dataname = dataname;
            this.idformating = Config.IDFormating[tablename];
            this.path = Config.BasePath + Path.DirectorySeparatorChar + Config.Paths[tablename] + Path.DirectorySeparatorChar;
            this.id = id;
        }


        public virtual string GetJoinOn(Joinable jointable) { return "id"; }

        public string GetTableName()
        {
            return this.tablename;
        }

        public List<string> GetFields(bool joinable)
        {
            List<string> f = new List<string>();
            foreach (KeyValuePair<string, string> item in this.FieldSet())
            {
                if (joinable)
                {
                    f.Add(this.tablename + "." + this.tablename + "_" + item.Key);
                }
                else
                {
                    f.Add(item.Key);
                }
                
            }
            return f;
        }

        public virtual bool Save()
        {
            bool ok = true;
            if (String.IsNullOrEmpty(this.id))
            {
                ok = db.Insert(this.tablename, this.FieldSet());
            }
            else
            {
                ok = db.Update(this.tablename, this.FieldSet(), "`id`='" + this.id + "'");

            }
            return ok;
        }

        public bool Delete()
        {
            bool ok = true;
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            tmp.Add("active", "0");

            ok = db.Update(this.tablename, tmp, "`id`='" + this.id + "'");
            return ok;
        }

        public virtual Dictionary<string, string> GetDictionary()
        {
            Result d = this.db.getRow(this.tablename, new string[] { "id", "name" }, "`active`='1'");
            if (d.RowCount < 1) return null;

            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (Row item in d.Rows)
            {
                r.Add(item.Cells["id"], item.Cells["name"]);
            }

            if (r.Count > 0) return r;
            else return null;
        }
     

        /// <summary>
        /// Fills the Internal Fields with the Data from given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract bool Load(string id);
        public abstract void Unload();
        List<string> GetIDList()
        {
            Result d = this.db.getRow(this.tablename, new string[] { "id" }, "`active`='1'");
            if (d.RowCount < 1) return null;

            List<string> r = new List<string>();
            foreach (Row item in d.Rows)
            {
                r.Add(item.Cells["id"]);
            }

            if (r.Count > 0) return r;
            else return null;
        }
        public List<CRUDable> GetFullList()
        {
            List<CRUDable> r = new List<CRUDable>();
            Result d = this.db.getRow(this.tablename, new string[] { "id" }, "`active`='1'", "id ASC");
            foreach (Row row in d.Rows)
            {
                r.Add(createObject(db, row.Cells["id"]));
            }
            return r;
        }
        
        public abstract CRUDable createObject(Database db, string id);

        public abstract Content ToContent(ExportCount c);

        public string DataName(ExportCount c)
        {
            if (c == ExportCount.MULTI)
            {
                return this.dataname;
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
                return this.tablename;
            }
            else
            {
                return this.NiceID;
            }
        }


        public abstract string Adress
        {
            get;
        }

        public string ObjectID
        {
            get { return this.NiceID; }
        }

        public abstract string WorkerName
        {
            get;
        }

        public abstract string DateCreated
        {
            get;
        }

        public abstract string DateSecond
        {
            get;
        }


        
    }
}
