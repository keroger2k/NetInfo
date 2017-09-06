using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS
{

    public class IOSInterface
    {
        public const string INTERFACE_TYPES = @"(Embedded-Service-Engine|BVI|BRI|POS|Hssi|FastEthernet|ATM|TenGigabitEthernet|Multilink|Serial|Port-channel|GigabitEthernet|Tunnel|Vlan|Loopback)(\d+.*)";
        public static readonly Regex INTERFACE_REGEX = new Regex(string.Format(@"^interface {0}$", INTERFACE_TYPES), RegexOptions.IgnoreCase);
        private static readonly Regex INTERFACE_DESC_REGEX = new Regex(@"^\s*description\s+(.+)$", RegexOptions.IgnoreCase);
        private static readonly Regex INTERFACE_SHUTDOWN_REGEX = new Regex(@"^\s*shutdown$", RegexOptions.IgnoreCase);
        private static readonly Regex INTERFACE_OSPF_MD5 = new Regex(@"^\s*ip ospf message-digest-key (\d+) md5 7 (.+)$", RegexOptions.IgnoreCase);
        private static readonly Regex VLAN_NUMBER = new Regex(@"^\s*switchport access vlan (\d+)$", RegexOptions.IgnoreCase);
        private static readonly Regex IOS_INTERFACE_ADDRESS = new Regex(@"^\s+ip address\s(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\s(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})$", RegexOptions.IgnoreCase);
        private static readonly Dictionary<string, bool> isInterfacePhysical;
        private readonly IEnumerable<string> commands;

        public IOSInterface(IEnumerable<string> commands)
        {
            if (commands == null || commands.Count() == 0)
            {
                throw new ArgumentNullException();
            }

            if (!INTERFACE_REGEX.Match(commands.ElementAt(0)).Success)
            {
                throw new NotImplementedException("Unknown Interface");
            }

            this.commands = commands;
        }

        static IOSInterface()
        {
            isInterfacePhysical = new Dictionary<string, bool>();
            isInterfacePhysical["FastEthernet"] = true;
            isInterfacePhysical["Embedded-Service-Engine"] = false;
            isInterfacePhysical["BVI"] = true;
            isInterfacePhysical["Hssi"] = true;
            isInterfacePhysical["BRI"] = true;
            isInterfacePhysical["ATM"] = true;
            isInterfacePhysical["POS"] = true;
            isInterfacePhysical["GigabitEthernet"] = true;
            isInterfacePhysical["Loopback"] = false;
            isInterfacePhysical["Port-channel"] = false;
            isInterfacePhysical["Multilink"] = false;
            isInterfacePhysical["Serial"] = true;
            isInterfacePhysical["TenGigabitEthernet"] = true;
            isInterfacePhysical["Tunnel"] = false;
            isInterfacePhysical["Vlan"] = false;
            isInterfacePhysical["VLAN"] = false;  //Legacy format
        }

        /// <summary>
        /// The address of the interface if it is configured
        /// </summary>
        public InterfaceAddress Address
        {
            get
            {
                var match = commands.FirstOrDefault(c => IOS_INTERFACE_ADDRESS.Match(c).Success);
                return (match == null) ? null : new InterfaceAddress
                {
                    NetworkAddress = IPAddress.Parse(IOS_INTERFACE_ADDRESS.Match(match).Groups[1].Value),
                    NetworkMask = IPAddress.Parse(IOS_INTERFACE_ADDRESS.Match(match).Groups[2].Value)
                };
            }
        }

        public IEnumerable<AccessGroup> AccessGroups
        {
            get
            {
                var groups = commands.Where(c => new Regex(@"ip\s+access-group\s+(.*)\s+(in|out)", RegexOptions.IgnoreCase).Match(c).Success);
                return groups.Select(c =>
                {
                    var g = new Regex(@"ip\s+access-group\s+(.*)\s+(in|out)", RegexOptions.IgnoreCase).Match(c);
                    return new AccessGroup
                    {
                        Direction = g.Groups[2].Value,
                        Name = g.Groups[1].Value
                    };
                });
            }
        }

        public string Type
        {
            get
            {
                return INTERFACE_REGEX.Match(commands.ElementAt(0)).Groups[1].Value;
            }
        }

        public string Name
        {
            get
            {
                var m = INTERFACE_REGEX.Match(commands.ElementAt(0));
                return string.Format("{0}", m.Groups[0].Value);
            }
        }

        public string ShortName
        {
            get
            {
                return this.Name.Split(' ').ElementAt(1);
            }
        }

        public string Description
        {
            get
            {
                var match = commands.FirstOrDefault(c => INTERFACE_DESC_REGEX.Match(c).Success);
                return (match == null) ? string.Empty : INTERFACE_DESC_REGEX.Match(match).Groups[1].Value;
            }
        }

        public bool Shutdown
        {
            get
            {
                return commands.FirstOrDefault(c => INTERFACE_SHUTDOWN_REGEX.Match(c).Success) != null;
            }
        }

        public bool IsCDPEnabled
        {
            get
            {
                return commands.FirstOrDefault(c => new Regex(@"no cdp enable", RegexOptions.IgnoreCase).Match(c).Success) == null;
            }
        }

        public int BridgeGroup
        {
            get
            {
                var bridgeGroupRgx = new Regex(@"bridge-group (?<groupNumber>\d+)", RegexOptions.IgnoreCase);
                var groupCommand = commands.FirstOrDefault(c => bridgeGroupRgx.Match(c).Success);
                return groupCommand == null ? -1 : int.Parse(bridgeGroupRgx.Match(groupCommand).Groups["groupNumber"].Value);
            }
        }

        public bool Physical
        {
            get
            {
                return isInterfacePhysical[this.Type];
            }
        }

        public int Vlan
        {
            get
            {
                int num = 1;
                var line = commands.SingleOrDefault(c => VLAN_NUMBER.Match(c).Success);
                if (line != null)
                {
                    num = int.Parse(VLAN_NUMBER.Match(line).Groups[1].Value);
                }
                else
                {
                    Match mtc = new Regex(@"^interface Vlan(\d+\/?\d*)$", RegexOptions.IgnoreCase).Match(commands.ElementAt(0));
                    if (mtc.Success)
                    {
                        num = int.Parse(mtc.Groups[1].Value);
                    }
                }
                return num;
            }
        }

        public SwitchPortSettings SwitchPort
        {
            get
            {
                var swport = new SwitchPortSettings();
                var settings = commands.Where(c => swport.GenericRegex.Match(c).Success);
                swport.Settings = settings;
                return swport;
            }
        }

        public IPSettings IP
        {
            get
            {
                var ipsettings = new IPSettings();
                var settings = commands.Where(c => ipsettings.GenericRegex.Match(c).Success);
                ipsettings.Settings = settings;
                return ipsettings;
            }
        }

        public AuthenticationSettings Authentication
        {
            get
            {
                var authSettings = new AuthenticationSettings();
                var settings = commands.Where(c => authSettings.GenericRegex.Match(c).Success);
                authSettings.Settings = settings;
                return authSettings;
            }
        }

        public Dot1xSettings Dot1x
        {
            get
            {
                var dot1xSettings = new Dot1xSettings();
                var settings = commands.Where(c => dot1xSettings.GenericRegex.Match(c).Success);
                dot1xSettings.Settings = settings;
                return dot1xSettings;
            }
        }

        public class IPSettings : BaseSetting, IConfigSetting
        {

            public Regex GenericRegex
            {
                get { return new Regex(@"^\s+(no ip|ip).*$", RegexOptions.IgnoreCase); }
            }

            public bool Unreachables
            {
                get
                {
                    var r = GetSetting(new Regex(@"^\s+no ip unreachables", RegexOptions.IgnoreCase));
                    return (r == null);
                }
            }

            public bool Redirects
            {
                get
                {
                    var r = GetSetting(new Regex(@"^\s+no ip redirects", RegexOptions.IgnoreCase));
                    return (r == null);
                }
            }

            public bool ProxyArp
            {
                get
                {
                    var r = GetSetting(new Regex(@"^\s+no ip proxy-arp", RegexOptions.IgnoreCase));
                    return (r == null);
                }
            }

            public bool DirectedBroadcast
            {
                get
                {
                    var r = GetSetting(new Regex(@"^\s+ip directed-broadcast", RegexOptions.IgnoreCase));
                    return (r != null);
                }
            }

            public bool MaskReply
            {
                get
                {
                    var r = GetSetting(new Regex(@"^\s+ip mask-reply", RegexOptions.IgnoreCase));
                    return (r != null);
                }
            }

            public OSPFSettings OSPF
            {
                get
                {
                    var ospfSettings = new OSPFSettings();
                    var settings = Settings.Where(c => ospfSettings.GenericRegex.Match(c).Success);
                    ospfSettings.Settings = settings;
                    return ospfSettings;
                }
            }

            public EIGRPSettings EIGRP
            {
                get
                {
                    var eigpSettings = new EIGRPSettings();
                    var settings = Settings.Where(c => eigpSettings.GenericRegex.Match(c).Success);
                    eigpSettings.Settings = settings;
                    return eigpSettings;
                }
            }

            public class OSPFSettings : BaseSetting, IConfigSetting
            {

                public Regex GenericRegex
                {
                    get { return new Regex(@"^\s+(no ip|ip) ospf .*$", RegexOptions.IgnoreCase); }
                }

                public bool Enabled
                {
                    get
                    {
                        return Settings.Any();
                    }
                }

                public int Cost
                {
                    get
                    {
                        var r = GetSetting(new Regex(@"^\s+ip ospf cost (?<cost>\d+)", RegexOptions.IgnoreCase));
                        return (r == null) ? default(int) : int.Parse(r.Groups["cost"].Value);
                    }
                }
                public string Area
                {
                    get
                    {
                        var r = GetSetting(new Regex(@"^\s+ip ospf \d+ area (?<area>\d+)", RegexOptions.IgnoreCase));
                        return (r == null) ? string.Empty : r.Groups["area"].Value;
                    }
                }

                public string MessageDigest
                {
                    get
                    {
                        var r = GetSetting(new Regex(@"^\s*ip ospf message-digest-key (\d+) md5 7 (?<key>.+)$", RegexOptions.IgnoreCase));
                        return (r == null) ? string.Empty : r.Groups["key"].Value;
                    }
                }
            }

            public class EIGRPSettings : BaseSetting, IConfigSetting
            {

                public Regex GenericRegex
                {
                    get { return new Regex(@"^\s+(no ip|ip) authentication .*$", RegexOptions.IgnoreCase); }
                }

                public bool Enabled
                {
                    get
                    {
                        return Settings.Any();
                    }
                }

                public string Mode
                {
                    get
                    {
                        var r = GetSetting(new Regex(@"^\s+(no ip|ip) authentication mode eigrp (\d+) (?<mode>.*)$", RegexOptions.IgnoreCase));
                        return (r == null) ? string.Empty : r.Groups["mode"].Value;
                    }
                }

                public string KeyChain
                {
                    get
                    {
                        var r = GetSetting(new Regex(@"^\s+(no ip|ip) authentication key-chain eigrp (\d+) (?<keyChain>.*)$", RegexOptions.IgnoreCase));
                        return (r == null) ? string.Empty : r.Groups["keyChain"].Value;
                    }
                }
            }
        }

        public class AuthenticationSettings : BaseSetting, IConfigSetting
        {

            public Regex GenericRegex
            {
                get { return new Regex(@"^\s+(no authentication|authentication).*$", RegexOptions.IgnoreCase); }
            }

            public bool Periodic
            {
                get
                {
                    var r = GetSetting(new Regex(@"^\s+authentication periodic", RegexOptions.IgnoreCase));
                    return (r != null);
                }
            }

            public bool PortControlAuto
            {
                get
                {
                    var r = GetSetting(new Regex(@"^\s+authentication port-control auto", RegexOptions.IgnoreCase));
                    return (r != null);
                }
            }

            public int TimerReauthenticate
            {
                get
                {
                    var r = GetSetting(new Regex(@"^\s+authentication timer restart (?<seconds>\d+)", RegexOptions.IgnoreCase));
                    return (r == null) ? -1 : int.Parse(r.Groups["seconds"].Value);
                }
            }
        }

        public class Dot1xSettings : BaseSetting, IConfigSetting
        {

            public Regex GenericRegex
            {
                get { return new Regex(@"^\s+(no dot1x|dot1x).*$", RegexOptions.IgnoreCase); }
            }

            public bool PAEAuthenticator
            {
                get
                {
                    var r = GetSetting(new Regex(@"^\s+dot1x pae authenticator", RegexOptions.IgnoreCase));
                    return (r != null);
                }
            }

            public int TimeoutQuietPeriod
            {
                get
                {
                    var r = GetSetting(new Regex(@"^\s+dot1x timeout quiet-period (?<seconds>\d+)", RegexOptions.IgnoreCase));
                    return (r == null) ? -1 : int.Parse(r.Groups["seconds"].Value);
                }
            }

            public int ReauthTimeout
            {
                get
                {
                    var r = GetSetting(new Regex(@"^\s+dot1x timeout re-authperiod (?<seconds>\d+)", RegexOptions.IgnoreCase));
                    return (r == null) ? 3600 : int.Parse(r.Groups["seconds"].Value);
                }
            }
        }

        public class SwitchPortSettings : BaseSetting, IConfigSetting
        {

            public enum PortType
            {
                Trunk,
                Access,
                Unknown
            }

            public Regex GenericRegex
            {
                get { return new Regex(@"^\s+switchport.*$", RegexOptions.IgnoreCase); }
            }

            public PortType Type
            {
                get
                {
                    var a = GetSetting(new Regex(@"^\s+switchport mode (?<mode>.*)", RegexOptions.IgnoreCase));
                    if (a != null)
                    {
                        var local = a.Groups["mode"].Value;
                        switch (local)
                        {
                            case "trunk":
                            case "dynamic desirable":
                                return PortType.Trunk;

                            case "access":
                                return PortType.Access;

                            default:
                                return PortType.Unknown;
                        }
                    }
                    var b = GetSetting(new Regex(@"^\s+switchport access", RegexOptions.IgnoreCase));
                    return (b != null) ? PortType.Access :
                      (Encapsulation != null || AllowedVlans != null) ? PortType.Trunk : PortType.Unknown;
                }
            }

            public string Encapsulation
            {
                get
                {
                    var r = GetSetting(new Regex(@"^\s+switchport trunk encapsulation (\w+)", RegexOptions.IgnoreCase));
                    return (r == null) ? null : r.Groups[1].Value;
                }
            }

            public IEnumerable<int> AllowedVlans
            {
                get
                {
                    var r = GetSetting(new Regex(@"^\s+switchport trunk allowed vlan (.*)$", RegexOptions.IgnoreCase));
                    return (r == null) ? new List<int>() : ParseVlans(r.Groups[1].Value);
                }
            }

            private IEnumerable<int> ParseVlans(string vlans)
            {
                var results = new List<int>();
                var numbersRanges = vlans.Split(',');
                var ranges = numbersRanges.Where(c => c.Contains('-'));
                results.AddRange(numbersRanges.Where(c => !c.Contains('-')).Select(c => int.Parse(c)));
                foreach (var item in ranges)
                {
                    var range = item.Split('-').Select(c => int.Parse(c));
                    results.AddRange(Enumerable.Range(range.ElementAt(0), range.ElementAt(1)).ToArray());
                }
                return results;
            }
        }

        public class InterfaceAddress
        {

            public IPAddress NetworkAddress { get; set; }

            public IPAddress NetworkMask { get; set; }
        }

        public class AccessGroup
        {

            public string Name { get; set; }

            public string Direction { get; set; }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}