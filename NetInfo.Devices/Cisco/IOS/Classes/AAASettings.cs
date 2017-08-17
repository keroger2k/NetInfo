using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS {

  public class AAASettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^aaa.*$", RegexOptions.IgnoreCase); }
    }

    public AuthenticationSettings Authentication {
      get {
        var set = new AuthenticationSettings();
        set.Settings = Settings.Where(c => set.GenericRegex.Match(c).Success);
        return set;
      }
    }

    public AuthorizationSettings Authorization {
      get {
        var set = new AuthorizationSettings();
        set.Settings = Settings.Where(c => set.GenericRegex.Match(c).Success);
        return set;
      }
    }

    public AccountingSettings Accounting {
      get {
        var set = new AccountingSettings();
        set.Settings = Settings.Where(c => set.GenericRegex.Match(c).Success);
        return set;
      }
    }

    public class AuthenticationSettings : BaseSetting, IConfigSetting {

      public Regex GenericRegex {
        get { return new Regex(@"^aaa authentication.*$", RegexOptions.IgnoreCase); }
      }

      public bool LoginGroupTacacsEnable {
        get {
          var r = GetSetting(new Regex(@"^aaa authentication login default group tacacs\+ enable$", RegexOptions.IgnoreCase));
          return (r != null);
        }
      }

      public bool EnableGroupTacacsEnable {
        get {
          var r = GetSetting(new Regex(@"^aaa authentication enable default group tacacs\+ enable$", RegexOptions.IgnoreCase));
          return (r != null);
        }
      }

      public Dot1xSettings Dot1x {
        get {
          var set = new Dot1xSettings();
          set.Settings = Settings.Where(c => set.GenericRegex.Match(c).Success);
          return set;
        }
      }

      public class Dot1xSettings : BaseSetting, IConfigSetting {

        public string DefaultGroup {
          get {
            var r = GetSetting(new Regex(@"^aaa authentication dot1x default group (\w+)$", RegexOptions.IgnoreCase));
            return (r == null) ? string.Empty : r.Groups[1].Value;
          }
        }

        public Regex GenericRegex {
          get { return new Regex(@"^aaa authentication dot1x.*$", RegexOptions.IgnoreCase); }
        }
      }
    }

    public class AuthorizationSettings : BaseSetting, IConfigSetting {

      public Regex GenericRegex {
        get { return new Regex(@"^aaa authorization.*$", RegexOptions.IgnoreCase); }
      }
    }

    public class AccountingSettings : BaseSetting, IConfigSetting {

      public Regex GenericRegex {
        get { return new Regex(@"^aaa accounting.*$", RegexOptions.IgnoreCase); }
      }
    }
  }
}