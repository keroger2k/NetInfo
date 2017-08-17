using System;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure the virtual routing interface for vlan 1 does not exist: "inteface ve 1"
  /// </summary>
  public class BS041 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS041(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIBOSDevice)Device);
      return device.Interfaces
        .Where(c => c.Type.Equals("ve", StringComparison.OrdinalIgnoreCase))
        .All(c => !c.Number.Equals("1"));
    }
  }
}