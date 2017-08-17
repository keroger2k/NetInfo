using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public class IKESettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^set ike .*$", RegexOptions.IgnoreCase); }
    }

    public string IdMode {
      get {
        var result = GetSetting(new Regex(@"^set ike id-mode (\w+)$", RegexOptions.IgnoreCase));
        return result != null ? result.Groups[1].Value : string.Empty;
      }
    }

    public bool PolicyCheckingEnabled {
      get {
        var result = GetSetting(new Regex(@"^set ike policy-checking$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public int RepondBadSpi {
      get {
        var result = GetSetting(new Regex(@"^set ike respond-bad-spi (\d+)$", RegexOptions.IgnoreCase));
        return result != null ? int.Parse(result.Groups[1].Value) : default(int);
      }
    }
  }
}