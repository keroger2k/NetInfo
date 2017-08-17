using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure two NTP servers are configured
  ///
  /// Exception:
  ///   All inside devices at non-redundant sites will have only one NTP server configured.
  /// </summary>
  public class IS007 : ISTIGItem {

    public IDevice Device { get; private set; }

    private Dictionary<string, int> _siteTypeLookups;
    private readonly Regex deviceTypeRegex = new Regex(@".*U00-(IR|OR)-(01|02)", RegexOptions.IgnoreCase);

    public IS007(INMCIIOSDevice device, Dictionary<string, int> siteTypeLookups) {
      this.Device = device;
      this._siteTypeLookups = siteTypeLookups;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      int count = 0;
      bool siteFound = _siteTypeLookups.TryGetValue(device.Hostname.Site, out count);
      return siteFound ?
              deviceTypeRegex.Match(device.Hostname.Name).Success ?
                device.NetworkTimeProtocol.Servers.Count() >= 2 :
                device.NetworkTimeProtocol.Servers.Count() >= count :
              false;
    }
  }
}