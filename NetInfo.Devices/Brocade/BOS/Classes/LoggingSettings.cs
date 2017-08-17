using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS {

  public class LoggingSettings : BaseSetting, IConfigSetting {

    public IEnumerable<IPAddress> Hosts {
      get {
        var r = GetSettings(new Regex(@"^logging host (?<address>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})$", RegexOptions.IgnoreCase));
        return (r == null) ? new List<IPAddress>() : r.Select(c => IPAddress.Parse(c.Groups["address"].Value)).Distinct();
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^(no\s+)?logging.*", RegexOptions.IgnoreCase); }
    }
  }
}