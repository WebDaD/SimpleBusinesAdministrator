using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDaD.Toolkit.Database;
using System.Text.RegularExpressions;

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
                return Properties.Settings.Default.app_name;
            }
        }

        public static string Version
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
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
                    if (!String.IsNullOrEmpty(item))
                    {
                        r.Add(item.Split('|')[0], item.Split('|')[1]);
                    }
                }
                return r;

            }
            set {
                string x = "";
                foreach (KeyValuePair<string,string> item in value)
                {
                        x += item.Key + "|" + item.Value + ";";
                }
                Properties.Settings.Default.paths = x;
                Properties.Settings.Default.Save();
            }
        }

        public static Dictionary<string, string> IDFormating
        {
            get
            {
                string[] t = Properties.Settings.Default.idformating.Split(';');
                Dictionary<string, string> r = new Dictionary<string, string>();
                foreach (string item in t)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        r.Add(item.Split('|')[0], item.Split('|')[1]);
                    }
                }
                return r;

            }
            set
            {
                string x = "";
                foreach (KeyValuePair<string, string> item in value)
                {
                    x += item.Key + "|" + item.Value + ";";
                }
                Properties.Settings.Default.idformating = x;
                Properties.Settings.Default.Save();
            }
        }

        public static string CreateNiceID(string formating, string id, DateTime date=new DateTime())
        {
            string r = formating;
            r = r.Replace("%YY%", date.ToString("yy"));
            r = r.Replace("%YYYY%", date.ToString("yyyy"));
            r = r.Replace("%M%", date.ToString("MM"));
            r = r.Replace("%D%", date.ToString("dd"));

            string reg_ID = @"%ID(\d+)%";
            string count="";
            Regex reg = new Regex(reg_ID);
            Match m = reg.Match(r);
            if(m.Success)
            {
                Group g = m.Groups[1];
                count = g.ToString();
                int c = Int32.Parse(count);
                r = r.Replace("%ID" + count + "%", id.PadLeft(c, '0'));
            }
            

            return r;
        }
        public static bool FirstStart
        {
            get { return Properties.Settings.Default.firststart; }
            set
            {
                Properties.Settings.Default.firststart = value;
                Properties.Settings.Default.Save();
            }
        }
        public static bool Timer
        {
            get { return Properties.Settings.Default.timer; }
            set
            {
                Properties.Settings.Default.timer = value;
                Properties.Settings.Default.Save();
            }
        }
        public static int TimerInterval
        {
            get { return Properties.Settings.Default.timer_interval; }
            set
            {
                Properties.Settings.Default.timer_interval = value;
                Properties.Settings.Default.Save();
            }
        }
        public static bool AskForExit
        {
            get { return Properties.Settings.Default.askforexit; }
            set
            {
                Properties.Settings.Default.askforexit = value;
                Properties.Settings.Default.Save();
            }
        }
        public static string WKHTMLTOPDFPath
        {
            get
            {
                return Properties.Settings.Default.wkhtmltopdf;
            }
            set
            {
                Properties.Settings.Default.wkhtmltopdf = value;
                Properties.Settings.Default.Save();
            }
        }
        public static string BackupFolder
        {
            get
            {
                return Properties.Settings.Default.backupFolder;
            }
            set
            {
                Properties.Settings.Default.backupFolder = value;
                Properties.Settings.Default.Save();
            }
        }
        public static bool AutoBackup
        {
            get
            {
                return Properties.Settings.Default.autobackup;
            }
            set
            {
                Properties.Settings.Default.autobackup = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
