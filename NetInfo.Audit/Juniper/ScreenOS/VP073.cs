using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set flow path-mtu
  /// </summary>
  public class VP073 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP073(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.FlowSettings.PathMTUEnabled;
    }
  }
}