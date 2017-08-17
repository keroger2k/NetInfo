using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS {

  public class TacacsSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^tacacs-server\s+(.*)$", RegexOptions.IgnoreCase); }
    }

    public IEnumerable<IPAddress> Hosts {
      get {
        var matches = GetSettings(new Regex(@"tacacs-server\s+host\s+(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})", RegexOptions.IgnoreCase));
        foreach (var match in matches) {
          yield return IPAddress.Parse(match.Groups[1].Value);
        }
      }
    }

    public string Key {
      get {
        var r = GetSetting(new Regex(@"tacacs-server\s+key\s+\d+\s+(.*)", RegexOptions.IgnoreCase));
        return (r == null) ? null : r.Groups[1].Value;
      }
    }
  }
}