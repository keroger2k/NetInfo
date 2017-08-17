using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate The Network Time Protocol is Disabled
  /// </summary>
  public class VP038 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP038(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return !device.NetworkTimeEnabled;
    }
  }
}