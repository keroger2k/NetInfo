using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure VLAN1 is pruned from all trunks
  ///
  /// Exceptions:
  ///   interfaces connected to a DP or WR router must include vlan1 (switchport trunk allowed vlan 1,99-1005);
  ///   at GRLK, interfaces on tail-end switches connecting to a WP or WB device;
  ///   all trunking interfaces on 2900XL and 3500XL switches must include vlan1.
  /// </summary>
  public class IS012 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS012(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var a = device.Interfaces.Where(c => c.Physical);
      var b = a.Where(c => c.SwitchPort.Type == IOSInterface.SwitchPortSettings.PortType.Trunk);
      var d = !b.Any() || b.Any(c => !c.SwitchPort.AllowedVlans.Contains(1));
      return d;
    }
  }
}