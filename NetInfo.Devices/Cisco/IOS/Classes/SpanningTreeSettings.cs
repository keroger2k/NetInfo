using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS {

  public class SpanningTreeSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^spanning-tree vlan (.*)$", RegexOptions.IgnoreCase); }
    }

    public int Count {
      get { return Settings.Count(); }
    }
  }
}