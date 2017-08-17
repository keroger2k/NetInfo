using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set snmp vpn is configured (ScreenOS 4.x only)
  /// </summary>
  public class VP079 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP079(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.SNMPSettings.Vpn;
    }
  }
}