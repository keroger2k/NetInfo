using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Riverbed.RIOS {

  public class TacacsSettings : BaseSetting, IConfigSetting {

    public IEnumerable<TacacsServer> Hosts {
      get {
        var matches = GetSettings(new Regex(@"tacacs-server\s+host\s+(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})", RegexOptions.IgnoreCase));
        return matches.Select(c => new TacacsServer {
          Host = IPAddress.Parse(c.Groups[1].Value)
        }).GroupBy(c => c.Host).Select(c => c.First());
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"tacacs-server\s+(.*)$", RegexOptions.IgnoreCase); }
    }

    public class TacacsServer {

      public IPAddress Host { get; set; }

      public string Key { get; set; }
    }
  }
}