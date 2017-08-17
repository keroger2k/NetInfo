using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.Cisco.IOS.Classes;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.Cisco.IOS.Patterns;

namespace NetInfo.Devices.IOS {

  public class IOSDevice : Device, IIOSDevice {
    private readonly Regex ALL_LINE_REGEX = new Regex(IOSRegex.IOS_LINE_ALL, RegexOptions.IgnoreCase);
    private readonly Regex LINE_VTY_REGEX = new Regex(IOSRegex.IOS_LINE_VTY, RegexOptions.IgnoreCase);
    private readonly Regex LINE_AUX_REGEX = new Regex(IOSRegex.IOS_LINE_AUX, RegexOptions.IgnoreCase);
    private readonly Regex LINE_CON_REGEX = new Regex(IOSRegex.IOS_LINE_CON, RegexOptions.IgnoreCase);
    private readonly Regex BANG_REGEX = new Regex(IOSRegex.BANG, RegexOptions.IgnoreCase);
    private readonly Regex TYPE7PASS_REGEX = new Regex(IOSRegex.IOS_TYPE7_PASSWORD, RegexOptions.IgnoreCase);

    public enum TransportBoundaryType {
      PrimarySite,
      SmallSiteNonRedundant,
      SmallSiteRedundant,
      VSSD,
      UNKNOWN
    }

    public IOSDevice(IAssetBlob AssetBlob)
      : base(AssetBlob) {
    }

    public IEnumerable<IOSLineItem> Lines {
      get {
        var lines = new List<IOSLineItem>();
        var foundLines = false;
        for (int i = 0; i < configLength; i++) {
          if (!foundLines && ALL_LINE_REGEX.Match(config.ElementAt(i)).Success) {
            foundLines = true;
          }
          if (foundLines && BANG_REGEX.Match(config.ElementAt(i)).Success) {
            break;
          }

          if (foundLines) {
            if (LINE_CON_REGEX.Match(config.ElementAt(i)).Success) {
              lines.Add(ParseCommands(new IOSLineItem {
                Type = LineType.CONSOLE,
                Name = config.ElementAt(i++),
                Commands = new List<string>()
              }, i));
            } else if (LINE_VTY_REGEX.Match(config.ElementAt(i)).Success) {
              lines.Add(ParseCommands(new IOSLineItem {
                Type = LineType.VTY,
                Name = config.ElementAt(i++),
                Commands = new List<string>()
              }, i));
            } else if (LINE_AUX_REGEX.Match(config.ElementAt(i)).Success) {
              lines.Add(ParseCommands(new IOSLineItem {
                Type = LineType.AUX,
                Name = config.ElementAt(i++),
                Commands = new List<string>()
              }, i));
            }
          }
        }
        return lines;
      }
    }

    private IOSLineItem ParseCommands(IOSLineItem item, int i) {
      while (!ALL_LINE_REGEX.Match(config.ElementAt(i)).Success && !BANG_REGEX.Match(config.ElementAt(i)).Success) {
        var match = TYPE7PASS_REGEX.Match(config.ElementAt(i));
        if (match.Success) {
          item.Password = new IOSPassword { Value = match.Groups[2].Value, Type = int.Parse(match.Groups[1].Value) };
          i++;
        } else {
          item.Commands.Add(config.ElementAt(i++));
        }
      }
      return item;
    }

    public IOSClock Clock {
      get {
        var x = GetConfigurationSetting(new Regex(IOSRegex.IOS_CLOCK, RegexOptions.IgnoreCase));
        return (x == null) ?
          new IOSClock() : (string.IsNullOrEmpty(x.Groups[3].Value)) ?
          new IOSClock(x.Groups[1].Value, int.Parse(x.Groups[2].Value)) :
          new IOSClock(x.Groups[1].Value, int.Parse(x.Groups[2].Value), int.Parse(x.Groups[3].Value));
      }
    }

    public IOSImage Image {
      get {
        return new IOSImage(ShowVersion.ImageFileName);
      }
    }

