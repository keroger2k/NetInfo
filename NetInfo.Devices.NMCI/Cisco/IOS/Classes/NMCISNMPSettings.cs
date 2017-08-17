using System.Text.RegularExpressions;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.Cisco.IOS.Patterns;
using NetInfo.Devices.NMCI.Cisco.IOS.Patterns;

namespace NetInfo.Devices.NMCI.Cisco.IOS {

  public class NMCISNMPSettings : SNMPSettings {
    private static readonly Regex rgxLocation = new Regex(NMCIIOSRegex.SNMP_LOCATION, RegexOptions.IgnoreCase);
    public static readonly Regex SNMPServerRegex = new Regex(IOSRegex.SNMP_SERVER_GENERIC, RegexOptions.IgnoreCase);

    new public SNMPLocation Location {
      get {
        SNMPLocation location = null;
        var match = rgxLocation.Match(base.Location ?? "");
        if (match.Groups.Count > 1) {
          location = new SNMPLocation {
            Building = match.Groups[1].Value,
            Floor = match.Groups[2].Value,
            Room = match.Groups[3].Value,
            Rack = match.Groups[4].Value,
          };
        }
        return location;
      }
    }

    public class SNMPLocation {

      public string Building { get; set; }

      public string Floor { get; set; }

      public string Room { get; set; }

      public string Rack { get; set; }
    }
  }
}