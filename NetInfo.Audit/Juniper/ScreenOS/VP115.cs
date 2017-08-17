using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set flow route tunnel prefer-reverse-route is configured
  /// </summary>
  public class VP115 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP115(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIScreenOSDevice)Device);
      return device.FlowSettings.PreferReverseRouteEnabled;
    }
  }
}