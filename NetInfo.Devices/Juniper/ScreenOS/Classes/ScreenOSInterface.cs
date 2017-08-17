using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public class ScreenOSInterface {
    public static readonly Regex INTERFACE_REGEX = new Regex(@"^Interface (ethernet|bgroup|serial)(.*):$", RegexOptions.IgnoreCase);
    private static readonly Dictionary<string, bool> isInterfacePhysical;
    private readonly IEnumerable<string> commands;

    public enum Link {
      up,
      down,
      unknown
    }

    public ScreenOSInterface(IEnumerable<string> commands) {
      if (commands == null || commands.Count() == 0) {
        throw new ArgumentNullException();
      }

      if (!INTERFACE_REGEX.Match(commands.ElementAt(0)).Success) {
        throw new NotImplementedException("Unknown Interface");
      }

      this.commands = commands;
    }

    static ScreenOSInterface() {
      isInterfacePhysical = new Dictionary<string, bool>();
      isInterfacePhysical["serial"] = true;
      isInterfacePhysical["ethernet"] = true;
      isInterfacePhysical["bgroup"] = true;
    }

    public string Type {
      get {
        return INTERFACE_REGEX.Match(commands.ElementAt(0)).Groups[1].Value;
      }
    }

    public bool Physical {
      get {
        return isInterfacePhysical[this.Type];
      }
    }

    public Link Status {
      get {
        var linkRgx = new Regex(@"link (up|down), phy-link (up|down)\/?(full-duplex)?", RegexOptions.IgnoreCase);
        var match = commands.FirstOrDefault(c => linkRgx.Match(c).Success);
        return (match == null) ? Link.unknown : (Link)Enum.Parse(typeof(Link), linkRgx.Match(match).Groups[1].Value);
      }
    }

    public class InterfaceAddress {

      public IPAddress NetworkAddress { get; set; }

      public byte MaskBytes { get; set; }
    }

    public InterfaceAddress Address {
      get {
        var ifaceRegex = new Regex(@"\*?ip (\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\/(\d+)\s+mac ([0-9a-f]{4}\.[0-9a-f]{4}\.[0-9a-f]{4})", RegexOptions.IgnoreCase);
        var match = commands.FirstOrDefault(c => ifaceRegex.Match(c).Success);
        return (match == null) ? null : new InterfaceAddress {
          NetworkAddress = IPAddress.Parse(ifaceRegex.Match(match).Groups[1].Value),
          MaskBytes = byte.Parse(ifaceRegex.Match(match).Groups[2].Value),
        };
      }
    }

    public class HostConfiguration {

      public string Key { get; set; }

      public IPAddress Host { get; set; }

      public IPAddress HostMask { get; set; }

      public string SourceInterface { get; set; }
    }
  }
}