using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set pki authority default cert-status revocation-check crl best-effort (ScreenOS 5.x only)
  /// </summary>
  public class VP075 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP075(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.PKISettings.RevocationCheckBestEffortEanbled;
    }
  }
}