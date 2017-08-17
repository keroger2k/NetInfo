using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS {

  public class WebManagement : BaseSetting, IConfigSetting {

    public bool Http {
      get {
        return !Settings.Any(c => c.Equals("no web-management http"));
      }
    }

    public bool HpTopTools {
      get {
        return !Settings.Any(c => c.Equals("no web-management hp-top-tools"));
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^(no web-management|web-management).*$", RegexOptions.IgnoreCase); }
    }
  }
}