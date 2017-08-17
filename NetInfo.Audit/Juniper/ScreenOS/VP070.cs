using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set auth-server "Local" server-name "Local"
  /// </summary>
  public class VP070 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP070(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.AutherServerName;
    }
  }
}