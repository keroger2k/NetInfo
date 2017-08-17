using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set clock dst-off
  /// </summary>
  public class VP060 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP060(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.ClockSettings.DaylightSavingsTimeOffEnabled;
    }
  }
}