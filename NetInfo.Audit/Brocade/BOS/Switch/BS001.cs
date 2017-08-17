using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure all applied ACLs w/o a terminating "permit ip any any" include a terminating "deny ip any any log" entry (see comment)
  /// </summary>
  public class BS001 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS001(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      var standardAcls = device.StandardAccessLists.ToList();
      bool standardResult = false;

      if (standardAcls.Any()) {
        standardResult = standardAcls
          .Where(c => !new Regex(@"access-list\s+\d+\s+permit\s+ip\s+any", RegexOptions.IgnoreCase).Match(c.RulesNoComments.Last()).Success)
          .All(c => new Regex(@"access-list\s+\d+\s+deny\s+any\s+log", RegexOptions.IgnoreCase).Match(c.RulesNoComments.Last()).Success);
      } else {
        standardResult = true;
      }

      return standardResult;
    }
  }
}