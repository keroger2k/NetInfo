using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public class NTPSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^(set|unset) ntp .*", RegexOptions.IgnoreCase); }
    }

    public IPAddress Server {
      get {
        var result = GetSetting(new Regex(@"set ntp server \""(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\""$", RegexOptions.IgnoreCase));
        return result != null ? IPAddress.Parse(result.Groups[1].Value) : IPAddress.Parse("0.0.0.0");
      }
    }

    public int Interval {
      get {
        var result = GetSetting(new Regex(@"^set ntp interval (\d+)$", RegexOptions.IgnoreCase));
        return result != null ? int.Parse(result.Groups[1].Value) : 0;
      }
    }
  }
}