using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure SNMPv3 user name is valid per the NMS script.
  ///
  /// Note: This check only applies to devices running a k2 or k9 image
  /// </summary>
  public class IS146 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly string[] approvedUserNames = new[] { "nmsops" };

    public IS146(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      bool compliant = false;
      if (device.ShowSnmpUser.UserSettings != null && device.ShowSnmpUser.UserSettings.Any(c => !string.IsNullOrEmpty(c.Username))) {
        compliant = device.ShowSnmpUser.UserSettings.All(c => approvedUserNames.Contains(c.Username));
      } else if (device.ShowSnmp.Enabled) {
        compliant = device.SNMPSettings.Servers.All(c => approvedUserNames.Contains(c.Username));
      }
      return compliant;
    }
  }
}