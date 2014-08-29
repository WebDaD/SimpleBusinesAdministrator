using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using ManageAdministerExalt.Classes;
using WebDaD.Toolkit.Licensing;
using System.Diagnostics;
using ManageAdministerExalt.Properties;
using WebDaD.Toolkit.Database;

namespace ManageAdministerExalt
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Debugger.IsAttached)
            {
                Config.DatabaseConnectionString = Database_SQLite.GetConnectionString(@"Z:\Shared\Code\Simba\eva_db.sqlite");
                Config.BackupFolder = @"Z:\Shared\Code\Simba\EinfacheVerwaltungsApplikation\bin\Debug\backup";
                Config.BasePath = @"Z:\Shared\Code\Simba\EinfacheVerwaltungsApplikation\bin\Debug";
            }
            else
            {
                //TODO: this is the real run!
                //if first start:
                    //backupfolder = appdata/webdad/backup
                    //basepath= appdata/webdad/simba
                    //db = basepath/eva_db.sqlite
                    //username / Email from setup?
                    //search for wkhtmltopdf exe

            }
            if (!String.IsNullOrEmpty(Config.LicenseKey))
            {
                Application.Run(new EnterLicense());
            }
            else
            {

                if (!String.IsNullOrEmpty(Config.DatabaseConnectionString)) //TODO: default use of sqlite
                {
                    Application.Run(new Main());
                }
                else
                {
                    Application.Run(new SelectModule());
                }
            }
        }
    }
}
