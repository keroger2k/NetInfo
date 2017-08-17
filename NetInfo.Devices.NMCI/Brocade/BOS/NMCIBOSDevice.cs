using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices.NMCI.Infrastructure.Implementations.Brocade.BOS;

namespace NetInfo.Devices.NMCI.Brocade.BOS {

  public class NMCIBOSDevice : NMCIBOSBase, INMCIBOSDevice {

    public NMCIBOSDevice(IAssetBlob AssetBlob)
      : base(AssetBlob) {
    }

    new public Hostname Hostname {
      get {
        return base.Hostname == null ? null : new Hostname(base.Hostname);
      }
    }

    public IEnumerable<string> TestScriptHeader {
      get {
        var bannerLines = new List<string>();
        var bannerRegex = new Regex(@"^[\w@-]+#$", RegexOptions.IgnoreCase);
        for (int i = 0; i < configLength; i++) {
          if (string.IsNullOrEmpty(config.ElementAt(i))) { continue; }
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