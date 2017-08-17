using System;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set ike id-mode subnet (ScreenOS 4.x only)
  /// </summary>
  public class VP011 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP011(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.IKESettings.IdMode.Equals("subnet", StringComparison.OrdinalIgnoreCase);
    }
  }
}