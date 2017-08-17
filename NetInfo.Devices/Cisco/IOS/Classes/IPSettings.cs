using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS {

  public class IPSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^(ip|no ip)\s(.*)$", RegexOptions.IgnoreCase); }
    }

    public bool SubnetZero {
      get {
        return GetSetting(new Regex(@"^ip subnet-zero", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool Finger {
      get {
        return GetSetting(new Regex(@"^ip finger", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool Dhcp {
      get {
        return GetSetting(new Regex(@"^ip dhcp", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool HttpServer {
      get {
        return GetSetting(new Regex(@"^ip http-server", RegexOptions.IgnoreCase)) != null ||
          GetSetting(new Regex(@"^ip http server", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool BootPServer {
      get {
        return GetSetting(new Regex(@"^ip bootp server", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool SecureHttpServer {
      get {
        return GetSetting(new Regex(@"^ip http secure-server", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool Unreachables {
      get {
        return GetSetting(new Regex(@"^ip unreachables", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool Identd {
      get {
        return GetSetting(new Regex(@"^ip identd", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool SourceRoute {
      get {
        return GetSetting(new Regex(@"^ip source-route", RegexOptions.IgnoreCase)) != null ||
          GetSetting(new Regex(@"^ip source route", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool Routing {
      get {
        return GetSetting(new Regex(@"^ip routing", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool GratuitousArps {
      get {
        return GetSetting(new Regex(@"^ip gratuitous-arps", RegexOptions.IgnoreCase)) != null;
      }
    }

    public bool DomainLookup {
      get {
        return GetSetting(new Regex(@"^ip domain-lookup", RegexOptions.IgnoreCase)) != null ||
               GetSetting(new Regex(@"^ip domain lookup", RegexOptions.IgnoreCase)) != null;
      }
    }

    public string TacacsSourceInterface {
      get {
        var r = GetSetting(new Regex(@"^ip tacacs source-interface (?<interface>.*)$", RegexOptions.IgnoreCase));
        return (r == null) ? string.Empty : r.Groups["interface"].Value;
      }
    }

    public string RadiusSourceInterface {
      get {
        var r = GetSetting(new Regex(@"^ip radius source-interface\s(?<interface>.*)$", RegexOptions.IgnoreCase));
        return (r == null) ? string.Empty : r.Groups["interface"].Value;
      }
    }

    public SSHSettings SSH {
      get {
        var set = new SSHSettings();
        set.Settings = Settings.Where(c => set.GenericRegex.Match(c).Success);
        return set;
      }
    }

    public TCPSettings TCP {
      get {
        var set = new TCPSettings();
        set.Settings = Settings.Where(c => set.GenericRegex.Match(c).Success);
        return set;
      }
    }

    public RCMDSettings RCMD {
      get {
        var set = new RCMDSettings();
        set.Settings = Settings.Where(c => set.GenericRegex.Match(c).Success);
        return set;
      }
    }

    public class SSHSettings : BaseSetting, IConfigSetting {

      public Regex GenericRegex {
        get { return new Regex(@"^ip ssh .*", RegexOptions.IgnoreCase); }
      }

      public int Timeout {
        get {
          var r = GetSetting(new Regex(@"^ip ssh time-out (\d+)$", RegexOptions.IgnoreCase));
          return (r == null) ? -1 : int.Parse(r.Groups[1].Value);
        }
      }

      public int AuthenticationRetries {
        get {
          var r = GetSetting(new Regex(@"^ip ssh authentication-retries (\d+)$", RegexOptions.IgnoreCase));
          return (r == null) ? -1 : int.Parse(r.Groups[1].Value);
        }
      }

      public int Version {
        get {
          var r = GetSetting(new Regex(@"^ip ssh version (\d+)$", RegexOptions.IgnoreCase));
          return (r == null) ? -1 : int.Parse(r.Groups[1].Value);
        }
      }
    }

    public class RCMDSettings : BaseSetting, IConfigSetting {

      public bool IsRCPEnabled {
        get {
          var result = GetSetting(new Regex(@"ip rcmd rcp-enable", RegexOptions.IgnoreCase));
          return result != null;
        }
      }

      public bool IsRSHEnabled {
        get {
          var result = GetSetting(new Regex(@"ip rcmd rsh-enable", RegexOptions.IgnoreCase));
          return result != null;
        }
      }

      public Regex GenericRegex {
        get { return new Regex(@"^ip rcmd (.*)$", RegexOptions.IgnoreCase); }
      }
    }

    public class TCPSettings : BaseSetting, IConfigSetting {

      public int InterceptionConnectionTimeout {
        get {
          var r = GetSetting(new Regex(@"ip tcp intercept connection-timeout (\d+)", RegexOptions.IgnoreCase));
          return (r == null) ? default(int) : int.Parse(r.Groups[1].Value);
        }
      }

      public int SynWaitTime {
        get {
          var r = GetSetting(new Regex(@"ip tcp synwait-time (\d+)", RegexOptions.IgnoreCase));
          return (r == null) ? default(int) : int.Parse(r.Groups[1].Value);
        }
      }

      public int InterceptionWatchTimeout {
        get {
          var r = GetSetting(new Regex(@"ip tcp intercept watch-timeout (\d+)", RegexOptions.IgnoreCase));
          return (r == null) ? default(int) : int.Parse(r.Groups[1].Value);
        }
      }

      public Regex GenericRegex {
        get { return new Regex(@"^ip tcp (.*)$", RegexOptions.IgnoreCase); }
      }
    }
  }
}