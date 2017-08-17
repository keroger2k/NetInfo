using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate use interface IP, Config Port: 80
  /// </summary>
  public class VP025 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP025(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.UseInterfaceConfigPort80;
    }
  }
}