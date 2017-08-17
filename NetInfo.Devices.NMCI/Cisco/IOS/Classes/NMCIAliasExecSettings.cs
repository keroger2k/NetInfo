using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices.NMCI.Cisco.IOS.Patterns;

namespace NetInfo.Devices.Cisco.IOS {

  public class NMCIAliasExecSettings : AliasExecSettings {
    private static readonly Regex determineTypeRegex = new Regex(NMCIIOSRegex.ALIAS_EXEC_ID, RegexOptions.IgnoreCase);

    public HardeningScript HardeningSettings {
      get {
        var hardeningInformation = Settings.FirstOrDefault(c => determineTypeRegex.Match(c).Groups[1].Value.Equals("harden"));
        return (hardeningInformation == null) ? null : new HardeningScript(hardeningInformation);
      }
    }

    public NMSScript NMSSettings {
      get {
        var hardeningInformation = Settings.FirstOrDefault(c => determineTypeRegex.Match(c).Groups[1].Value.Equals("nms"));
        return (hardeningInformation == null) ? null : new NMSScript(hardeningInformation);
      }
    }

    public class HardeningScript {
      private Match hardeningMatches;
      private static readonly Regex hardeingSettingsRegex = new Regex(@"^alias exec harden (\w+)-(\w+)-(\w+-)?(\w+)-v(\d+)_(\d+)_(\d+)$", RegexOptions.IgnoreCase);

      public HardeningScript(string setting) {
        this.hardeningMatches = hardeingSettingsRegex.Match(setting);
      }

      public string Enclave {
        get {
          return hardeningMatches.Groups[1].Value;
        }
      }

      public string Zone {
        get {
          return hardeningMatches.Groups[2].Value;
        }
      }

      public string Type {
        get {
          return hardeningMatches.Groups[4].Value;
        }
      }

      public string Version {
        get {
          return string.Format("{0}.{1}.{2}",
            hardeningMatches.Groups[5].Value,
            hardeningMatches.Groups[6].Value,
            hardeningMatches.Groups[7].Value);
        }
      }

      public int MajorVersion {
        get {
          return (string.IsNullOrEmpty(hardeningMatches.Groups[5].Value)) ? -1 : int.Parse(hardeningMatches.Groups[5].Value);
        }
      }
    }

    public class NMSScript {
      private Match nmsMatches;

      private static readonly Regex hardeingSettingsRegex =
        new Regex(@"^alias exec nms (?<noc>\w+)-(?<solutionType>\w+)-(?<snmpVersion>\w+-)?(?<deviceType>\w+)-v(?<major>\d+)_(?<minor>\d+)_(?<revision>\d+)$", RegexOptions.IgnoreCase);

      public NMSScript(string setting) {
        this.nmsMatches = hardeingSettingsRegex.Match(setting);
      }

      public string Enclave {
        get {
          return nmsMatches.Groups[1].Value;
        }
      }

      public string Zone {
        get {
          return nmsMatches.Groups[2].Value;
        }
      }

      public string Type {
        get {
          return nmsMatches.Groups[4].Value;
        }
      }

      public string Version {
        get {
          return string.Format("{0}.{1}.{2}",
            nmsMatches.Groups["major"].Value,
            nmsMatches.Groups["minor"].Value,
            nmsMatches.Groups["revision"].Value);
        }
      }

      public int MajorVersion {
        get {
          return string.IsNullOrEmpty(nmsMatches.Groups["major"].Value) ? -1 : int.Parse(nmsMatches.Groups["major"].Value);
        }
      }
    }
  }
}