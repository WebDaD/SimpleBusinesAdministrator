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

        public static List<string> ActiveTabs
        {
            get
            {
                List<string> r = new List<string>();
                foreach (string item in Properties.Settings.Default.active_tabs)
                {
                    r.Add(item);
                }
                return r;
            }
            set
            {
                Properties.Settings.Default.active_tabs.Clear();
                foreach (string item in value)
                {
                    Properties.Settings.Default.active_tabs.Add(item);
                }
                Properties.Settings.Default.Save();
            }
        }

        public static Dictionary<string, string> Paths
        {
            get
            {
                string[] t = Properties.Settings.Default.paths.Split(';');
                Dictionary<string, string> r = new Dictionary<string, string>();
                foreach (string item in t)
                {
                    r.Add(item.Split('-')[0], item.Split('-')[1]);
                }
                return r;

            }
            set {
                string x = "";
                foreach (KeyValuePair<string,string> item in value)
                {
                    x += item.Key + "-" + item.Value + ";";
                }
                Properties.Settings.Default.paths = x;
                Properties.Settings.Default.Save();
            }
        }

        public static string CreateNiceID(string formating, string id)
        {
            string r = "";
            //TODO: parse string
            return r;
        }
    }
}
