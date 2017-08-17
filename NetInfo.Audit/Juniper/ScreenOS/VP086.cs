using System;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate the following line exists under "get license":  "Capacity:  unlimited number of users"
  /// </summary>
  public class VP086 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP086(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.GetLicense.Capacity.Equals("unlimited number of users", StringComparison.OrdinalIgnoreCase);
    }
  }
}