using System;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set auth default auth server "Local" (ScreenOS 4.x only)
  /// </summary>
  public class VP068 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP068(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.AutherServer.Equals("Local", StringComparison.OrdinalIgnoreCase);
    }
  }
}