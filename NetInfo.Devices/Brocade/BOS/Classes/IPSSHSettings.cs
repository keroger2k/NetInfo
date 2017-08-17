using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS {

  public class IPSSHSettings : BaseSetting, IConfigSetting {

    public int IdleTime {
      get {
        var r = GetSetting(new Regex(@"^ip\s+ssh\s+idle-time\s+(\d+)$", RegexOptions.IgnoreCase));
        return (r == null) ? default(int) : int.Parse(r.Groups[1].Value);
      }
    }

    public int AccessGroup {
      get {
        var r = GetSetting(new Regex(@"^ssh access-group (\d+)$", RegexOptions.IgnoreCase));
        return (r == null) ? default(int) : int.Parse(r.Groups[1].Value);
      }
    }

    public int Timeout {
      get {
        var r = GetSetting(new Regex(@"^ip\s+ssh\s+timeout\s+(\d+)$", RegexOptions.IgnoreCase));
        return (r == null) ? default(int) : int.Parse(r.Groups[1].Value);
      }
    }

    public int AuthenticationRetries {
      get {
        var r = GetSetting(new Regex(@"^ip\s+ssh\s+authentication-retries\s+(\d+)$", RegexOptions.IgnoreCase));
        return (r == null) ? 3 : int.Parse(r.Groups[1].Value);
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^(ssh|ip)\s+.*", RegexOptions.IgnoreCase); }
    }
  }
}