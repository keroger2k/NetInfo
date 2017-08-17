using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS {

  public class Dot1xSettings : BaseSetting, IConfigSetting {

    public bool SystemAuthControl {
      get {
        var r = GetSetting(new Regex(@"^dot1x system-auth-control$", RegexOptions.IgnoreCase));
        return (r != null);
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^dot1x .*", RegexOptions.IgnoreCase); }
    }
  }
}