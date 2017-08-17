using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands {

  public class ShowIpInterface : BaseSetting {

    public ShowIpInterface(IEnumerable<string> settings) {
      Settings = settings;
    }

    public IEnumerable<InterfaceOutput> Interfaces {
      get {
        var list = new List<InterfaceOutput>();
        for (int i = 0; i < Settings.Count(); i++) {
          var settings = new List<string>();
          var output = new InterfaceOutput();
          list.Add(output);
          settings.Add(Settings.ElementAt(i++));
          while (i < Settings.Count() && new Regex(@"^\s", RegexOptions.IgnoreCase).Match(Settings.ElementAt(i)).Success) {
            settings.Add(Settings.ElementAt(i++));
          }
          output.Settings = settings;
        }
        return list;
      }
    }

    public class InterfaceOutput : BaseSetting {
      private static Regex InterfaceRgx = new Regex(@"^([\w\/]+) is.*(up|down).*(up|down)$", RegexOptions.IgnoreCase);
      private static Regex AddressRgx = new Regex(@"^\s+Internet address is (\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\/(\d+)$", RegexOptions.IgnoreCase);

      public string Name {
        get {
          return InterfaceRgx.Match(Settings.ElementAt(0)).Groups[1].Value;
        }
      }

      public bool Shutdown {
        get {
          return InterfaceRgx.Match(Settings.ElementAt(0)).Groups[2].Value.Equals("down", System.StringComparison.OrdinalIgnoreCase);
        }
      }

      public bool LineProtocol {
        get {
          return InterfaceRgx.Match(Settings.ElementAt(0)).Groups[3].Value.Equals("up", System.StringComparison.OrdinalIgnoreCase);
        }
      }

      public bool NetworkAddressTranslationEnabled {
        get {
          var r = GetSetting(new Regex(@"^\s+Network address translation is enabled", RegexOptions.IgnoreCase));
          return (r != null);
        }
      }

      public bool IpCefSwitchingEanbled {
        get {
          var r = GetSetting(new Regex(@"^\s+IP CEF switching is enabled", RegexOptions.IgnoreCase));
          return (r != null);
        }
      }

      public IPAddress InternetAddress {
        get {
          var r = GetSetting(AddressRgx);
          return (r == null) ? null : IPAddress.Parse(r.Groups[1].Value);
        }
      }

      public byte NetworkMask {
        get {
          var r = GetSetting(AddressRgx);
          return (r == null) ? default(byte) : byte.Parse(r.Groups[2].Value);
        }
      }
    }
  }
}