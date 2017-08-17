using System;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set admin auth server "Local"
  /// </summary>
  public class VP080 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP080(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIScreenOSDevice)Device);
      return device.AdminSettings.AuthServer.Equals("Local", StringComparison.OrdinalIgnoreCase);
    }
  }
}