    public IOSPassword EnableSecret {
      get {
        var x = GetConfigurationSetting(new Regex(IOSRegex.IOS_ENABLE_SECRET, RegexOptions.IgnoreCase));
        return (x == null) ? new IOSPassword { Value = "NOT SET" } : new IOSPassword { Value = x.Groups[2].Value, Type = int.Parse(x.Groups[1].Value) };
      }
    }

    private IEnumerable<IOSInterface> _Interfaces;

    public IEnumerable<IOSInterface> Interfaces {
      get {
        if (_Interfaces == null) {
          var interfaces = new List<IOSInterface>();
          var firstInterface = false;
          for (int i = 0; i < configLength; i++) {
            if (IOSInterface.INTERFACE_REGEX.Match(config.ElementAt(i)).Success) {
              firstInterface = true;
              var commands = new List<string>();
              commands.Add(config.ElementAt(i++));
              while (true) {
                if (BANG_REGEX.Match(config.ElementAt(i)).Success) {
                  break;
                }
                commands.Add(config.ElementAt(i++));
              }
              interfaces.Add(new IOSInterface(commands));
            } else if (firstInterface) {
              //should only hit this once finished with interface processing
              break;
            }
          }
          _Interfaces = interfaces;
        }
        return _Interfaces;
      }
    }

    public IEnumerable<IOSRadiusServer> RadiusServers {
      get {
        var matches = GetConfigurationMatches(new Regex(IOSRegex.IOS_RADIUS_SERVER, RegexOptions.IgnoreCase));
        if (matches.Any()) {
          return matches.Select(c => new IOSRadiusServer {
            Host = IPAddress.Parse(c.Groups[1].Value),
            AuthPort = int.Parse(c.Groups[2].Value),
            AcctPort = int.Parse(c.Groups[3].Value),
            Key = new IOSPassword {
              Type = int.Parse(c.Groups[4].Value),
              Value = c.Groups[5].Value
            }
          });
        }
        return new List<IOSRadiusServer>();
      }
    }

    private string _hostname;

    public string Hostname {
      get {
        if (_hostname == null) {
          var match = GetConfigurationSetting(new Regex(IOSRegex.IOS_HOSTNAME, RegexOptions.IgnoreCase));
          _hostname = (match == null) ? null : match.Groups[1].Value;
        }
        return _hostname;
      }
    }

    private string _Model;

    public string Model {
      get {
        if (string.IsNullOrEmpty(_Model)) {
          _Model = ShowInventory.Model;
        }
        if (string.IsNullOrEmpty(_Model)) {
          _Model = ShowVersion.Model;
        }
        return _Model;
      }
    }

    public bool IsManaged {
      get {
        var vtyLines = this.Lines.Where(c => c.Type == LineType.VTY);
        return !vtyLines.All(c => c.Commands.Contains(" transport input none"));
      }
    }

    private bool? _IsODMNEnabled;

    public bool IsODMNEnabled {
      get {
        if (!_IsODMNEnabled.HasValue) {
          _IsODMNEnabled = this.ExtendedAccessLists.Any() && this.ExtendedAccessLists.Select(c => c.Name).Any(c => new Regex(@"ODMN_ONLY_TRAFFIC_ACL", RegexOptions.IgnoreCase).Match(c).Success);
        }
        return _IsODMNEnabled.Value;
      }
    }

    public string TacacsSourceInterface {
      get {
        var match = GetConfigurationSetting(new Regex(@"^ip\s+tacacs\s+source-interface\s+(\w+)$", RegexOptions.IgnoreCase));
        return (match == null) ? null : match.Groups[1].Value;
      }
    }

    public string Domain {
      get {
        var match = GetConfigurationSetting(new Regex(IOSRegex.DOMAIN_NAME, RegexOptions.IgnoreCase));
        return (match.Groups.Count < 2) ? null : match.Groups[2].Value;
      }
    }

