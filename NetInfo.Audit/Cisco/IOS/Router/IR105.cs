using System.Collections.Generic;
using System.Linq;
using NetInfo.Audit.Models;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Validate all ACLs are at the current major version number
  /// </summary>
  public class IR105 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<AccessControlList> _lastestAccessControlLists;

    public IR105(INMCIIOSDevice device, IEnumerable<AccessControlList> lastestAccessControlLists) {
      this.Device = device;
      this._lastestAccessControlLists = lastestAccessControlLists;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var extendedAcls = device.Interfaces.Where(c => c.AccessGroups != null);
      if (extendedAcls != null) {
        var distinctAcls = extendedAcls.SelectMany(c => c.AccessGroups.Select(d => d.Name)).Distinct();
        foreach (var item in distinctAcls) {
          var m = AccessControlList.AccessControlListRegex.Match(item);
          if (m.Success) {
            var acl = new AccessControlList {
              FullName = item,
              ShortName = m.Groups["name"].Value,
              MajorVersion = int.Parse(m.Groups["majorVersion"].Value),
              MinorVersion = string.IsNullOrEmpty(m.Groups["minorVersion"].Value) ? 0 : int.Parse(m.Groups["minorVersion"].Value)
            };

            if (!_lastestAccessControlLists.Any(c => c.ShortName.Equals(acl.ShortName, System.StringComparison.OrdinalIgnoreCase) &&
              c.MajorVersion == acl.MajorVersion)) {
              return false;
            }
          }
        }
      }
      return true;
    }
  }
}