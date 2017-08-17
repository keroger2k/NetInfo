using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS {

  public class NTPSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^s?ntp\s+(.*)$", RegexOptions.IgnoreCase); }
    }

    public IEnumerable<Server> Servers {
      get {
        var servers = new List<Server>();
        servers.AddRange(Settings.Where(c => new Regex(@"s?ntp\s+server\s+(?<address>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})( key (?<key>\d+))?", RegexOptions.IgnoreCase).Match(c).Success)
          .Select(c => {
            var x = new Server();
            var m = new Regex(@"s?ntp\s+server\s+(?<address>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})( key (?<key>\d+))?", RegexOptions.IgnoreCase).Match(c);
            x.Address = IPAddress.Parse(m.Groups["address"].Value);
            if (m.Groups["key"].Success) {
              x.Key = int.Parse(m.Groups["key"].Value);
            }
            return x;
          }));
        return servers.GroupBy(c => c.Address).Select(c => c.First());
      }
    }

    public bool Authenticate {
      get {
        var r = GetSetting(new Regex(@"ntp authenticate", RegexOptions.IgnoreCase));
        return r != null;
      }
    }

    public IEnumerable<int> TrustedKeys {
      get {
        var r = GetSettings(new Regex(@"ntp trusted-key (?<number>\d+)", RegexOptions.IgnoreCase));
        return r == null ? new List<int>() : r.Select(c => int.Parse(c.Groups["number"].Value));
      }
    }

    public string SourceVlan {
      get {
        var source = Settings.FirstOrDefault(c => new Regex(@"^ntp source (?<sourceInterface>.*)$", RegexOptions.IgnoreCase).Match(c).Success);
        return (source == null) ? string.Empty : new Regex(@"^ntp source (?<sourceInterface>.*)$", RegexOptions.IgnoreCase).Match(source).Groups["sourceInterface"].Value;
      }
    }

    public IEnumerable<Key> Keys {
      get {
        var r = GetSettings(new Regex(@"ntp authentication-key (?<number>\d+) md5 (?<hash>\w+) 7", RegexOptions.IgnoreCase));
        if (r != null) {
          return r.Select(c => new Key {
            Number = int.Parse(c.Groups["number"].Value),
            Hash = c.Groups["hash"].Value
          });
        }
        return new List<Key>();
      }
    }

    public class Server {

      public IPAddress Address { get; set; }

      public int? Key { get; set; }
    }

    public class Key {

      public int Number { get; set; }

      public string Hash { get; set; }
    }
  }
}