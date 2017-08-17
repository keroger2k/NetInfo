using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate the Network Time Protocol is Enabled
  /// </summary>
  public class VP059 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP059(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.NetworkTimeEnabled;
    }
  }
}