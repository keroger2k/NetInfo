using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands {

  public sealed class ShowInterface : BaseSetting {

    public ShowInterface(IEnumerable<string> settings) {
      this.Settings = settings;
    }

    public IEnumerable<Interface> Interfaces {
      get {
        var interfaceList = new List<Interface>();
        int settingCount = Settings.Count();
        for (int i = 0; i < settingCount; i++) {
          var m = Interface.InterfaceRgx.Match(Settings.ElementAt(i));
          if (m.Success) {
            var list = new List<string>();
            list.Add(Settings.ElementAt(i++));
            while (!Interface.InterfaceRgx.Match(Settings.ElementAt(i)).Success) {
              list.Add(Settings.ElementAt(i++));
              if (i == Settings.Count()) { break; }
            }
            interfaceList.Add(new Interface(list));
            i--;
          }
        }
        return interfaceList;
      }
    }

    public class Interface : BaseSetting {
      public static Regex InterfaceRgx = new Regex(@"^(?<interfaceName>.*) is (administratively )?(?<interfaceStatus>down|up), line protocol is (?<protocolStatus>up|down)", RegexOptions.IgnoreCase);
      private Regex descriptionRgx = new Regex(@"\s+Description:\s+(?<description>.*)", RegexOptions.IgnoreCase);

      public enum ProtocolStatus {
        up,
        down
      }

      public Interface(IEnumerable<string> settings) {
        this.Settings = settings;
        var m = InterfaceRgx.Match(settings.First());
        if (m.Success) {
          this.Name = m.Groups["interfaceName"].Value;
          this.Enabled = m.Groups["interfaceStatus"].Value.Equals("up", StringComparison.OrdinalIgnoreCase);
          this.Protocol = (ProtocolStatus)Enum.Parse(typeof(ProtocolStatus), m.Groups["protocolStatus"].Value);
        }
      }

      public string Name { get; private set; }

      public string Description {
        get {
          var m = Settings.FirstOrDefault(c => descriptionRgx.Match(c).Success);
          return m == null ? string.Empty : descriptionRgx.Match(m).Groups["description"].Value;
        }
      }

      public bool Enabled { get; private set; }

      public ProtocolStatus Protocol { get; private set; }
    }
  }
}