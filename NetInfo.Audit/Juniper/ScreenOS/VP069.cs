using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set auth-server "Local" id 0
  /// </summary>
  public class VP069 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP069(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.AutherServerLocalId;
    }
  }
}