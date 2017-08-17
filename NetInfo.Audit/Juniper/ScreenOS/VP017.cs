using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set pki x509 raw-cn enable
  /// </summary>
  public class VP017 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP017(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.PKISettings.x509.RawCn;
    }
  }
}