using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Infrastructure.Implementations.Cisco.IOS;

namespace NetInfo.Devices.NMCI.Cisco.IOS {

  public class NMCIIOSDevice : NMCIIOSBase, INMCIIOSDevice {

    public NMCIIOSDevice(IAssetBlob AssetBlob)
      : base(AssetBlob) {
    }

    private Hostname _hostname;

    new public Hostname Hostname {
      get {
        if (_hostname == null) {
          _hostname = base.Hostname == null ? null : new Hostname(base.Hostname);
        }
        return _hostname;
      }
    }

    new public NMCISNMPSettings SNMPSettings {
      get {
        var r = new NMCISNMPSettings();
        r.Settings = config.Where(c => NMCISNMPSettings.SNMPServerRegex.Match(c).Success);
        return r;
      }
    }

    new public NMCIAliasExecSettings AliasExecSettings {
      get {
        var r = new NMCIAliasExecSettings();
        r.Settings = config.Where(c => new Regex(@"^alias exec ([\w-]+) (.*)$", RegexOptions.IgnoreCase).Match(c).Success);
        return r;
      }
    }

    public IEnumerable<string> TestScriptHeader {
      get {
        var bannerLines = new List<string>();
        var bannerRegex = new Regex(@"^[\w-]+#!$", RegexOptions.IgnoreCase);
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