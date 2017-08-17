using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure all disabled ports are assigned to VLAN2 (see comment)
  /// </summary>
  public class IS067 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS067(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var interfaces = ((INMCIIOSDevice)Device).Interfaces;
      return !interfaces.Any(c => c.Physical && c.Shutdown && c.Vlan != 2);
    }
  }
}