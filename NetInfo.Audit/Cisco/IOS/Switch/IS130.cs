using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure all access ports are configured with one of the following commands: "dot1x port-control" or "authentication port-control auto
  ///
  /// Exceptions:
  ///   Devices that do not have user zone access switchports configured.
  ///   Device with IOS that does not support 802.1x.
  /// </summary>
  public class IS130 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS130(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.Interfaces
        .Where(c => c.SwitchPort.Type == IOSInterface.SwitchPortSettings.PortType.Access)
        .All(c => c.Authentication.PortControlAuto);
    }
  }
}