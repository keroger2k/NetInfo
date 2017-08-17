using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate unset alg mgcp enable is configured
  /// </summary>
  public class VP117 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP117(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIScreenOSDevice)Device);
      return device.AlgSettings.Mgcp;
    }
  }
}