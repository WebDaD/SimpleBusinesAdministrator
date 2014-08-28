using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ManageAdministerExalt.Classes;

namespace ManageAdministerExalt
{
    public partial class job_create_bill : Form
    {
        public job_create_bill()
        {
            InitializeComponent();
            this.Text = Config.Name + " :: " + "Erstelle Rechnung"; //TODO: Also show Job
            this.Icon = Properties.Resources.simba;
        }
    }
}
