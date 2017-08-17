using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Riverbed.RIOS.Commands {

  public class ShowInfo : BaseSetting {

    public ShowInfo(IEnumerable<string> output) {
      this.Settings = output;
    }

    public string Model {
      get {
        var r = GetSetting(new Regex(@"Model:\s+(?<model>[\w-]+)", RegexOptions.IgnoreCase));
        return r == null ? string.Empty : r.Groups["model"].Value;
      }
    }
  }
}