    public string CryptoKeyName {
      get {
        var match = GetConfigurationSetting(new Regex(IOSRegex.CRYPTO_KEY_NAME, RegexOptions.IgnoreCase));
        string result = string.Empty;
        if (match != null) {
          if (!string.IsNullOrEmpty(match.Groups[1].Value)) {
            var subValues = match.Groups[1].Value.Split('.');
            result = string.Format(@"{0}.{1}", subValues[subValues.Count() - 2], subValues[subValues.Count() - 1]);
          }
        }
        return result;
      }
    }

    public IEnumerable<string> Banner {
      get {
        var lines = new List<string>();
        var bannerFound = false;
        for (int i = 0; i < configLength; i++) {
          if (config.ElementAt(i).Contains(@"banner login ^C")) {
            bannerFound = true;
            lines.Add(config.ElementAt(i++));
            while (true) {
              if (bannerFound && config.ElementAt(i).Trim().Contains(@"^C")) {
                lines.Add(config.ElementAt(i));
                break;
              }
              lines.Add(config.ElementAt(i++));
            }
          } else if (bannerFound) {
            //should only hit this once finished with interface processing
            break;
          }
        }
        return lines;
      }
    }

    public bool BootNetwork {
      get { return !string.IsNullOrEmpty(base.GetConfigurationSetting("boot network")); }
    }

    public bool VStackEnabled {
      get {
        return
          string.IsNullOrEmpty(base.GetConfigurationSetting("no vstack")) &&
          string.IsNullOrEmpty(base.GetConfigurationSetting("no vstack enable"));
      }
    }

    public bool ServiceConfig {
      get { return !string.IsNullOrEmpty(base.GetConfigurationSetting("service config")); }
    }

    public IPSettings IPSettings {
      get {
        return base.ParseSettings<IPSettings>();
      }
    }

    public AliasExecSettings AliasExecSettings {
      get {
        return base.ParseSettings<AliasExecSettings>();
      }
    }

    public SNMPSettings SNMPSettings {
      get {
        return base.ParseSettings<SNMPSettings>();
      }
    }

    public LoggingSettings SyslogSettings {
      get {
        return base.ParseSettings<LoggingSettings>();
      }
    }

    public Dot1xSettings Dot1xSettings {
      get {
        return base.ParseSettings<Dot1xSettings>();
      }
    }

    public ServiceSettings ServiceSettings {
      get {
        return base.ParseSettings<ServiceSettings>();
      }
    }

    public TacacsSettings TacacsServer {
      get {
        return base.ParseSettings<TacacsSettings>();
      }
    }

    public NTPSettings NetworkTimeProtocol {
      get {
        return base.ParseSettings<NTPSettings>();
      }
    }

    public AAASettings AAA {
      get {
        return base.ParseSettings<AAASettings>();
      }
    }

    public CryptoSettings Crypto {
      get {
        return base.ParseSettings<CryptoSettings>();
      }
    }

    public UserSettings UserSettings {
      get {
        return base.ParseSettings<UserSettings>();
      }
    }

    public MonitorSettings MonitorSettings {
      get {
        return base.ParseSettings<MonitorSettings>();
      }
    }

    public SpanningTreeSettings SpanningTree {
      get {
        return base.ParseSettings<SpanningTreeSettings>();
      }
    }

    public ShowVersion ShowVersion {
      get {
        return new ShowVersion(base.GetShowCommand("show version"));
      }
    }

    public ShowVtpPassword ShowVtpPassword {
      get {
        return new ShowVtpPassword(base.GetShowCommand("show vtp password"));
      }
    }

    public ShowVtpStatus ShowVtpStatus {
      get {
        return new ShowVtpStatus(base.GetShowCommand("show vtp status"));
      }
    }

    public ShowInventory ShowInventory {
      get {
        return new ShowInventory(base.GetShowCommand("show inventory"));
      }
    }

    public ShowInterface ShowInterface {
      get {
        var tmp = base.GetShowCommand("show interface").Where(c => !string.IsNullOrEmpty(c));
        return new ShowInterface(tmp);
      }
    }

    public ShowInterfaceStatus ShowInterfaceStatus {
      get {
        var tmp = base.GetShowCommand("show interface status").Where(c => !string.IsNullOrEmpty(c));
        return new ShowInterfaceStatus(tmp.Skip(1));
      }
    }

