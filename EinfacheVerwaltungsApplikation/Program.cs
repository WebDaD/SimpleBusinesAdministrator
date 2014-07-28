using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using ManageAdministerExalt.Classes;
using WebDaD.Toolkit.Licensing;

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

            if (!String.IsNullOrEmpty(Config.LicenseKey))
            {
                Application.Run(new EnterLicense());
            }
            else
            {

                if (!String.IsNullOrEmpty(Config.DatabaseConnectionString))
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
