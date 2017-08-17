using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate unset alg h323 enable is configured
  /// </summary>
  public class VP123 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP123(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIScreenOSDevice)Device);
      return device.AlgSettings.H323;
    }
  }
}