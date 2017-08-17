using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Validate the "snmp-server group" command for unclassified devices includes
  /// "access 67" or "access 68" or "access 69" and for classified devices include "access 69"
  /// </summary>
  public class IR042 : ISTIGItem {

    public IDevice Device { get; private set; }

    private int[] allowedAccessLists = new int[] { 67, 68, 69 };

    public IR042(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return (device.SNMPSettings.Groups != null && device.SNMPSettings.Groups.Any(c => c.AccessGroup != 0) ?
        allowedAccessLists.Union(device.SNMPSettings.Groups.Select(c => c.AccessGroup)).Count() == allowedAccessLists.Count() :
        device.ShowSnmpUser.UserSettings != null && device.ShowSnmpUser.UserSettings.All(c => c.StorageType != null && allowedAccessLists.Contains(c.StorageType.AccessGroup)));
    }
  }
}