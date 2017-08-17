using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public class AlgSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^(set|unset) alg .*", RegexOptions.IgnoreCase); }
    }

    public bool Sip {
      get {
        var result = GetSetting(new Regex(@"^unset alg sip enable$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public bool Mgcp {
      get {
        var result = GetSetting(new Regex(@"^unset alg mgcp enable$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public bool Sccp {
      get {
        var result = GetSetting(new Regex(@"^unset alg sccp enable$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public bool Sunrpc {
      get {
        var result = GetSetting(new Regex(@"^unset alg sunrpc enable$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public bool Msrpc {
      get {
        var result = GetSetting(new Regex(@"^unset alg msrpc enable$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public bool Sql {
      get {
        var result = GetSetting(new Regex(@"^unset alg sql enable$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public bool Rtsp {
      get {
        var result = GetSetting(new Regex(@"^unset alg rtsp enable$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public bool H323 {
      get {
        var result = GetSetting(new Regex(@"^unset alg h323 enable$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }
  }
}