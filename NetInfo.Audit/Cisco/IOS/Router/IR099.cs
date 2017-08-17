using System;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Clock timezone is utc
  /// </summary>
  public class IR099 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR099(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIIOSDevice)Device).Clock;
      return device != null &&
        device.Timezone.Equals("utc", StringComparison.InvariantCultureIgnoreCase) &&
        device.HourOffset == 0 &&
        device.MinuteOffset == 0;
    }
  }
}