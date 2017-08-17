using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set ssl encrypt 3des sha-1 is configured
  /// </summary>
  public class VP021 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP021(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.SSLSettings.Encryption.Equals(@"3des sha-1", System.StringComparison.OrdinalIgnoreCase);
    }
  }
}