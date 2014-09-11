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
                Config.DatabaseConnectionString = Database_SQLite.GetConnectionString(@"Z:\Shared\Code\Simba\simba_demo.sqlite");
                Config.BackupFolder = @"Z:\Shared\Code\Simba\EinfacheVerwaltungsApplikation\bin\Debug\backup";
                Config.BasePath = @"Z:\Shared\Code\Simba\EinfacheVerwaltungsApplikation\bin\Debug";
                Config.LicenseKey = "1234";
                Config.FirstStart = false;
            }

            if (String.IsNullOrEmpty(Config.LicenseKey))
            {
                Application.Run(new EnterLicense());
            }
            else
            {

                if (Config.FirstStart)
                {
                    Application.Run(new SelectModule());
                }
                else
                {
                    Application.Run(new Main());
                   
                }
            }
        }
    }
}
