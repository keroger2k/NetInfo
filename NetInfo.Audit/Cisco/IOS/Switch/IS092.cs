using System;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Clock timezone is utc
  /// </summary>
  public class IS092 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS092(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var clock = device.Clock;
      var showClock = device.ShowClock;
      return clock == null ? showClock.Settings.Contains("UTC") : clock.Timezone.Equals("UTC", StringComparison.OrdinalIgnoreCase);
    }
  }
}