using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Riverbed.RIOS {

  public class WebSettings : BaseSetting, IConfigSetting {

    public SSLSettings SSL {
      get {
        var ssl = new SSLSettings();
        ssl.Settings = Settings.Where(c => ssl.GenericRegex.Match(c).Success);
        return ssl;
      }
    }

    public HttpSettings Http {
      get {
        var http = new HttpSettings();
        http.Settings = Settings.Where(c => http.GenericRegex.Match(c).Success);
        return http;
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^\s*(no )?web (.+)$", RegexOptions.IgnoreCase); }
    }

    public class SSLSettings : BaseSetting, IConfigSetting {

      public bool V2Enabled {
        get {
          var r = GetSetting(new Regex(@"^\s*web ssl protocol sslv2$", RegexOptions.IgnoreCase));
          return (r != null);
        }
      }

      public Regex GenericRegex {
        get { return new Regex(@"^\s*(no )?web ssl .*$", RegexOptions.IgnoreCase); }
      }
    }

    public class HttpSettings : BaseSetting, IConfigSetting {

      public bool Enabled {
        get {
          var r = GetSetting(new Regex(@"^\s*web http enable$", RegexOptions.IgnoreCase));
          return (r != null);
        }
      }

      public Regex GenericRegex {
        get { return new Regex(@"^\s*(no )?web http .*$", RegexOptions.IgnoreCase); }
      }
    }
  }
}