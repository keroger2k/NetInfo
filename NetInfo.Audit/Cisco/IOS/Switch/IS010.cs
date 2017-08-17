using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure all applied ACLs w/o a terminating "permit ip any any" include a terminating "deny ip any any log" entry (see comment)
  /// </summary>
  public class IS010 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS010(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var standardAcls = device.StandardAccessLists.ToList();
      var extendedAcls = device.ExtendedAccessLists.ToList();
      bool standardResult = false;
      bool extendedResult = false;

      if (standardAcls.Any() && standardAcls.All(c => c.RulesNoComments.Any())) { //Make sure the ACL isn't just remarks.
        standardResult = standardAcls
          .Where(c => !new Regex(@"access-list\s+\d+\s+permit\s+ip\s+any", RegexOptions.IgnoreCase).Match(c.RulesNoComments.Last()).Success)
          .All(c => new Regex(@"access-list\s+\d+\s+deny\s+any\s+log", RegexOptions.IgnoreCase).Match(c.RulesNoComments.Last()).Success);
      } else {
        standardResult = true;
      }

      if (extendedAcls.Any() && extendedAcls.All(c => c.RulesNoComments.Any())) {
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