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
    public partial class job_create_reminder : Form
    {
        public job_create_reminder()
        {
            InitializeComponent();
            this.Text = Config.Name + " :: " + "Erstelle Mahnung"; //TODO: also show Job
            this.Icon = Properties.Resources.simba;
        }
    }
}
