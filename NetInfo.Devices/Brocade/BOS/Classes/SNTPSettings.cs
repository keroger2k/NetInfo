using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS {

  public class SNTPSettings : BaseSetting, IConfigSetting {
    private readonly Regex rgxServer = new Regex(@"^sntp server (?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}) \d+( authentication-key \d+ \d+ (?<key>.*))?$", RegexOptions.IgnoreCase);
    private readonly Regex rgxPoll = new Regex(@"^sntp poll-interval (\d+)$", RegexOptions.IgnoreCase);

    public IEnumerable<SNTPAddress> Servers {
      get {
        var results = Settings.Where(c => rgxServer.Match(c).Success);
        var list = new List<SNTPAddress>();
        foreach (var line in results) {
          var ms = rgxServer.Match(line);
          list.Add(new SNTPAddress {
            Address = IPAddress.Parse(ms.Groups["ip"].Value),
            Key = ms.Groups["key"] == null ? null : ms.Groups["key"].Value
          });
        }
        return list;
      }
    }

    public int PollInterval {
      get {
        var result = Settings.SingleOrDefault(c => rgxPoll.Match(c).Success);
        if (result != null) {
          return int.Parse(rgxPoll.Match(result).Groups[1].Value);
        }
        return default(int);
      }
    }

    public class SNTPAddress {

      public IPAddress Address { get; set; }

      public string Key { get; set; }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^sntp .*$", RegexOptions.IgnoreCase); }
    }
  }
}