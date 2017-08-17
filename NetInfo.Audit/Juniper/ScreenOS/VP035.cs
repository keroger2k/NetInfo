using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate this command exists: "set admin auth timeout 10" or "set admin auth web timeout 10"
  /// </summary>
  public class VP035 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP035(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIScreenOSDevice)Device);
      return device.AdminSettings.AuthTimeout == 10;
    }
  }
}