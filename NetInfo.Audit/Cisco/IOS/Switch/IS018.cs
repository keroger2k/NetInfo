using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure SNMPv3 uses DES encryption (see "show snmp user" output)
  /// </summary>
  public class IS018 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly string[] approvedProtocols = new[] { "DES", "3DES", "AES", "AES256", "AES192", "AES128" };

    public IS018(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      bool compliant = false;
      if (device.ShowSnmpUser.UserSettings != null && device.ShowSnmpUser.UserSettings.Any(c => !string.IsNullOrEmpty(c.PrivacyProtocol))) {
        compliant = device.ShowSnmpUser.UserSettings.All(c => approvedProtocols.Contains(c.PrivacyProtocol));
      } else {
        compliant =
          device.SNMPSettings.Servers.Any() &&
          device.SNMPSettings.Servers.All(c => c.VersionKeyword.Equals("priv", System.StringComparison.OrdinalIgnoreCase)) &&
          device.SNMPSettings.Groups != null &&
          device.SNMPSettings.Groups.All(c => c.VerionKeyword.Equals("priv", System.StringComparison.OrdinalIgnoreCase));
      }
      return compliant;
    }
  }
}