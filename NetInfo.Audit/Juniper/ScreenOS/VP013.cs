using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set ike respond-bad-spi 1 (ScreenOS 4.x only)
  /// </summary>
  public class VP013 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP013(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.IKESettings.RepondBadSpi == 1;
    }
  }
}