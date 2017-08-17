using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure the 802.1x reauthentication timeout is set to the default 3600 seconds or shorter.
  ///
  ///Exceptions:
  /// Devices that do not have user zone access switchports configured.
  /// Device with IOS that does not support 802.1x."
  /// </summary>
  public class IS131 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS131(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.Interfaces
        .Where(c => c.SwitchPort.Type == IOSInterface.SwitchPortSettings.PortType.Access)
        .All(c => c.Dot1x.ReauthTimeout != -1 && c.Dot1x.ReauthTimeout <= 3600);
    }
  }
}