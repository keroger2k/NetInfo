using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public class InterfaceSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^(set|unset) interface .*", RegexOptions.IgnoreCase); }
    }

    public bool IsVlan1IpSet {
      get {
        var result = GetSetting(new Regex(@"^unset interface vlan1 ip$", RegexOptions.IgnoreCase));
        return result == null;
      }
    }

    public bool IsVlan1BypassNonIp {
      get {
        var result = GetSetting(new Regex(@"^unset interface vlan1 bypass-non-ip$", RegexOptions.IgnoreCase));
        return result == null;
      }
    }

    public bool IsVlan1BypassOthersIpsec {
      get {
        var result = GetSetting(new Regex(@"^unset interface vlan1 bypass-others-ipsec$", RegexOptions.IgnoreCase));
        return result == null;
      }
    }
  }
}