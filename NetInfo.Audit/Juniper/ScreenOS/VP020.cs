using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set ssh enable (ScreenOS 5.x only)
  /// </summary>
  public class VP020 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP020(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.SSHEnabled;
    }
  }
}