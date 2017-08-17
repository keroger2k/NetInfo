using System.Text.RegularExpressions;

namespace NetInfo.Devices.McAfee {

  public class SensorInfoConfig : BaseSetting, IConfigSetting {

    public string SystemName {
      get {
        var setting = GetSetting(new Regex(@"^System\s+Name\s+:\s+(\w+)$", RegexOptions.IgnoreCase));
        return (setting == null) ? string.Empty : setting.Groups[1].Value;
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"\[Sensor Info\]", RegexOptions.IgnoreCase); }
    }
  }
}