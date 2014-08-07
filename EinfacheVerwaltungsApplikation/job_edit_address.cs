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
    public partial class job_edit_address : Form
    {
        private Job job;
        public job_edit_address(Job job)
        {
            InitializeComponent();
            this.job = job;
            //TODO: Load Services, fill left list and listview
            this.Text = Config.Name + " :: " + "Adresse :: " + job.NiceID; 
        }
    }
}
