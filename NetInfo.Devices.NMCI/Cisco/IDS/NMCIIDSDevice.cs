using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices.NMCI.Infrastructure.Implementations.Cisco.IOS;

namespace NetInfo.Devices.NMCI.Cisco.IOS.IDS {

  public class NMCIIDSDevice : NMCIIDSBase, INMCIIDSDevice {

    public NMCIIDSDevice(IAssetBlob AssetBlob) :
      base(AssetBlob) {
    }

    public IEnumerable<string> TestScriptHeader {
      get {
        var bannerLines = new List<string>();
        var bannerRegex = new Regex(@"terminal\s+length\s+0", RegexOptions.IgnoreCase);
        for (int i = 0; i < configLength; i++) {
          if (string.IsNullOrEmpty(config.ElementAt(i))) {
            continue;
          }

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