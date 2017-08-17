using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS {

  public class AliasExecSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^alias exec (.*)$", RegexOptions.IgnoreCase); }
    }

    public int Count {
      get { return Settings.Count(); }
    }
  }
}