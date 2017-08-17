using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands {

  public class WriteMem : BaseSetting {

    public WriteMem(IEnumerable<string> settings) {
      Settings = settings;
    }

    public bool Success {
      get {
        return Settings.Any(c => new Regex(@"\[OK\]", RegexOptions.IgnoreCase).Match(c).Success);
      }
    }
  }
}