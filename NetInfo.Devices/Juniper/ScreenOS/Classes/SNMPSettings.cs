using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public class SNMPSettings : BaseSetting, IConfigSetting {

    public string Name {
      get {
        var result = GetSetting(new Regex(@"^set snmp name ""(\w+)""$", RegexOptions.IgnoreCase));
        return result != null ? result.Groups[1].Value : string.Empty;
      }
    }

    public string Community {
      get {
        var result = GetSetting(new Regex(@"^set snmp community ""(.*)"" Read-Only Trap-on", RegexOptions.IgnoreCase));
        return result != null ? result.Groups[1].Value : string.Empty;
      }
    }

    public string Location {
      get {
        var result = GetSetting(new Regex(@"^set snmp location ""(\w+)""$", RegexOptions.IgnoreCase));
        return result != null ? result.Groups[1].Value : string.Empty;
      }
    }

    public int ListenPort {
      get {
        var result = GetSetting(new Regex(@"^set snmp port listen (\d+)$", RegexOptions.IgnoreCase));
        return result != null ? int.Parse(result.Groups[1].Value) : -1;
      }
    }

    public int TrapPort {
      get {
        var result = GetSetting(new Regex(@"^set snmp port trap (\d+)$", RegexOptions.IgnoreCase));
        return result != null ? int.Parse(result.Groups[1].Value) : -1;
      }
    }

    public IEnumerable<HostConfiguration> Hosts {
      get {
        var results = GetSettings(new Regex(@"^set snmp host \""(.*)\"" (\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}) (\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})", RegexOptions.IgnoreCase));
        return results.Any() ? results.Select(c => new HostConfiguration {
          Key = c.Groups[1].Value,
          Host = IPAddress.Parse(c.Groups[2].Value),
          HostMask = IPAddress.Parse(c.Groups[3].Value),
        }) : new List<HostConfiguration>();
      }
    }

    public class HostConfiguration {

      public string Key { get; set; }

      public IPAddress Host { get; set; }

      public IPAddress HostMask { get; set; }

      public string SourceInterface { get; set; }
    }

    /// <summary>
    /// OS 4.x ONLY
    /// </summary>
    public bool Vpn {
      get {
        var result = GetSetting(new Regex(@"^set snmp vpn$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^set snmp .*$", RegexOptions.IgnoreCase); }
    }
  }
}