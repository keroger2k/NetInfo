using System;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Printer VLAN ACL is applied on the printer VLANs
  /// </summary>
  public class IR100 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR100(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIIOSDevice)Device);
      var inter = device.Interfaces.Where(c => !c.Physical && !c.Shutdown && c.Vlan >= 500 && c.Vlan <= 599);
      return inter.Any(c =>
        c.AccessGroups.Any(d => d.Name.Contains(@"NMCI_Printer_VLAN_ACL_IN") && d.Direction.Equals("in", StringComparison.InvariantCultureIgnoreCase)) &&
        c.AccessGroups.Any(d => d.Name.Contains(@"NMCI_Printer_VLAN_ACL_OUT") && d.Direction.Equals("out", StringComparison.InvariantCultureIgnoreCase)));
    }
  }
}