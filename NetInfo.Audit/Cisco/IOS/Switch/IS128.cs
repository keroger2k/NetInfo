using System;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure the following command exists: "aaa authentication dot1x default group radius"
  ///
  /// Exceptions:
  ///   Devices that do not have user zone access switchports configured.
  ///   Device with IOS that does not support 802.1x.
  /// </summary>
  public class IS128 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS128(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIIOSDevice)Device);
      return device.AAA.Authentication.Dot1x.DefaultGroup.Equals("radius", StringComparison.OrdinalIgnoreCase);
    }
  }
}