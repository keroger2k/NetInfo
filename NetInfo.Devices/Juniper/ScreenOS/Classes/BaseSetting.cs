using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public class BaseSetting {

    public IEnumerable<string> Settings { get; set; }

    protected virtual Match GetSetting(Regex rgx) {
      foreach (var line in Settings) {
        if (rgx.Match(line).Success) {
          return rgx.Match(line);
        }
      }
      return null;
    }

    protected virtual IEnumerable<Match> GetSettings(Regex rgx) {
      var list = new List<Match>();
      foreach (var line in Settings) {
        if (rgx.Match(line).Success) {
          list.Add(rgx.Match(line));
        }
      }
      return list;
    }
  }
}