    public ShowInterfacesTrunk ShowInterfacesTrunk {
      get {
        var tmp = base.GetShowCommand("show interfaces trunk").Where(c => !string.IsNullOrEmpty(c));
        return new ShowInterfacesTrunk(tmp.Skip(1));
      }
    }

    public ShowSnmpUser ShowSnmpUser {
      get {
        return new ShowSnmpUser(base.GetShowCommand("show snmp user"));
      }
    }

    public ShowSnmpGroup ShowSnmpGroup {
      get {
        return new ShowSnmpGroup(base.GetShowCommand("show snmp group"));
      }
    }

    public ShowSnmp ShowSnmp {
      get {
        return new ShowSnmp(base.GetShowCommand("show snmp"));
      }
    }

    public ShowClock ShowClock {
      get {
        return new ShowClock(base.GetShowCommand("show clock"));
      }
    }

    public ShowIpInterface ShowIpInterface {
      get {
        return new ShowIpInterface(base.GetShowCommand("show ip interface"));
      }
    }

    public ShowCdpInterface ShowCdpInterface {
      get {
        return new ShowCdpInterface(base.GetShowCommand("show cdp interface"));
      }
    }

    public ShowCdpNeighbor ShowCdpNeighbors {
      get {
        return new ShowCdpNeighbor(base.GetShowCommand("show cdp neighbor"));
      }
    }

    public DirAllFileSystems DirAllFileSystems {
      get {
        return new DirAllFileSystems(base.GetShowCommand("dir all-filesystems"));
      }
    }

    public WriteMem WriteMem {
      get {
        return new WriteMem(base.GetShowCommand("write mem"));
      }
    }

    public bool IsBGPConfigured {
      get { return this.BGP.ASN != -1; }
    }

    public bool IsEIGRPConfigured {
      get { return this.EIGRP.ASN != -1; }
    }

    public bool IsOSPFConfigured {
      get { return this.OSPF.ProcessId != -1; }
    }

    public BorderGatewayProtocol BGP {
      get {
        var settings = new List<string>();
        var bgp = new BorderGatewayProtocol();

        for (int i = 0; i < configLength; i++) {
          if (new Regex(@"router bgp (\d+)", RegexOptions.IgnoreCase).Match(config.ElementAt(i)).Success) {
            settings.Add(config.ElementAt(i));
            while (!new Regex(@"^!$").Match(config.ElementAt(i)).Success) {
              settings.Add(config.ElementAt(++i));
            }
          }
        }

        bgp.Settings = settings;
        return bgp;
      }
    }

    public EnhancedInteriorGatewayRoutingProtocol EIGRP {
      get {
        var settings = new List<string>();
        var eigrp = new EnhancedInteriorGatewayRoutingProtocol();

        for (int i = 0; i < configLength; i++) {
          if (new Regex(@"router eigrp (\d+)", RegexOptions.IgnoreCase).Match(config.ElementAt(i)).Success) {
            settings.Add(config.ElementAt(i));
            while (!new Regex(@"^!$").Match(config.ElementAt(i)).Success) {
              settings.Add(config.ElementAt(++i));
            }
          }
        }

        eigrp.Settings = settings;
        return eigrp;
      }
    }

    public OpenShortestPathFirstProtocol OSPF {
      get {
        var settings = new List<string>();
        var ospf = new OpenShortestPathFirstProtocol();

        for (int i = 0; i < configLength; i++) {
          if (new Regex(@"router ospf (\d+)", RegexOptions.IgnoreCase).Match(config.ElementAt(i)).Success) {
            settings.Add(config.ElementAt(i));
            while (!new Regex(@"^!$").Match(config.ElementAt(i)).Success) {
              settings.Add(config.ElementAt(++i));
            }
          }
        }

        ospf.Settings = settings;
        return ospf;
      }
    }

    private IEnumerable<ExtendedAccessList> _ExtendedAccessLists;

