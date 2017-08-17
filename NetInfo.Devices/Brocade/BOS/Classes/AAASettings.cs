using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS {

  public class AAASettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^(enable aaa|aaa).*$", RegexOptions.IgnoreCase); }
    }

    public bool ConsoleEnabled {
      get {
        return Settings.Any(c => new Regex(@"enable aaa console").Match(c).Success);
      }
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
          var r = GetSetting(new Regex(@"^aaa authentication login default tacacs\+ enable$", RegexOptions.IgnoreCase));
          return (r != null);
        }
      }

      public bool LoginPrivilegeMode {
        get {
          var r = GetSetting(new Regex(@"^aaa authentication login privilege-mode$", RegexOptions.IgnoreCase));
          return (r != null);
        }
      }

      public bool EnableGroupTacacsEnable {
        get {
          var r = GetSetting(new Regex(@"^aaa authentication enable default tacacs\+ enable$", RegexOptions.IgnoreCase));
          return (r != null);
        }
      }

      public bool Dot1xDefaultRadisuEnable {
        get {
          var r = GetSetting(new Regex(@"^aaa authentication dot1x default radius$", RegexOptions.IgnoreCase));
          return (r != null);
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