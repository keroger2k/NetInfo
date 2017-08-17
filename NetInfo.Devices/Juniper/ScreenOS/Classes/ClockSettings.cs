using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public class ClockSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^set clock .*$", RegexOptions.IgnoreCase); }
    }

    public bool DaylightSavingsTimeOffEnabled {
      get {
        var result = GetSetting(new Regex(@"^set clock dst-off$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public bool NTPEnabled {
      get {
        var result = GetSetting(new Regex(@"^set clock ntp$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public int Timezone {
      get {
        var result = GetSetting(new Regex(@"^set clock timezone (\d+)$", RegexOptions.IgnoreCase));
        return result != null ? int.Parse(result.Groups[1].Value) : -1;
      }
    }
  }
}