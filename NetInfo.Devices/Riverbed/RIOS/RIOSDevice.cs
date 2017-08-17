using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices.Riverbed.RIOS.Commands;

namespace NetInfo.Devices.Riverbed.RIOS {

  public class RIOSDevice : Device, IRIOSDevice {

    public RIOSDevice(IAssetBlob AssetBlob) :
      base(AssetBlob) {
    }

    public string Hostname {
      get {
        var match = GetConfigurationSetting(new Regex(@"hostname ""(.+)""$", RegexOptions.IgnoreCase));
        return (match == null) ? null : match.Groups[1].Value;
      }
    }

    private string _Model;

    public string Model {
      get {
        if (string.IsNullOrEmpty(_Model)) {
          _Model = this.ShowInfo.Model;
        }
        return _Model;
      }
    }

    public string GetClock() {
      var regex = new Regex(@"(^\s*clock timezone\s\w+$)", RegexOptions.IgnoreCase);
      var x = GetConfigurationSetting(regex);
      return (x == null) ? null : x.Groups[1].Value;
    }

    public IEnumerable<string> Banner {
      get {
        var lines = new List<string>();
        var bannerFound = false;
        for (int i = 0; i < configLength; i++) {
          if (config.ElementAt(i).Contains(@"banner login """)) {
            bannerFound = true;
            while (true) {
              //have to check for cli-default command because older test scripts don't have a blank line afterwards
              //and is throwing errors.
              if (config.ElementAt(i).Trim().Equals(@"""") || config.ElementAt(i).Trim().Contains(@"cli default auto-logout 3.0")) {
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

    public string Status {
      get {
        var statusRegx = new Regex(@"^\s*Status:\s+(\w+)", RegexOptions.IgnoreCase);
        var result = config.FirstOrDefault(c => statusRegx.Match(c).Success);
        return result == null ? "" : statusRegx.Match(result).Groups[1].Value;
      }
    }

    public bool OptimizationServiceEnabled {
      get {
        var regex = new Regex(@"^\s*Optimization Service: (\w+)$", RegexOptions.IgnoreCase);
        var x = GetConfigurationSetting(regex);
        return (x != null) ? x.Groups[1].Value.Equals("running", StringComparison.InvariantCultureIgnoreCase) : false;
      }
    }

    public SNMPSettings SNMP {
      get {
        return base.ParseSettings<SNMPSettings>();
      }
    }

    public NTPServerSettings NTP {
      get {
        return base.ParseSettings<NTPServerSettings>();
      }
    }

    public SSHSettings SSH {
      get {
        return base.ParseSettings<SSHSettings>();
      }
    }

    public JobSettings JobSettings {
      get {
        return base.ParseSettings<JobSettings>();
      }
    }

    public UserSettings UserSettings {
      get {
        return base.ParseSettings<UserSettings>();
      }
    }

    public ShowBoot ShowBoot {
      get {
        return new ShowBoot(GetShowCommand("show boot"));
      }
    }

    public ShowVersion ShowVersion {
      get {
        return new ShowVersion(GetShowCommand("show version"));
      }
    }

    public ShowInfo ShowInfo {
      get {
        return new ShowInfo(GetShowCommand("show info"));
      }
    }

    public ShowInterfaceBrief ShowInterfaceBrief {
      get {
        return new ShowInterfaceBrief(GetShowCommand("show interface brief"));
      }
    }

    public WebSettings Web {
      get {
        return base.ParseSettings<WebSettings>();
      }
    }

    public TacacsSettings Tacacs {
      get {
        return base.ParseSettings<TacacsSettings>();
      }
    }

    public AAASettings AAA {
      get {
        return base.ParseSettings<AAASettings>();
      }
    }

    protected override IEnumerable<string> GetShowCommand(string command) {
      var commandOutput = new List<string>();
      var commandRgx = new Regex(string.Format(@".*\s+#\s+{0}", command), RegexOptions.IgnoreCase);
      for (var i = 0; i < configLength; i++) {
        var line = config.ElementAt(i);
        if (commandRgx.Match(line).Success) {
          while (!config.ElementAt(i).Contains("# #")) {
            commandOutput.Add(config.ElementAt(++i));
          }
          break;
        }
      }
      return commandOutput.Take(commandOutput.Count() - 1);
    }
  }
}