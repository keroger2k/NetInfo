using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate unset alg sunrpc enable is configured
  /// </summary>
  public class VP119 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP119(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIScreenOSDevice)Device);
      return device.AlgSettings.Sunrpc;
    }
  }
}