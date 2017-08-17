using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS.Commands {

  public class GetLicense {
    private readonly IEnumerable<string> _output;

    public GetLicense(IEnumerable<string> output) {
      this._output = output;
    }

    public string Capacity {
      get {
        foreach (var item in _output) {
          var rgx = new Regex(@"Capacity:\s+(.*)", RegexOptions.IgnoreCase);
          if (rgx.Match(item).Success) {
            return rgx.Match(item).Groups[1].Value;
          }
        }
        return string.Empty;
      }
    }
  }
}