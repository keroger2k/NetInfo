using System;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.McAfee {

  public class SensorNetworkConfig : BaseSetting, IConfigSetting {

    public IPAddress Address {
      get {
        var setting = GetSetting(new Regex(@"^IP\s+Address\s+:\s+(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})$", RegexOptions.IgnoreCase));
        if (setting != null) {
          return IPAddress.Parse(setting.Groups[1].Value);
        }
        return IPAddress.Parse("0.0.0.0");
      }
    }

    public IPAddress Netmask {
      get {
        var setting = GetSetting(new Regex(@"^Netmask\s+:\s+(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})$", RegexOptions.IgnoreCase));
        if (setting != null) {
          return IPAddress.Parse(setting.Groups[1].Value);
        }
        return IPAddress.Parse("0.0.0.0");
      }
    }

    public IPAddress DefaultGateway {
      get {
        var setting = GetSetting(new Regex(@"^Default\s+Gateway\s+:\s+(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})$", RegexOptions.IgnoreCase));
        if (setting != null) {
          return IPAddress.Parse(setting.Groups[1].Value);
        }
        return IPAddress.Parse("0.0.0.0");
      }
    }

    public bool SSHRemoteLoginsEanbled {
      get {
        var setting = GetSetting(new Regex(@"^SSH\s+Remote\s+Logins\s+:\s+(\w+)$", RegexOptions.IgnoreCase));
        return setting.Groups[1].Value.Equals("enabled", StringComparison.OrdinalIgnoreCase);
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"\[Sensor Network Config\]", RegexOptions.IgnoreCase); }
    }
  }
}