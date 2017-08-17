using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure all disabled ports are assigned to VLAN2 (see comment)
  ///
  /// "All disabled switchports must be assigned to Vlan2, except at PNDL, which uses Vlan998.
  /// Exceptions: routed only ports which will not accept switchport commands."
  /// </summary>
  public class IR067 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR067(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var interfaces = ((INMCIIOSDevice)Device).Interfaces;
      return !interfaces.Any(c => c.Physical && c.Shutdown && c.Vlan != 2);
    }
  }
}