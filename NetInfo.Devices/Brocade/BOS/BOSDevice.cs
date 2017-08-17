using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using NetInfo.Devices.Brocade.BOS.Classes;
using NetInfo.Devices.Brocade.BOS.Classes.Commands;
using NetInfo.Devices.Brocade.BOS.Commands;

namespace NetInfo.Devices.Brocade.BOS {

  public class BOSDevice : Device, IBOSDevice {
    private static readonly Regex BANG_REGEX = new Regex(@"^!$", RegexOptions.IgnoreCase);

    public BOSDevice(IAssetBlob AssetBlob) :
      base(AssetBlob) {
    }

    public int ConsoleTimeOut {
      get {
        var regex = new Regex(@"^console timeout (\d+)$", RegexOptions.IgnoreCase);
        var x = GetConfigurationSetting(regex);
        return (x == null) ? 0 : int.Parse(x.Groups[1].Value);
      }
    }

    public WriteMem WriteMem {
      get {
        for (var i = 0; i < configLength; i++) {
          var line = config.ElementAt(i);
          if (line.Contains("write memory")) {
            return new WriteMem { Success = new Regex(@"\.\.Write startup-config done\.|^.*#|^.*>$").Match(config.ElementAt(i + 1)).Success };
          }
        }
        return new WriteMem { Success = false };
      }
    }

    public Password SuperPassword {
      get {
        var regex = new Regex(@"^enable super-user-password (\d) (.+)$", RegexOptions.IgnoreCase);
        var x = GetConfigurationSetting(regex);
        return (x == null) ? new Password() : new Password {
          Value = x.Groups[2].Value,
          Type = int.Parse(x.Groups[1].Value)
        };
      }
    }

    public GlobalDot1x GlobalDot1xSettings {
      get {
        GlobalDot1x result = null;
        bool found = false;
        foreach (var line in AssetBlob.Configuration) {
          if (line.Equals("dot1x-enable", StringComparison.InvariantCultureIgnoreCase)) {
            result = new GlobalDot1x();
            result.Enabled = found = true;
          }
          if (found && line.Equals(" re-authentication", StringComparison.InvariantCultureIgnoreCase)) {
            result.ReAuthentication = true;
          }
          if (found && line.Equals(" servertimeout 15", StringComparison.InvariantCultureIgnoreCase)) {
            result.ServerTimeout = int.Parse(line.Trim().Split(' ')[1]);
          }
          if (found && line.Equals(" timeout quiet-period 30", StringComparison.InvariantCultureIgnoreCase)) {
            result.TimeoutQuietPeriod = int.Parse(line.Trim().Split(' ')[2]);
          }
          if (found && line.Equals("!")) {
            break;
          }
        }
        return result;
      }
    }

    private string _hostname;

    public string Hostname {
      get {
        if (_hostname == null) {
          var regex = new Regex(@"^hostname (.+)$", RegexOptions.IgnoreCase);
          var match = GetConfigurationSetting(regex);
          _hostname = (match == null) ? null : match.Groups[1].Value;
        }
        return _hostname;
      }
    }

    public IEnumerable<Vlan> Vlans {
      get {
        var vlans = new List<Vlan>();
        var firstVlan = false;
        var vlanRgx = new Regex(@"vlan (?<number>\d+)( name (?<name>[\w-]+))? by port", RegexOptions.IgnoreCase);
        for (int i = 0; i < configLength; i++) {
          var m = vlanRgx.Match(config.ElementAt(i));
          if (m.Success) {
            firstVlan = true;
            var vlan = new Vlan();
            vlan.Name = m.Groups["name"].Value;
            vlan.Number = int.Parse(m.Groups["number"].Value);
            if (!BANG_REGEX.Match(config.ElementAt(i + 1)).Success) {
              var commands = new List<string>();
              while (true) {
                if (BANG_REGEX.Match(config.ElementAt(++i)).Success) {
                  break;
                }
                vlan.Commands.Add(config.ElementAt(i));
              }
            } else {
              i++;
            }
            vlans.Add(vlan);
          } else if (firstVlan) {
            //should only hit this once finished processing
            break;
          }
        }
        return vlans;
      }
    }

    public IEnumerable<BOSInterface> Interfaces {
      get {
        var interfaces = new List<BOSInterface>();
        var firstInterface = false;
        for (int i = 0; i < configLength; i++) {
          if (BOSInterface.INTERFACE_REGEX.Match(config.ElementAt(i)).Success) {
            firstInterface = true;
            var commands = new List<string>();
            commands.Add(config.ElementAt(i++));
            while (true) {
              if (BANG_REGEX.Match(config.ElementAt(i)).Success) {
                break;
              }
              commands.Add(config.ElementAt(i++));
            }
            interfaces.Add(new BOSInterface(commands));
          } else if (firstInterface) {
            //should only hit this once finished with interface processing
            break;
          }
        }
        return interfaces;
      }
    }

