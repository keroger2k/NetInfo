using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS {

  public class AliasSettings : BaseSetting, IConfigSetting {

    public string GetValue(string value) {
      var r = GetSetting(new Regex(string.Format("alias {0}=(.*)", value), RegexOptions.IgnoreCase));
      if (r != null) {
        return r.Groups[1].Value;
      }
      return string.Empty;
    }

    public Regex GenericRegex {
      get { return new Regex(@"^alias.*", RegexOptions.IgnoreCase); }
    }
  }
}