    public IEnumerable<ExtendedAccessList> ExtendedAccessLists {
      get {
        if (_ExtendedAccessLists == null) {
          var acls = new List<ExtendedAccessList>();
          for (int i = 0; i < configLength; i++) {
            var rgx = new Regex(@"^ip access-list extended (.*)", RegexOptions.IgnoreCase);
            var m = rgx.Match(config.ElementAt(i));
            if (m.Success) {
              var acl = new ExtendedAccessList();
              acls.Add(acl);
              var rules = new List<string>();
              acl.Name = m.Groups[1].Value;
              while (!new Regex(@"^!$").Match(config.ElementAt(i)).Success && !rgx.Match(config.ElementAt(i + 1)).Success) {
                rules.Add(config.ElementAt(++i));
              }
              acl.Rules = rules.Where(c => !new Regex(@"^!$").Match(c).Success).ToList();
            }
          }
          _ExtendedAccessLists = acls.GroupBy(c => c.Name).Select(c => c.FirstOrDefault());
        }
        return _ExtendedAccessLists;
      }
    }

    public IEnumerable<RouteMap> RouteMaps {
      get {
        var routeMaps = new List<RouteMap>();
        for (int i = 0; i < configLength; i++) {
          var rgx = new Regex(@"route-map (?<name>[\w-]+) (?<access>permit|deny) (?<seq_number>\d+)", RegexOptions.IgnoreCase);
          var m = rgx.Match(config.ElementAt(i));
          if (m.Success) {
            var routeMap = new RouteMap();
            routeMaps.Add(routeMap);
            var rules = new List<string>();
            routeMap.Name = m.Groups["name"].Value;
            while (!new Regex(@"^!$").Match(config.ElementAt(i)).Success && !rgx.Match(config.ElementAt(i + 1)).Success) {
              rules.Add(config.ElementAt(++i));
            }
            routeMap.Matches = rules.Where(c => !new Regex(@"^!$").Match(c).Success).ToList();
          }
        }
        return routeMaps;
      }
    }

    public IEnumerable<StandardAccessList> StandardAccessLists {
      get {
        var acls = new List<StandardAccessList>();
        for (int i = 0; i < configLength; ) {
          var rgx = new Regex(@"^access-list (\d+) .*", RegexOptions.IgnoreCase);
          var m = rgx.Match(config.ElementAt(i));

          if (m.Success) {
            int currentAclNumber = int.Parse(m.Groups[1].Value);
            var rules = new List<string>();
            var acl = new StandardAccessList();

            acls.Add(acl);
            acl.Rules = rules;

            rules.Add(config.ElementAt(i++));

            while (rgx.Match(config.ElementAt(i)).Success && int.Parse(rgx.Match(config.ElementAt(i)).Groups[1].Value) == currentAclNumber) {
              rules.Add(config.ElementAt(i++));
            }
          } else { i++; }
        }
        return acls;
      }
    }

    public IEnumerable<Vlan> Vlans {
      get {
        var vlans = new List<Vlan>();
        var vlanRgx = new Regex(@"^vlan (?<number>\d+)", RegexOptions.IgnoreCase);
        var vlanNameRgx = new Regex(@"^\s+name (?<name>.*)", RegexOptions.IgnoreCase);
        for (int i = 0; i < configLength; i++) {
          var m = vlanRgx.Match(config.ElementAt(i));
          if (m.Success) {
            while (vlanRgx.Match(config.ElementAt(i)).Success ||
                   vlanNameRgx.Match(config.ElementAt(i)).Success ||
                   BANG_REGEX.Match(config.ElementAt(i)).Success) {
              if (new Regex("^!").Match(config.ElementAt(i)).Success) { i++; continue; }
              var vlanMatch = vlanRgx.Match(config.ElementAt(i++));
              var vlanNameMatch = vlanNameRgx.Match(config.ElementAt(i++));
              var vlan = new Vlan();
              vlan.Number = int.Parse(vlanMatch.Groups["number"].Value);
              vlan.ShortDescription = vlanNameMatch.Groups["name"].Value;
              vlans.Add(vlan);
            }
            break;
          }
        }
        return vlans;
      }
    }
  }
}