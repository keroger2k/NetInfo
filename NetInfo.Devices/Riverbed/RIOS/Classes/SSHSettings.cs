using System.Text.RegularExpressions;

namespace NetInfo.Devices.Riverbed.RIOS {

  public class SSHSettings : BaseSetting, IConfigSetting {

    public bool V2OnlyEnable {
      get {
        var r = GetSetting(new Regex(@"^\s*ssh server v2-only enable$", RegexOptions.IgnoreCase));
        return (r != null);
      }
    }

    public bool ServerListenEnable {
      get {
        var r = GetSetting(new Regex(@"^\s*ssh server listen enable$", RegexOptions.IgnoreCase));
        return (r != null);
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^\s*ssh server (.+)$", RegexOptions.IgnoreCase); }
    }
  }
}