using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure at least 2 Radius servers are configured for 802.1x use.
  /// </summary>
  public class IR171 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR171(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((IIOSDevice)Device).RadiusServers.Count() > 1;
    }
  }
}