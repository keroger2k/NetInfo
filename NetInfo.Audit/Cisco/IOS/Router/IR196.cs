using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure Vlan1 is not configured on any enabled access port
  /// </summary>
  public class IR196 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR196(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return !device.Interfaces.Any(c => c.SwitchPort.Type == IOSInterface.SwitchPortSettings.PortType.Access && c.Vlan == 1);
    }
  }
}