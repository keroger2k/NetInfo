using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Remove all old unused ACLs
  ///
  /// Two versions of operational ACL's (non standard ACL's such as the uB1 ACL or uB3 COI ACL)
  /// can be kept in the configuration: the current version plus the previous version.
  /// This is allowed in case there is an issue with the new ACL and it is necessary to
  /// backout and reapply the previous version to the interface.
  /// </summary>
  public class IR102 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR102(INMCIIOSDevice device) {
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

      var snmpAcls = 0;

      if (device.SNMPSettings.Groups != null) {
        snmpAcls = device.SNMPSettings.Groups
        .Where(c => c.AccessGroup != 0)
        .Select(c => c.AccessGroup)
        .ToList()
        .Distinct()
        .Count();
      }
      var configuredExtendedAcls = device.ExtendedAccessLists.Select(c => c.Name).Distinct();
      var configuredstandardAcls = device.StandardAccessLists.Select(c => c.Number).Distinct();

      return appliedAcls.Count() + lineAcls.Count() + snmpAcls == (configuredExtendedAcls.Count() + configuredstandardAcls.Count());
    }
  }
}