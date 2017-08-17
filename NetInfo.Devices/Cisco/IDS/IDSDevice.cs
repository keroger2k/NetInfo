using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices.Cisco.IDS.Commands;
using NetInfo.Devices.IDS;

namespace NetInfo.Devices.IPS {

  public class IDSDevice : Device, IIDSDevice {

    public IDSDevice(IAssetBlob AssetBlob) :
      base(AssetBlob) {
    }

    public string Hostname {
      get {
        var result = GetConfigurationSetting(new Regex(@"^\s*host-name (\w+)$", RegexOptions.IgnoreCase));
        return (result == null) ? string.Empty : result.Groups[1].Value;
      }
    }

    public string Timezone {
      get {
        var result = GetConfigurationSetting(new Regex(@"^\s*standard-time-zone-name[:]? (\w+)[ <defaulted>]*?$", RegexOptions.IgnoreCase));
        return (result == null) ? string.Empty : result.Groups[1].Value;
      }
    }

    public int TimezoneOffset {
      get {
        var result = GetConfigurationSetting(new Regex(@"^\s*offset\s+(\d+)$", RegexOptions.IgnoreCase));
        return (result == null) ? -1 : int.Parse(result.Groups[1].Value);
      }
    }

    public bool TelnetOptionDisabled {
      get {
        var result = GetConfigurationSetting(new Regex(@"telnet-option disabled", RegexOptions.IgnoreCase));
        return (result != null);
      }
    }

    public bool VirtualSensorVS0 {
      get {
        var result = GetConfigurationSetting(new Regex(@"virtual-sensor vs0", RegexOptions.IgnoreCase));
        return (result != null);
      }
    }

    public ShowUsersAll ShowUsersAll {
      get {
        return new ShowUsersAll(GetShowCommand("show users all").Skip(1));
      }
    }

    protected override IEnumerable<string> GetShowCommand(string command) {
      var commandOutput = new List<string>();
      var regex = new Regex(@"^.*#.*$", RegexOptions.IgnoreCase);
      for (var i = 0; i < configLength; i++) {
        var line = config.ElementAt(i);
        if (new Regex(string.Format("^.*#\\s+{0}", command), RegexOptions.IgnoreCase).Match(line).Success) {
          while (!regex.Match(config.ElementAt(++i)).Success) {
            commandOutput.Add(config.ElementAt(i));
          }
          break;
        }
      }
      return commandOutput;
    }
  }
}