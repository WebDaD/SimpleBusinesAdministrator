using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;

namespace ManageAdministerExalt.Classes
{
    public static class Config
    {
        public static DatabaseType DatabaseType 
        {
            get
            {
                return Properties.Settings.Default.db_type;
            }
            set
            {
                Properties.Settings.Default.db_type = value;
                Properties.Settings.Default.Save();
            }
        }

        public static string DatabaseConnectionString
        {
            get
            {
                return Properties.Settings.Default.db_connection_string;
            }
            set
            {
                Properties.Settings.Default.db_connection_string = value;
                Properties.Settings.Default.Save();
            }
        }

        public static string BasePath
        {
            get
            {
                return Properties.Settings.Default.basepath;
            }
            set
            {
                Properties.Settings.Default.basepath = value;
                Properties.Settings.Default.Save();
            }
        }

        public static string Name
        {
            get
            {
                return Properties.Settings.Default.app_name + " " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static string Username
        {
            get
            {
                return Properties.Settings.Default.username;
            }
            set
            {
                Properties.Settings.Default.username = value;
                Properties.Settings.Default.Save();
            }
        }

        public static string LicenseKey
        {
            get
            {
                return Properties.Settings.Default.license_key;
            }
            set
            {
                Properties.Settings.Default.license_key = value;
                Properties.Settings.Default.Save();
            }
        }

        public static string DefaultTab
        {
            get
            {
                return Properties.Settings.Default.default_tab;
            }
            set
            {
                Properties.Settings.Default.default_tab = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
