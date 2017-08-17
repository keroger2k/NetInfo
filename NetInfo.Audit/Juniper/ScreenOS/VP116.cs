using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate unset alg sip enable is configured
  /// </summary>
  public class VP116 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP116(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIScreenOSDevice)Device);
      return device.AlgSettings.Sip;
    }
  }
}