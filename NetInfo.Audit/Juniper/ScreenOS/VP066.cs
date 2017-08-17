using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set snmp name "=hostname" is configured
  /// </summary>
  public class VP066 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP066(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return string.Compare(device.Hostname.Name, device.SNMPSettings.Name, false) != -1;
    }
  }
}