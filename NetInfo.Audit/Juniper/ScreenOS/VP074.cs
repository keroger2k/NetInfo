using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set pki authority default cert-status revocation-check best-effort (ScreenOS 4.x only)
  /// </summary>
  public class VP074 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP074(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.PKISettings.RevocationCheckBestEffortEanbled;
    }
  }
}