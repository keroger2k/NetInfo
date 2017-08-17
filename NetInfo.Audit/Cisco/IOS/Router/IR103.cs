using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Remove all references to ACLs that don't exist or add the ACL to the configuration
  /// </summary>
  public class IR103 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR103(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var appliedAcls = device.Interfaces
        .Where(c => c.AccessGroups.Any())
        .SelectMany(c => c.AccessGroups.Select(d => d.Name))
        .ToList()
        .Distinct();

      var lineAcls = device.Lines
        .Where(c => c.AccessClass != 0)
        .Select(c => c.AccessClass)
        .ToList()
        .Distinct();

      IEnumerable<int> snmpAcls = new List<int>();

      if (device.SNMPSettings.Groups != null) {
        snmpAcls = device.SNMPSettings.Groups
        .Where(c => c.AccessGroup != 0)
        .Select(c => c.AccessGroup)
        .ToList()
        .Distinct();
      }

      var configuredExtendedAcls = device.ExtendedAccessLists.Select(c => c.Name).Distinct();
      var configuredstandardAcls = device.StandardAccessLists.Select(c => c.Number).Distinct();

      foreach (var item in appliedAcls) {
        if (!configuredExtendedAcls.Contains(item)) {
          return false;
        }
      }

      foreach (var item in lineAcls) {
        if (!configuredstandardAcls.Contains(item)) {
          return false;
        }
      }

      foreach (var item in snmpAcls) {
        if (!configuredstandardAcls.Contains(item)) {
          return false;
        }
      }

      return true;
    }
  }
}