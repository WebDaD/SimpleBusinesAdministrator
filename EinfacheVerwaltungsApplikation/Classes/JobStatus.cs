using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManageAdministerExalt.Classes
{
    public enum JobStatus
    {
        S0_SAVED,
        S1_CREATED,
        S2_SENT,
        S3_BILL_CREATED,
        S4_BILL_SENT,
        S5_REMINDER_SENT,
        S6_PAYMENT_RECEIVED
    }
}
