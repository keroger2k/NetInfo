using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public class SSLSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^(set|unset) ssl .*", RegexOptions.IgnoreCase); }
    }

    public bool Eanbled {
      get {
        var result = GetSetting(new Regex(@"^set ssl enable$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public string Encryption {
      get {
        var result = GetSetting(new Regex(@"^set ssl encrypt (.*)$", RegexOptions.IgnoreCase));
        return result != null ? result.Groups[1].Value : string.Empty;
      }
    }
  }
}