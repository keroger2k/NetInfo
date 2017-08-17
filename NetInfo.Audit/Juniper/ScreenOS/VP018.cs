using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set scs enable (ScreenOS 4.x only)
  /// </summary>
  public class VP018 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP018(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.SCSEnabled;
    }
  }
}