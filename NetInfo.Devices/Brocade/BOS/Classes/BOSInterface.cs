using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS.Classes {

  public class BOSInterface {
    public static readonly Regex INTERFACE_REGEX = new Regex(@"^interface (ethernet|ve|management)\s+(.*)$");
    private static readonly Regex INTERFACE_DESC_REGEX = new Regex(@"^\s*port-name\s+(.+)$", RegexOptions.IgnoreCase);
    private static readonly Regex IOS_INTERFACE_ADDRESS = new Regex(@"^\s+ip address\s(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\s(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})$", RegexOptions.IgnoreCase);
    private static readonly Dictionary<string, bool> isInterfacePhysical;
    private readonly IEnumerable<string> commands;

    public BOSInterface(IEnumerable<string> commands) {
      if (commands == null || commands.Count() == 0) {
        throw new ArgumentNullException();
      }

      if (!INTERFACE_REGEX.Match(commands.ElementAt(0)).Success) {
        throw new NotImplementedException("Unknown Interface");
      }

      this.commands = commands;
    }

    static BOSInterface() {
      isInterfacePhysical = new Dictionary<string, bool>();
      isInterfacePhysical["management"] = true;
      isInterfacePhysical["ethernet"] = true;
      isInterfacePhysical["ve"] = false;
    }

    public InterfaceAddress Address {
      get {
        var match = commands.FirstOrDefault(c => IOS_INTERFACE_ADDRESS.Match(c).Success);
        return (match == null) ? null : new InterfaceAddress {
          NetworkAddress = IPAddress.Parse(IOS_INTERFACE_ADDRESS.Match(match).Groups[1].Value),
          NetworkMask = IPAddress.Parse(IOS_INTERFACE_ADDRESS.Match(match).Groups[2].Value)
        };
      }
    }

    public string Type {
      get {
        return INTERFACE_REGEX.Match(commands.ElementAt(0)).Groups[1].Value;
      }
    }

    public string Number {
      get {
        return INTERFACE_REGEX.Match(commands.ElementAt(0)).Groups[2].Value;
      }
    }

    public string PortName {
      get {
        var match = commands.FirstOrDefault(c => INTERFACE_DESC_REGEX.Match(c).Success);
        return (match == null) ? string.Empty : INTERFACE_DESC_REGEX.Match(match).Groups[1].Value;
      }
    }

    public bool Enabled {
      get {
        return commands.FirstOrDefault(c => new Regex(@"^\s*disable", RegexOptions.IgnoreCase).Match(c).Success) == null;
      }
    }

    public bool Physical {
      get {
        return isInterfacePhysical[this.Type];
      }
    }

    public Dot1xSettings Dot1x {
      get {
        var dot1xSettings = new Dot1xSettings();
        var settings = commands.Where(c => dot1xSettings.GenericRegex.Match(c).Success);
        dot1xSettings.Settings = settings;
        return dot1xSettings;
      }
    }

    public class Dot1xSettings : BaseSetting, IConfigSetting {

      public Regex GenericRegex {
        get { return new Regex(@"^\s+(no dot1x|dot1x|mac-authentication).*$", RegexOptions.IgnoreCase); }
      }

      public bool PortControlAuto {
        get {
          var r = GetSetting(new Regex(@"^\s+dot1x port-control auto", RegexOptions.IgnoreCase));
          return (r != null);
        }
      }

      public bool MACAuthentication {
        get {
          var r = GetSetting(new Regex(@"^\s+mac-authentication enable", RegexOptions.IgnoreCase));
          return (r != null);
        }
      }
    }

    public class InterfaceAddress {

      public IPAddress NetworkAddress { get; set; }

      public IPAddress NetworkMask { get; set; }
    }
  }
}