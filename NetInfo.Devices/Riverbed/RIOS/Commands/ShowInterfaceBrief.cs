using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Riverbed.RIOS.Commands {

  public class ShowInterfaceBrief : BaseSetting {
    private Regex rgxInterface = new Regex(@"Interface\s+(?<name>\w+) state", RegexOptions.IgnoreCase);

    public ShowInterfaceBrief(IEnumerable<string> output) {
      this.Settings = output;
    }

    public IEnumerable<RIOSInterface> Interfaces {
      get {
        var list = new List<RIOSInterface>();
        for (int i = 0; i < Settings.Count(); i++) {
          var m = rgxInterface.Match(Settings.ElementAt(i));
          if (m.Success) {
            var name = m.Groups["name"].Value;
            var settings = new List<string>();
            while (!string.IsNullOrEmpty(Settings.ElementAt(++i))) {
              settings.Add(Settings.ElementAt(i));
            }

            var x = new RIOSInterface(name, settings);
            list.Add(x);
          }
        }
        return list;
      }
    }

    public class RIOSInterface : BaseSetting {

      public RIOSInterface(string Name, IEnumerable<string> output) {
        this.Settings = output;
        this.Name = Name;
      }

      public string Name { get; private set; }

      public bool Up {
        get {
          var r = GetSetting(new Regex(@"\s+Up:\s+yes", RegexOptions.IgnoreCase));
          return r != null;
        }
      }

      public bool Link {
        get {
          var r = GetSetting(new Regex(@"\s+Link:\s+no", RegexOptions.IgnoreCase));
          return r == null;
        }
      }
    }
  }
}