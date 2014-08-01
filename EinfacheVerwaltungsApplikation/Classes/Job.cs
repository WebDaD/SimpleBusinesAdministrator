using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Export;
using WebDaD.Toolkit.Database;

namespace ManageAdministerExalt.Classes
{
    /// <summary>
    /// A Job, the Main Object in this software
    /// </summary>
    public class Job
    {
        private Dictionary<Service,int> services; // int is number of units, object may be alteres and must be saved into job_has_services
        private List<Reminder> reminders;
        private List<Discount> discounts;
        private Bill bill;
    }
}
