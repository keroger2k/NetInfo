using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure SNMPv3 group name is valid per the NMS script.
  ///
  /// Note: This check only applies to devices running a k2 or k9 image
  /// </summary>
  public class IR201 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly string[] approvedGroupNames = new[] { "nmcigroup" };

    public IR201(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      bool compliant = false;
      if (device.ShowSnmpUser.UserSettings != null && device.ShowSnmpUser.UserSettings.Any(c => !string.IsNullOrEmpty(c.GroupName))) {
        compliant = device.ShowSnmpUser.UserSettings.All(c => approvedGroupNames.Contains(c.GroupName)) &&
          device.ShowSnmpGroup.GroupSettings.All(c => approvedGroupNames.Contains(c.GroupName));
      } else if (device.ShowSnmp.Enabled) {
        compliant = device.SNMPSettings.Groups.All(c => approvedGroupNames.Contains(c.Name));
      }
      return compliant;
    }
  }
}