using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set admin name "NS-ADMIN"
  /// </summary>
  public class VP010 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP010(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIScreenOSDevice)Device);
      return device.AdminSettings.Name.Equals("NS-ADMIN", System.StringComparison.OrdinalIgnoreCase);
    }
  }
}