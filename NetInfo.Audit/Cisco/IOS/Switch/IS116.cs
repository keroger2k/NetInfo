using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure at least 2 Radius servers are configured for 802.1x use.
  /// </summary>
  public class IS116 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS116(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIIOSDevice)Device).RadiusServers.Count() > 1;
    }
  }
}