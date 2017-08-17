using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public class PKISettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^set pki .*$", RegexOptions.IgnoreCase); }
    }

    public string SourceInterface {
      get {
        var result = GetSetting(new Regex(@"^set pki src-interface ([\w\/]+)$", RegexOptions.IgnoreCase));
        return result != null ? result.Groups[1].Value : string.Empty;
      }
    }

    public bool RevocationCheckBestEffortEanbled {
      get {
        var result = GetSetting(new Regex(@"^set pki authority default cert-status revocation-check (crl )?best-effort$", RegexOptions.IgnoreCase));
        return result != null;
      }
    }

    public x509Settings x509 {
      get {
        var set = new x509Settings();
        set.Settings = Settings.Where(c => set.GenericRegex.Match(c).Success);
        return set;
      }
    }

    public class x509Settings : BaseSetting, IConfigSetting {

      public Regex GenericRegex {
        get { return new Regex(@"^set pki x509 .*$", RegexOptions.IgnoreCase); }
      }

      public bool RawCn {
        get {
          var result = GetSetting(new Regex(@"^set pki x509 raw-cn (\w+)$", RegexOptions.IgnoreCase));
          return result != null ?
            result.Groups[1].Value.Equals("enable", System.StringComparison.OrdinalIgnoreCase) : false;
        }
      }

      public string CertPath {
        get {
          var result = GetSetting(new Regex(@"^set pki x509 default cert-path (\w+)$", RegexOptions.IgnoreCase));
          return result != null ? result.Groups[1].Value : string.Empty;
        }
      }

      public DNSettings DN {
        get {
          var set = new DNSettings();
          set.Settings = Settings.Where(c => set.GenericRegex.Match(c).Success);
          return set;
        }
      }

      public class DNSettings : BaseSetting, IConfigSetting {

        public Regex GenericRegex {
          get { return new Regex(@"^set pki x509 dn .*$", RegexOptions.IgnoreCase); }
        }

        public string CountryName {
          get {
            var result = GetSetting(new Regex(@"^set pki x509 dn country-name ""(\w+)""$", RegexOptions.IgnoreCase));
            return result != null ? result.Groups[1].Value : string.Empty;
          }
        }

        public string Name {
          get {
            var result = GetSetting(new Regex(@"^set pki x509 dn name ""([\w\.]+)""$", RegexOptions.IgnoreCase));
            return result != null ? result.Groups[1].Value : string.Empty;
          }
        }

        public string OrgName {
          get {
            var result = GetSetting(new Regex(@"^set pki x509 dn org-name ""([\w\.\s]+)""$", RegexOptions.IgnoreCase));
            return result != null ? result.Groups[1].Value : string.Empty;
          }
        }

        public string OrgUnitName {
          get {
            var result = GetSetting(new Regex(@"^set pki x509 dn org-unit-name ""([\w\,]+)""$", RegexOptions.IgnoreCase));
            return result != null ? result.Groups[1].Value : string.Empty;
          }
        }
      }
    }
  }
}