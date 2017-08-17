using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure all applied ACLs w/o a terminating "permit ip any any" include a terminating "deny ip any any log" entry (see comment)
  /// </summary>
  public class IR010 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR010(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var standardAcls = device.StandardAccessLists.ToList();
      var extendedAcls = device.ExtendedAccessLists.ToList();
      var appliedAccessGroups = device.Interfaces.SelectMany(c => c.AccessGroups).Select(c => c.Name);
      var routeMapAcls = device.RouteMaps.SelectMany(c => c.GetMatchStandardAccessLists()).ToList();
      //removing extended access groups that are not applied to an interface.
      extendedAcls = extendedAcls.Where(c => appliedAccessGroups.Contains(c.Name)).ToList();
      standardAcls = standardAcls.Where(c => !routeMapAcls.Contains(c.Number)).ToList();

      bool standardResult = false;
      bool extendedResult = false;

      if (standardAcls.Any()) {
        standardResult = standardAcls
          .Where(c => !new Regex(@"access-list\s+\d+\s+permit\s+ip\s+any", RegexOptions.IgnoreCase).Match(c.RulesNoComments.Last()).Success)
          .All(c => new Regex(@"access-list\s+\d+\s+deny\s+any\s+log", RegexOptions.IgnoreCase).Match(c.RulesNoComments.Last()).Success);
      } else {
        standardResult = true;
      }

      if (extendedAcls.Any()) {
        extendedResult = extendedAcls
          .Where(c => !new Regex(@"permit\s+ip\s+any\s+any", RegexOptions.IgnoreCase).Match(c.RulesNoComments.Last()).Success)
          .All(c => new Regex(@"deny\s+ip\s+any\s+any\s+log", RegexOptions.IgnoreCase).Match(c.RulesNoComments.Last()).Success);
      } else {
        extendedResult = true;
      }

      return standardResult && extendedResult;
    }
  }
}