    public bool PasswordEncryption {
      get {
        var regex = new Regex(@"^no service password-encryption$", RegexOptions.IgnoreCase);
        var match = GetConfigurationSetting(regex);
        return (match == null);
      }
    }

    public bool TelnetServer {
      get {
        var regex = new Regex(@"^no telnet server$", RegexOptions.IgnoreCase);
        var match = GetConfigurationSetting(regex);
        return (match == null);
      }
    }

    public IEnumerable<string> Banner {
      get {
        var lines = new List<string>();
        var bannerFound = false;
        for (int i = 0; i < configLength; i++) {
          if (config.ElementAt(i).Contains(@"banner motd ^C")) {
            bannerFound = true;
            while (true) {
              if (config.ElementAt(i).Trim().Equals(@"!")) {
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

    public IPSSHSettings SSH {
      get {
        return base.ParseSettings<IPSSHSettings>();
      }
    }

    public IEnumerable<RadiusServer> RadiusServers {
      get {
        var matches = GetConfigurationMatches(new Regex(@"^radius-server host (\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}) auth-port (\d+) acct-port (\d+) default key (\d) (.+) dot1x", RegexOptions.IgnoreCase));
        if (matches.Any()) {
          return matches.Select(c => new RadiusServer {
            Host = IPAddress.Parse(c.Groups[1].Value),
            AuthPort = int.Parse(c.Groups[2].Value),
            AcctPort = int.Parse(c.Groups[3].Value),
            Key = new Password {
              Type = int.Parse(c.Groups[4].Value),
              Value = c.Groups[5].Value
            }
          });
        }
        return new List<RadiusServer>();
      }
    }

    public WebManagement WebManagement {
      get {
        return base.ParseSettings<WebManagement>();
      }
    }

    public SNTPSettings SNTP {
      get {
        return base.ParseSettings<SNTPSettings>();
      }
    }

    public TacacsSettings TacacsServer {
      get {
        return base.ParseSettings<TacacsSettings>();
      }
    }

    public SNMPSettings SNMP {
      get {
        return base.ParseSettings<SNMPSettings>();
      }
    }

    public UserSettings UserSettings {
      get {
        return base.ParseSettings<UserSettings>();
      }
    }

    public LoggingSettings LoggingSettings {
      get {
        return base.ParseSettings<LoggingSettings>();
      }
    }

    public AAASettings AAA {
      get {
        return base.ParseSettings<AAASettings>();
      }
    }

    public AliasSettings AliasSettings {
      get {
        return base.ParseSettings<AliasSettings>();
      }
    }

    public ShowInterfaceBrief ShowInterfaceBrief {
      get {
        var tmp = GetShowCommand("show interfaces brief").Where(c => !string.IsNullOrEmpty(c));
        return new ShowInterfaceBrief(tmp.Skip(1));
      }
    }

    public ShowInterface ShowInterface {
      get {
        var tmp = GetShowCommand("show interfaces").Where(c => !string.IsNullOrEmpty(c));
        return new ShowInterface(tmp);
      }
    }

    public ShowSnmpUser ShowSnmpUser {
      get {
        return new ShowSnmpUser(GetShowCommand("show snmp user"));
      }
    }

    public ShowDot1x ShowDot1xResults {
      get {
        return new ShowDot1x(GetShowCommand("show dot1x"));
      }
    }

    public ShowVersion ShowVersion {
      get {
        return new ShowVersion(GetShowCommand("show version"));
      }
    }

    public ShowVlan ShowVlan {
      get {
        return new ShowVlan(GetShowCommand("show vlan"));
      }
    }

    protected override IEnumerable<string> GetShowCommand(string command) {
      var commandOutput = new List<string>();
      var regex = new Regex(string.Format(@"^.*{0}#|^.*{0}>$|END-OF-TEST-SCRIPT", this.Hostname), RegexOptions.IgnoreCase);
      var commandRegex = new Regex(string.Format(@"^.*#{0}$|^.*>{0}$", command), RegexOptions.IgnoreCase);
      for (var i = 0; i < configLength; i++) {
        var line = config.ElementAt(i);
        if (commandRegex.Match(line).Success) {
          while (!regex.Match(config.ElementAt(++i)).Success) {
            commandOutput.Add(config.ElementAt(i));
            if ((i + 1) == configLength) {
              throw new ArgumentOutOfRangeException("Unable to find show command");
            }
          }
          break;
        }
      }
      return commandOutput.Take(commandOutput.Count() - 1);
    }

    public IEnumerable<ExtendedAccessList> ExtendedAccessLists {
      get {
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
        return acls;
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
  }
}