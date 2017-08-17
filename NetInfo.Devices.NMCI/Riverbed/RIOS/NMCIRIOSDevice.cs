using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices.NMCI.Infrastructure.Implementations.Riverbed.RIOS;

namespace NetInfo.Devices.NMCI.Riverbed.RIOS {

  public class NMCIRIOSDevice : NMCIRIOSBase, INMCIRIOSDevice {

    public NMCIRIOSDevice(IAssetBlob AssetBlob)
      : base(AssetBlob) {
    }

    new public Hostname Hostname {
      get {
        return base.Hostname == null ? null : new Hostname(base.Hostname);
      }
    }

    new public NMCISNMPSettings SNMP {
      get {
        return new NMCISNMPSettings {
          Settings = config.Where(c =>
            new Regex(@"^\s*(no snmp-server|snmp-server) .*$", RegexOptions.IgnoreCase).Match(c).Success)
        };
      }
    }

    public IEnumerable<string> TestScriptHeader {
      get {
        var bannerLines = new List<string>();
        var bannerRegex = new Regex(@"^[\w-]+\s+#\s+#$", RegexOptions.IgnoreCase);
        for (int i = 0; i < configLength; i++) {
          if (!bannerRegex.Match(config.ElementAt(i)).Success) {
            bannerLines.Add(config.ElementAt(i));
          } else {
            break;
          }
        }
        return bannerLines;
      }
    }
  }
}