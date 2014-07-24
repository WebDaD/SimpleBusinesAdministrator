﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Export;

namespace ManageAdministerExalt.Classes
{
    /// <summary>
    /// A Job, the Main Object in this software
    /// </summary>
    public class Job : Exportable
    {
        private Dictionary<Service,int> services; // int is number of units, object may be alteres and must be saved into job_has_services
        private List<Reminder> reminders;
        private List<Discount> discounts;
        private Bill bill;

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
