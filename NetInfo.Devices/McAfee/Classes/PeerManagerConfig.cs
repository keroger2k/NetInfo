using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.McAfee {

  public class PeerManagerConfig : BaseSetting, IConfigSetting {

    public IPAddress Address {
      get {
        var setting = GetSetting(new Regex(@"^Manager\s+IP\s+addr\s+:\s+(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\s+\(primary intf\)$", RegexOptions.IgnoreCase));
        if (setting != null) {
          return IPAddress.Parse(setting.Groups[1].Value);
        }
        return IPAddress.Parse("0.0.0.0");
      }
    }

    public int InstallTcpPort {
      get {
        var setting = GetSetting(new Regex(@"^Install\s+TCP\s+Port\s+:\s+(\d+)$", RegexOptions.IgnoreCase));
        if (setting != null) {
          return int.Parse(setting.Groups[1].Value);
        }
        return default(int);
      }
    }

    public int AlertTcpPort {
      get {
        var setting = GetSetting(new Regex(@"^Alert\s+TCP\s+Port\s+:\s+(\d+)$", RegexOptions.IgnoreCase));
        if (setting != null) {
          return int.Parse(setting.Groups[1].Value);
        }
        return default(int);
      }
    }

    public int LoggingTcpPort {
      get {
        var setting = GetSetting(new Regex(@"^Logging\s+TCP\s+Port\s+:\s+(\d+)$", RegexOptions.IgnoreCase));
        if (setting != null) {
          return int.Parse(setting.Groups[1].Value);
        }
        return default(int);
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"\[Peer Manager Config\]", RegexOptions.IgnoreCase); }
    }
  }
}