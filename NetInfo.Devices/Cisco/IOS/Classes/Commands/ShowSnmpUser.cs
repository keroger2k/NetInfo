using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands {

  public class ShowSnmpUser : BaseSetting {
    private readonly bool SNMP_ENABLED;

    public ShowSnmpUser(IEnumerable<string> settings) {
      Settings = settings;
      this.SNMP_ENABLED = !Settings.Contains("%SNMP agent not enabled");
    }

    public IEnumerable<UserSetting> UserSettings {
      get {
        if (!SNMP_ENABLED) {
          return null;
        }

        var list = new List<UserSetting>();
        for (int i = 0; i < Settings.Count(); i++) {
          var settings = new List<string>();

          if (Settings.ElementAt(i).Equals("%SNMP agent not enabled", System.StringComparison.OrdinalIgnoreCase)) {
            break;
          }

          while (!string.IsNullOrEmpty(Settings.ElementAt(i))) {
            settings.Add(Settings.ElementAt(i++));
          }
          if (settings.Any()) {
            var s = new UserSetting();
            s.Settings = settings;
            list.Add(s);
          }
        }
        return list;
      }
    }

    public class UserSetting : BaseSetting {

      public string Username {
        get {
          var r = GetSetting(new Regex(@"User name: (\w+)", RegexOptions.IgnoreCase));
          return (r == null) ? null : r.Groups[1].Value;
        }
      }

      public string EngineId {
        get {
          var r = GetSetting(new Regex(@"Engine ID: (\w+)", RegexOptions.IgnoreCase));
          return (r == null) ? null : r.Groups[1].Value;
        }
      }

      public StorageTypeSettings StorageType {
        get {
          var r = GetSetting(new Regex(@"storage-type:\s+(\w+)\s+(\w+)\s+access-list:\s+(\d+)", RegexOptions.IgnoreCase));
          return (r == null) ? null : new StorageTypeSettings {
            AccessGroup = int.Parse(r.Groups[3].Value),
            Active = r.Groups[2].Value.Equals("active", System.StringComparison.OrdinalIgnoreCase),
            Type = r.Groups[1].Value
          };
        }
      }

      public string AuthenticationProtocol {
        get {
          var r = GetSetting(new Regex(@"Authentication Protocol: (\w+)", RegexOptions.IgnoreCase));
          return (r == null) ? null : r.Groups[1].Value;
        }
      }

      public string PrivacyProtocol {
        get {
          var r = GetSetting(new Regex(@"Privacy Protocol: (\w+)", RegexOptions.IgnoreCase));
          return (r == null) ? null : r.Groups[1].Value;
        }
      }

      public string GroupName {
        get {
          var r = GetSetting(new Regex(@"Group-name: (\w+)", RegexOptions.IgnoreCase));
          return (r == null) ? null : r.Groups[1].Value;
        }
      }

      public class StorageTypeSettings {

        public string Type { get; set; }

        public bool Active { get; set; }

        public int AccessGroup { get; set; }
      }
    }
  }
}