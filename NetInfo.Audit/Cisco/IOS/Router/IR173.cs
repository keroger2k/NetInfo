using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure 802.1x reauthentication is enabled
  ///
  /// "The command to enable reauthentication is IOS version specific.
  /// Look for one of the following commands:  "authentication periodic" or "dot1x reauthentication"
  ///
  ///Exceptions:
  /// Devices that do not have user zone access switchports configured.
  /// Device with IOS that does not support 802.1x."
  /// </summary>
  public class IR173 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR173(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.Interfaces
        .Where(c => c.SwitchPort.Type == IOSInterface.SwitchPortSettings.PortType.Access)
        .All(c => c.Authentication.Periodic);
    }
  }
}