using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set xauth lifetime 1
  /// </summary>
  public class VP023 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP023(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.XAuthSettings.Lifetime == 1;
    }
  }
}