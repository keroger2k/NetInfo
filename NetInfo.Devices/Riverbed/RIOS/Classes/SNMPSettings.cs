using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Riverbed.RIOS {

  public class SNMPSettings : BaseSetting, IConfigSetting {

    public IEnumerable<Server> Servers {
      get {
        var r = GetSettings(new Regex(@"^\s*snmp-server host (\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}) traps version (\d) (.*)$", RegexOptions.IgnoreCase));
        return r.Select(c => new Server {
          Address = IPAddress.Parse(c.Groups[1].Value),
          Password = c.Groups[3].Value,
          Version = int.Parse(c.Groups[2].Value)
        }).GroupBy(c => c.Address).Select(c => c.First());
      }
    }

    public string Location {
      get {
        var r = GetSetting(new Regex(@"^\s*snmp-server location ""(.*)""$", RegexOptions.IgnoreCase));
        return (r == null) ? null : r.Groups[1].Value;
      }
    }

    public string Community {
      get {
        var r = GetSetting(new Regex(@"^\s*snmp-server community ""(.*)""$", RegexOptions.IgnoreCase));
        return (r == null) ? null : r.Groups[1].Value;
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^\s*(no snmp-server|snmp-server) .*$", RegexOptions.IgnoreCase); }
    }

    public class Server {

      public IPAddress Address { get; set; }

      public int Version { get; set; }

      public string Password { get; set; }
    }
  }
}