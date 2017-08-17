using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Remove port descriptions for VPN devices that aren't connected
  ///
  /// Remove the Navy descriptions at USMC only sites and conversely,
  /// remove USMC descriptions at Navy only sites.
  /// </summary>
  public class IR079 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly Regex rgxVpn = new Regex(@"NETSCREEN", RegexOptions.IgnoreCase);

    public IR079(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var shutdownInterfaces = device.Interfaces.Where(c => c.Shutdown);
      return shutdownInterfaces.All(c => !rgxVpn.Match(c.Description).Success);
    }
  }
}