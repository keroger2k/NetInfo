using System;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure no access ports are assigned to VLAN1
  /// </summary>
  public class IS053 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS053(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var accessPorts = device.ShowInterfaceStatus.Interfaces.Where(c =>
        (c.Status == ShowInterfaceStatus.InterfaceStatus.connected ||
        c.Status == ShowInterfaceStatus.InterfaceStatus.notconnect ||
        c.Status == ShowInterfaceStatus.InterfaceStatus.inactive ||
        c.Status == ShowInterfaceStatus.InterfaceStatus.notconnected) &&
        !c.Vlan.Equals("trunk", StringComparison.OrdinalIgnoreCase));
      return accessPorts.All(c => !c.Vlan.Equals("1"));
    }
  }
}