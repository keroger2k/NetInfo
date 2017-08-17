using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure Vlan1 is not configured on any enabled access port
  /// </summary>
  public class IS142 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS142(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return !device.Interfaces.Any(c => c.Physical && c.SwitchPort.Type == Devices.Cisco.IOS.IOSInterface.SwitchPortSettings.PortType.Access && c.Vlan == 1);
    }
  }
}