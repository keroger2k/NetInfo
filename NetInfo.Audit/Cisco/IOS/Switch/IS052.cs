using System;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure no IP address is configured on VLAN1 and it is shutdown
  /// </summary>
  public class IS052 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS052(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIIOSDevice)Device);
      return device.Interfaces
        .Where(c => c.Type.Equals("Vlan", StringComparison.InvariantCultureIgnoreCase) && c.Vlan == 1)
        .All(c => c.Address == null && c.Shutdown);
    }
  }
}