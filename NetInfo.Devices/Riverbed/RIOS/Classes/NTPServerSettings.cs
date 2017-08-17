using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Riverbed.RIOS {

  public class NTPServerSettings : BaseSetting, IConfigSetting {

    public IEnumerable<NTPServer> Servers {
      get {
        var r = GetSettings(GenericRegex);
        return r.Select(c => new NTPServer {
          Address = IPAddress.Parse(c.Groups[1].Value),
          Version = int.Parse(c.Groups[2].Value)
        });
      }
    }

    public class NTPServer {

      public IPAddress Address { get; set; }

      public int Version { get; set; }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^\s*ntp server (\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}) version ""(\d)""$", RegexOptions.IgnoreCase); }
    }
  }
}