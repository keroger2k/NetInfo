using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public class ConsoleSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^(set|unset) console .*", RegexOptions.IgnoreCase); }
    }

    public int Timeout {
      get {
        var result = GetSetting(new Regex(@"^set console timeout (\d+)$", RegexOptions.IgnoreCase));
        return result != null ? int.Parse(result.Groups[1].Value) : -1;
      }
    }

    public int Page {
      get {
        var result = GetSetting(new Regex(@"^set console page (\d+)$", RegexOptions.IgnoreCase));
        return result != null ? int.Parse(result.Groups[1].Value) : -1;
      }
    }
  }
}