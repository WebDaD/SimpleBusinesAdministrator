﻿using System;
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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            this.Text = Config.Name + " :: " + "Optionen";
            this.Icon = Properties.Resources.simba;
        }
    }
}
