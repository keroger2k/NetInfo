using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS
{

    public class AAASettings : BaseSetting, IConfigSetting
    {

        public Regex GenericRegex
        {
            get { return new Regex(@"^aaa.*|^\s+server name (?<server>.*)", RegexOptions.IgnoreCase); }
        }

        public AuthenticationSettings Authentication
        {
            get
            {
                var set = new AuthenticationSettings();
                set.Settings = Settings.Where(c => set.GenericRegex.Match(c).Success);
                return set;
            }
        }

        public AuthorizationSettings Authorization
        {
            get
            {
                var set = new AuthorizationSettings();
                set.Settings = Settings.Where(c => set.GenericRegex.Match(c).Success);
                return set;
            }
        }

        public AccountingSettings Accounting
        {
            get
            {
                var set = new AccountingSettings();
                set.Settings = Settings.Where(c => set.GenericRegex.Match(c).Success);
                return set;
            }
        }

        public IEnumerable<GroupServerSettings> Groups
        {
            get
            {
                var set = new List<GroupServerSettings>();

                var groupRgx = new Regex("^aaa group .*$");
                var srvRgx = new Regex(@"^\s+server name (?<server>.*)");

                for (int i = 0; i < base.Settings.Count(); i++)
                {
                    if (groupRgx.Match(base.Settings.ElementAt(i)).Success)
                    {
                        var results = new List<string>();
                        results.Add(base.Settings.ElementAt(i));

                        while (base.Settings.Count() > i + 1 &&  srvRgx.Match(base.Settings.ElementAt(++i)).Success)
                        {
                            results.Add(srvRgx.Match(base.Settings.ElementAt(i)).Groups["server"].ToString());
                        }

                        set.Add(new GroupServerSettings { Settings = results });
                        i--;
                    }
                }
                return set;
            }
        }

        public class AuthenticationSettings : BaseSetting, IConfigSetting
        {

            public Regex GenericRegex
            {
                get { return new Regex(@"^aaa authentication.*$", RegexOptions.IgnoreCase); }
            }

            public bool LoginGroupTacacsEnable
            {
                get
                {
                    var r = GetSetting(new Regex(@"^aaa authentication login default group tacacs\+ enable$", RegexOptions.IgnoreCase));
                    return (r != null);
                }
            }

            public bool EnableGroupTacacsEnable
            {
                get
                {
                    var r = GetSetting(new Regex(@"^aaa authentication enable default group tacacs\+ enable$", RegexOptions.IgnoreCase));
                    return (r != null);
                }
            }

            public Dot1xSettings Dot1x
            {
                get
                {
                    var set = new Dot1xSettings();
                    set.Settings = Settings.Where(c => set.GenericRegex.Match(c).Success);
                    return set;
                }
            }

            public class Dot1xSettings : BaseSetting, IConfigSetting
            {

                public string DefaultGroup
                {
                    get
                    {
                        var r = GetSetting(new Regex(@"^aaa authentication dot1x default group (\w+)$", RegexOptions.IgnoreCase));
                        return (r == null) ? string.Empty : r.Groups[1].Value;
                    }
                }

                public Regex GenericRegex
                {
                    get { return new Regex(@"^aaa authentication dot1x.*$", RegexOptions.IgnoreCase); }
                }
            }
        }

        public class AuthorizationSettings : BaseSetting, IConfigSetting
        {

            public Regex GenericRegex
            {
                get { return new Regex(@"^aaa authorization.*$", RegexOptions.IgnoreCase); }
            }
        }

        public class AccountingSettings : BaseSetting, IConfigSetting
        {

            public Regex GenericRegex
            {
                get { return new Regex(@"^aaa accounting.*$", RegexOptions.IgnoreCase); }
            }
        }

        public class GroupServerSettings : BaseSetting, IConfigSetting
        {
            public enum AAAGroupServerTypes
            {
                tacacsPlus,
                radius,
                UKNOWN
            }

            private Regex _grpRegex = new Regex(@"^aaa group server (?<groupServerType>.*) (?<groupServerName>.*)$");
            private Regex _srvRgx = new Regex(@"^server name (?<server>.*)");

            public AAAGroupServerTypes GroupServerType
            {
                get
                {
                    switch (_grpRegex.Match(base.Settings.ElementAt(0)).Groups["groupServerType"].ToString())
                    {
                        case "tacacs+":
                            return AAAGroupServerTypes.tacacsPlus;
                        case "radius":
                            return AAAGroupServerTypes.radius;
                        default:
                            return AAAGroupServerTypes.UKNOWN;
                    }
                }
            }

            public string GroupServerName
            {
                get
                {
                    return _grpRegex.Match(base.Settings.ElementAt(0)).Groups["groupServerName"].ToString();
                }
            }

            public IEnumerable<string> ServerAliases
            {
                get
                {
                    return base.Settings.Skip(1);
                }
            }

            public Regex GenericRegex
            {
                get { return new Regex(@"^aaa .*$", RegexOptions.IgnoreCase); }
            }
        }
    }
}