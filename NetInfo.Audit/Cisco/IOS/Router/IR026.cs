using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure CDP is disabled on all externally facing interfaces on outer routers
  ///
  /// CDP should be enabled on interfaces that connect directly to another
  /// Cisco device and disabled on the WAN interfaces.
  /// </summary>
  public class IR026 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly Regex rgxHostname = new Regex(@"[\w]{4}-[\w]{3}-[\w]{2}-[\d]{2}\.NMCI-ISF.com", RegexOptions.IgnoreCase);

    public IR026(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var cdpNeighbors = device.ShowCdpNeighbors.Interfaces.ToList();
      return device.ShowCdpInterface.Interfaces.Count() == cdpNeighbors.Count() && cdpNeighbors.All(c => rgxHostname.Match(c.DestinationHostname).Success);
    }
  }
}