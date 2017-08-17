using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure the following command exists: "dot1x system-auth-control"
  ///
  /// Exceptions:
  ///   Devices that do not have user zone access switchports configured.
  ///   Device with IOS that does not support 802.1x.
  /// </summary>
  public class IS129 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS129(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIIOSDevice)Device);
      return device.Dot1xSettings.SystemAuthControl;
    }
  }
}