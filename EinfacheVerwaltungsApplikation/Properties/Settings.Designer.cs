﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.18408
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ManageAdministerExalt.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SQLite")]
        public global::WebDaD.Toolkit.Database.DatabaseType db_type {
            get {
                return ((global::WebDaD.Toolkit.Database.DatabaseType)(this["db_type"]));
            }
            set {
                this["db_type"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string db_connection_string {
            get {
                return ((string)(this["db_connection_string"]));
            }
            set {
                this["db_connection_string"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string basepath {
            get {
                return ((string)(this["basepath"]));
            }
            set {
                this["basepath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SimBA")]
        public string app_name {
            get {
                return ((string)(this["app_name"]));
            }
            set {
                this["app_name"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string username {
            get {
                return ((string)(this["username"]));
            }
            set {
                this["username"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string license_key {
            get {
                return ((string)(this["license_key"]));
            }
            set {
                this["license_key"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("tab_jobs")]
        public string default_tab {
            get {
                return ((string)(this["default_tab"]));
            }
            set {
                this["default_tab"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("workers|mitarbeiter;customers|kunde;discounts|rabatt;expenses|ausgabe;items|Gegen" +
            "stand;jobs|auftrag;paymentconditions|zahlungsbedingung;reminders|Mahnung;service" +
            "s|leistung;terms|agb;bills|rechnungen;")]
        public string paths {
            get {
                return ((string)(this["paths"]));
            }
            set {
                this["paths"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("bills|B%ID5%;customers|C%ID5%;discounts|D%ID5%;expenses|E%ID5%;items|I%ID5%;jobs|" +
            "J%ID5%;paymentconditions|P%ID5%;reminders|R%ID5%;services|S%ID5%;terms|P %ID2%;w" +
            "orkers|W%ID5%;")]
        public string idformating {
            get {
                return ((string)(this["idformating"]));
            }
            set {
                this["idformating"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>tab_customers</string>
  <string>tab_services</string>
  <string>tab_terms</string>
  <string>tab_reports</string>
  <string>tab_worker</string>
  <string>tab_jobs</string>
  <string>tab_items</string>
  <string>tab_expenses</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection active_tabs {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["active_tabs"]));
            }
            set {
                this["active_tabs"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool timer {
            get {
                return ((bool)(this["timer"]));
            }
            set {
                this["timer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int timer_interval {
            get {
                return ((int)(this["timer_interval"]));
            }
            set {
                this["timer_interval"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool askforexit {
            get {
                return ((bool)(this["askforexit"]));
            }
            set {
                this["askforexit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\\\Program Files\\\\wkhtmltopdf\\\\bin\\\\wkhtmltopdf.exe")]
        public string wkhtmltopdf {
            get {
                return ((string)(this["wkhtmltopdf"]));
            }
            set {
                this["wkhtmltopdf"] = value;
            }
        }
    }
}
