using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Riverbed.RIOS {

  public class AAASettings : BaseSetting, IConfigSetting {

    public AuthenticationSettings Authentication {
      get {
        var aaa = new AuthenticationSettings();
        aaa.Settings = Settings.Where(c => aaa.GenericRegex.Match(c).Success);
        return aaa;
      }
    }

    public AuthorizationSettings Authorization {
      get {
        var aaa = new AuthorizationSettings();
        aaa.Settings = Settings.Where(c => aaa.GenericRegex.Match(c).Success);
        return aaa;
      }
    }

    public AccountingSettings Accounting {
      get {
        var aaa = new AccountingSettings();
        aaa.Settings = Settings.Where(c => aaa.GenericRegex.Match(c).Success);
        return aaa;
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^\s*aaa .*$", RegexOptions.IgnoreCase); }
    }

    public class AuthenticationSettings : BaseSetting, IConfigSetting {

      public bool LoginDefaultTacacsLocal {
        get {
          var r = GetSetting(new Regex(@"^\s*aaa authentication login default tacacs\+ local", RegexOptions.IgnoreCase));
          return (r != null);
        }
      }

      public bool LoginDefaultTacacsConsole {
        get {
          var r = GetSetting(new Regex(@"^\s*aaa authentication console-login default tacacs\+ local", RegexOptions.IgnoreCase));
          return (r != null);
        }
      }

      public Regex GenericRegex {
        get { return new Regex(@"^\s*aaa authentication .*$", RegexOptions.IgnoreCase); }
      }
    }

    public class AuthorizationSettings : BaseSetting, IConfigSetting {

      public Regex GenericRegex {
        get { return new Regex(@"^\s*aaa authorization .*$", RegexOptions.IgnoreCase); }
      }
    }

    public class AccountingSettings : BaseSetting, IConfigSetting {

      public Regex GenericRegex {
        get { return new Regex(@"^\s*aaa accounting .*$", RegexOptions.IgnoreCase); }
      }
    }
  }
}