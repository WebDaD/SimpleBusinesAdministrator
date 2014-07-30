using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Export;
using WebDaD.Toolkit.Database;

namespace ManageAdministerExalt.Classes
{
    public class Worker : Exportable, Joinable, CRUDable
    {
        public Content ToContent(ExportCount c)
        {
            throw new NotImplementedException();
        }

        public string DataName(ExportCount c)
        {
            throw new NotImplementedException();
        }

        public string Filename(ExportCount c)
        {
            throw new NotImplementedException();
        }

        public string GetJoinOn(Joinable jointable)
        {
            throw new NotImplementedException();
        }

        public string GetTableName()
        {
            throw new NotImplementedException();
        }

        public List<string> GetFields()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public CRUDable New()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetIDList()
        {
            throw new NotImplementedException();
        }

        public CRUDable GetSingleInstance(string id)
        {
            throw new NotImplementedException();
        }

        public List<CRUDable> GetFullList()
        {
            throw new NotImplementedException();
        }
    }
}
