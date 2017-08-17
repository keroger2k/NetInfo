using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS {

  public class ServiceSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^(no\s)?service\s(.*)$", RegexOptions.IgnoreCase); }
    }

    public bool Pad {
      get {
        return GetSetting(new Regex(@"^service pad", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool PasswordEncryption {
      get {
        return GetSetting(new Regex(@"^service password-encryption", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool UdpSmallServers {
      get {
        return GetSetting(new Regex(@"^service udp-small-servers", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool TcpSmallServers {
      get {
        return GetSetting(new Regex(@"^service tcp-small-servers", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool TcpKeepalivesIn {
      get {
        return GetSetting(new Regex(@"^service tcp-keepalives-in", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool TcpKeepalivesOut {
      get {
        return GetSetting(new Regex(@"^service tcp-keepalives-out", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool Dhcp {
      get {
        return GetSetting(new Regex(@"^service dhcp", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool CallHome {
      get {
        return GetSetting(new Regex(@"^service call-home", RegexOptions.IgnoreCase)) != null;
      }
    }
  }
}