using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Riverbed.RIOS.Commands {

  public class ShowVersion : BaseSetting {

    public ShowVersion(IEnumerable<string> output) {
      this.Settings = output;
    }

    public string OSRelease {
      get {
        var r = GetSetting(new Regex(@"Product release:\s+(?<release>.*)", RegexOptions.IgnoreCase));
        return r == null ? string.Empty : r.Groups["release"].Value;
      }
    }

    public string Model {
      get {
        var r = GetSetting(new Regex(@"Product model:\s+(?<release>.*)", RegexOptions.IgnoreCase));
        return r == null ? string.Empty : r.Groups["release"].Value;
      }
    }
  }
}