using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set xauth default auth server Local
  /// </summary>
  public class VP022 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP022(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.XAuthSettings.AuthServer.Equals("Local", System.StringComparison.OrdinalIgnoreCase);
    }
  }
}