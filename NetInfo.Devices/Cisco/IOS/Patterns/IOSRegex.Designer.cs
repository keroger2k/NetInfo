﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetInfo.Devices.Cisco.IOS.Patterns {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class IOSRegex {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal IOSRegex() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NetInfo.Devices.Cisco.IOS.Patterns.IOSRegex", typeof(IOSRegex).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^!(\s+)?$.
        /// </summary>
        public static string BANG {
            get {
                return ResourceManager.GetString("BANG", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^Key name: [^.]*.(.*)$.
        /// </summary>
        public static string CRYPTO_KEY_NAME {
            get {
                return ResourceManager.GetString("CRYPTO_KEY_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^ip (domain-name|domain name) (.*)$.
        /// </summary>
        public static string DOMAIN_NAME {
            get {
                return ResourceManager.GetString("DOMAIN_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^clock timezone (\w+) (\d)\s?(\d)?$.
        /// </summary>
        public static string IOS_CLOCK {
            get {
                return ResourceManager.GetString("IOS_CLOCK", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^enable secret (\d) (.+)$.
        /// </summary>
        public static string IOS_ENABLE_SECRET {
            get {
                return ResourceManager.GetString("IOS_ENABLE_SECRET", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^hostname (.+)$.
        /// </summary>
        public static string IOS_HOSTNAME {
            get {
                return ResourceManager.GetString("IOS_HOSTNAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^line \W*.
        /// </summary>
        public static string IOS_LINE_ALL {
            get {
                return ResourceManager.GetString("IOS_LINE_ALL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^line aux*.
        /// </summary>
        public static string IOS_LINE_AUX {
            get {
                return ResourceManager.GetString("IOS_LINE_AUX", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^line con*.
        /// </summary>
        public static string IOS_LINE_CON {
            get {
                return ResourceManager.GetString("IOS_LINE_CON", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^line vty*.
        /// </summary>
        public static string IOS_LINE_VTY {
            get {
                return ResourceManager.GetString("IOS_LINE_VTY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^radius-server host (\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}) auth-port (\d+) acct-port (\d+) key (\d) (\w+)$.
        /// </summary>
        public static string IOS_RADIUS_SERVER {
            get {
                return ResourceManager.GetString("IOS_RADIUS_SERVER", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^\s?password (7) (\w+)$.
        /// </summary>
        public static string IOS_TYPE7_PASSWORD {
            get {
                return ResourceManager.GetString("IOS_TYPE7_PASSWORD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^\s*snmp-server location (.*)$.
        /// </summary>
        public static string SNMP_LOCATION {
            get {
                return ResourceManager.GetString("SNMP_LOCATION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^\s*(no snmp-server|snmp-server) .*$.
        /// </summary>
        public static string SNMP_SERVER_GENERIC {
            get {
                return ResourceManager.GetString("SNMP_SERVER_GENERIC", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^\s*snmp-server host (\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}) informs version (\d+) (\w+) (\w+)$.
        /// </summary>
        public static string SNMP_SERVER_HOSTS {
            get {
                return ResourceManager.GetString("SNMP_SERVER_HOSTS", resourceCulture);
            }
        }
    }
}
