using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands {

  public class ShowSnmpGroup : BaseSetting {
    private readonly bool SNMP_ENABLED;

    public ShowSnmpGroup(IEnumerable<string> settings) {
      Settings = settings;
      this.SNMP_ENABLED = !Settings.Contains("%SNMP agent not enabled");
    }

    public IEnumerable<GroupSetting> GroupSettings {
      get {
        if (!SNMP_ENABLED) {
          return null;
        }

        var list = new List<GroupSetting>();
        for (int i = 0; i < Settings.Count(); i++) {
          var settings = new List<string>();

          while (!string.IsNullOrEmpty(Settings.ElementAt(i))) {
            settings.Add(Settings.ElementAt(i++));
          }
          if (settings.Any()) {
            var s = new GroupSetting();
            s.Settings = settings;
            list.Add(s);
          }
        }
        return list;
      }
    }

    public class GroupSetting : BaseSetting {

      public string GroupName {
        get {
          var r = GetSetting(new Regex(@"groupname: (?<groupName>\w+)", RegexOptions.IgnoreCase));
          return (r == null) ? null : r.Groups["groupName"].Value;
        }
      }
    }
  }
}