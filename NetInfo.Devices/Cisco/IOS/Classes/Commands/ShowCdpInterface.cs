using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands {

  public class ShowCdpInterface : BaseSetting {

    private readonly Regex rgxInterface = new Regex(
      string.Format(@"^(?<interface>{0}) is (?<enabled>up|down), line protocol is (?<protocol>up|down)",
      IOSInterface.INTERFACE_TYPES), RegexOptions.IgnoreCase);

    private readonly bool CDP_ENABLED;

    public bool CDPEnabled {
      get { return CDP_ENABLED; }
    }

    public ShowCdpInterface(IEnumerable<string> settings) {
      this.Settings = settings;
      this.CDP_ENABLED = !Settings.Contains("% CDP is not enabled");
    }

    public IEnumerable<CDPInterface> Interfaces {
      get {
        var list = new List<CDPInterface>();
        for (int i = 0; i < Settings.Count(); i++) {
          var m = rgxInterface.Match(Settings.ElementAt(i));
          if (m.Success) {
            list.Add(new CDPInterface {
              Name = m.Groups["interface"].Value,
              Enabled = m.Groups["enabled"].Value.Equals("up"),
              Protocol = m.Groups["protocol"].Value.Equals("up")
            });
          }
        }
        return list;
      }
    }

    public class CDPInterface {

      public string Port {
        get {
          var firstSlash = this.Name.IndexOf('/');
          if (firstSlash > 0) {
            return this.Name.Substring(firstSlash - 1, this.Name.Length - (firstSlash - 1));
          }
          return string.Empty;
        }
      }

      public string Name { get; set; }

      public bool Enabled { get; set; }

      public bool Protocol { get; set; }
    }
  }
}