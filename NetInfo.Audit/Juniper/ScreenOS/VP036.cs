using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set console timeout 3
  /// </summary>
  public class VP036 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP036(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.ConsoleSettings.Timeout == 3;
    }
  }
}