using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure VLAN1 is pruned from all trunks
  ///
  /// Exception:  878, 2600, 2800, 3600, 3700 and 3800 routers.   
  /// Vlan1 cannot be pruned.  The default Vlans 1,1002-1005 must be included in the vlan allowed list. 
  /// (e.g., switchport trunk allowed 1,99-1005)
  /// </summary>
  public class IR012 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR012(INMCIIOSDevice device) {
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