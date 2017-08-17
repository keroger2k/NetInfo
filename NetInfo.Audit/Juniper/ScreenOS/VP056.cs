using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set clock ntp
  /// </summary>
  public class VP056 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP056(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.ClockSettings.NTPEnabled;
    }
  }
}