using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public class XAuthSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^set xauth .*$", RegexOptions.IgnoreCase); }
    }

    public string AuthServer {
      get {
        var result = GetSetting(new Regex(@"^set xauth default auth server (\w+)$", RegexOptions.IgnoreCase));
        return result != null ? result.Groups[1].Value : string.Empty;
      }
    }

    public int Lifetime {
      get {
        var result = GetSetting(new Regex(@"^set xauth lifetime (\d+)$", RegexOptions.IgnoreCase));
        return result != null ? int.Parse(result.Groups[1].Value) : default(int);
      }
    }
  }
}