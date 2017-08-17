using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validiate set snmp name "=hostname" is configured
  /// </summary>
  public class VP064 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP064(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.Hostname.Name.Equals(device.SNMPSettings.Name, System.StringComparison.OrdinalIgnoreCase);
    }
  }
}