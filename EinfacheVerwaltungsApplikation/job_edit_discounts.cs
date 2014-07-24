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
    public partial class job_edit_discounts : Form
    {
        public job_edit_discounts()
        {
            InitializeComponent();
            this.Text = Config.Name + " :: " + "Rabatte"; //TODO: Also show Job
        }
    }
}
