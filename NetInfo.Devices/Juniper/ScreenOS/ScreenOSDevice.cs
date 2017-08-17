using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices.Juniper.ScreenOS.Commands;
using NetInfo.Devices.Juniper.ScreenOS.Patterns;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public class ScreenOSDevice : Device, IScreenOSDevice {

    public ScreenOSDevice(IAssetBlob AssetBlob) :
      base(AssetBlob) {
    }

    public IEnumerable<ScreenOSInterface> Interfaces {
      get {
        var interfaces = new List<ScreenOSInterface>();
        var firstInterface = false;
        for (int i = 0; i < configLength; ) {
          if (ScreenOSInterface.INTERFACE_REGEX.Match(config.ElementAt(i)).Success) {
            firstInterface = true;
            var commands = new List<string>();
            commands.Add(config.ElementAt(i++));
            while (true) {
              if (ScreenOSInterface.INTERFACE_REGEX.Match(config.ElementAt(i)).Success ||
                new Regex(@".*->", RegexOptions.IgnoreCase).Match(config.ElementAt(i)).Success) {
                break;
              }
              commands.Add(config.ElementAt(i++));
            }
            interfaces.Add(new ScreenOSInterface(commands));
          } else if (firstInterface) {
            //should only hit this once finished with interface processing
            break;
          } else {
            i++;
          }
        }
        return interfaces;
      }
    }

    public string GetClockTimezone() {
      var regex = new Regex(ScreenOSRegex.CLOCK_TIMEZONE, RegexOptions.IgnoreCase);
      var x = GetConfigurationSetting(regex);
      return (x == null) ? null : x.Groups[1].Value;
    }

    public string Hostname {
      get {
        var regex = new Regex(ScreenOSRegex.ScreenOS_HOSTNAME, RegexOptions.IgnoreCase);
        var match = GetConfigurationSetting(regex);
        return (match == null) ? null : match.Groups[1].Value;
      }
    }

    public bool AutherServerLocalId {
      get {
        var match = GetConfigurationSetting(new Regex(@"^set auth-server ""Local"" id 0$", RegexOptions.IgnoreCase));
        return (match != null);
      }
    }

    public bool NetworkTimeEnabled {
      get {
        var match = GetConfigurationSetting(new Regex(@"^The Network Time Protocol is Disabled$", RegexOptions.IgnoreCase));
        return (match == null);
      }
    }

    public bool UseInterfaceConfigPort80 {
      get {
        var match = GetConfigurationSetting(new Regex(@"^Use interface IP, Config Port: 80$", RegexOptions.IgnoreCase));
        return (match != null);
      }
    }

    public bool AutherServerName {
      get {
        var match = GetConfigurationSetting(new Regex(@"^set auth-server ""Local"" server-name ""Local""$", RegexOptions.IgnoreCase));
        return (match != null);
      }
    }

    public string AutherServer {
      get {
        var match = GetConfigurationSetting(new Regex(@"^set auth default auth server ""(\w+)""$", RegexOptions.IgnoreCase));
        return (match == null) ? string.Empty : match.Groups[1].Value;
      }
    }

    public bool SCSEnabled {
      get {
        var match = GetConfigurationSetting(new Regex(@"^set scs (\w+)$", RegexOptions.IgnoreCase));
        return (match == null) ? false : match.Groups[1].Value.Equals("enable", System.StringComparison.OrdinalIgnoreCase);
      }
    }

    public bool SSHEnabled {
      get {
        var match = GetConfigurationSetting(new Regex(@"^set ssh (\w+)$", RegexOptions.IgnoreCase));
        return (match == null) ? false : match.Groups[1].Value.Equals("enable", System.StringComparison.OrdinalIgnoreCase);
      }
    }

    public AdminSettings AdminSettings {
      get {
        return ParseSettings<AdminSettings>();
      }
    }

    public InterfaceSettings InterfaceSettings {
      get {
        return ParseSettings<InterfaceSettings>();
      }
    }

    public IKESettings IKESettings {
      get {
        return ParseSettings<IKESettings>();
      }
    }

    public PKISettings PKISettings {
      get {
        return ParseSettings<PKISettings>();
      }
    }

    public XAuthSettings XAuthSettings {
      get {
        return ParseSettings<XAuthSettings>();
      }
    }

    public ClockSettings ClockSettings {
      get {
        return ParseSettings<ClockSettings>();
      }
    }

    public SNMPSettings SNMPSettings {
      get {
        return ParseSettings<SNMPSettings>();
      }
    }

    public FlowSettings FlowSettings {
      get {
        return ParseSettings<FlowSettings>();
      }
    }

    public SSLSettings SSLSettings {
      get {
        return ParseSettings<SSLSettings>();
      }
    }

    public ConsoleSettings ConsoleSettings {
      get {
        return ParseSettings<ConsoleSettings>();
      }
    }

    public AlgSettings AlgSettings {
      get {
        return ParseSettings<AlgSettings>();
      }
    }

    public NTPSettings NTP {
      get {
        return ParseSettings<NTPSettings>();
      }
    }

    public GetRoute GetRoute {
      get {
        return new GetRoute(GetShowCommand("get route"));
      }
    }

    public GetLicense GetLicense {
      get {
        return new GetLicense(GetShowCommand("get license"));
      }
    }

    protected override IEnumerable<string> GetShowCommand(string command) {
      var commandOutput = new List<string>();
      var regex = new Regex(@"^.*->", RegexOptions.IgnoreCase);
      for (var i = 0; i < configLength; i++) {
        var line = config.ElementAt(i);
        if (line.Contains(command)) {
          while (!regex.Match(config.ElementAt(++i)).Success) {
            commandOutput.Add(config.ElementAt(i));
          }
          break;
        }
      }
      return commandOutput.Take(commandOutput.Count() - 1);
    }
  }
}