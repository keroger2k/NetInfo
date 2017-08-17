using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public class FlowSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^(set|unset) flow .*", RegexOptions.IgnoreCase); }
    }

    public bool PathMTUEnabled {
      get {
        var result = GetSetting(new Regex(@"^set flow path-mtu$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public bool NoTCPSequenceCheckEnabled {
      get {
        var result = GetSetting(new Regex(@"^set flow no-tcp-seq-check$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public bool TCPSynCheckEnabled {
      get {
        var result = GetSetting(new Regex(@"^set flow tcp-syn-check$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public bool PreferReverseRouteEnabled {
      get {
        var result = GetSetting(new Regex(@"^set flow route tunnel prefer-reverse-route$|^set flow reverse-route tunnel prefer$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }
  }
}