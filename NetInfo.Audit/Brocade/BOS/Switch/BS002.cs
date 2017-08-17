using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure all applied ACLs include "log" statements at the end of each deny line
  /// </summary>
  public class BS002 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS002(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      var acls = device.StandardAccessLists
        .Where(c => new int[] { 69, 99 }
          .Contains(c.Number))
          .SelectMany(c => c.RulesNoComments)
          .ToList();
      return acls
        .Where(c => new Regex(@"^access-list\s+\d+\s+deny", RegexOptions.IgnoreCase).Match(c).Success)
        .All(c => new Regex(@"^access-list\s+\d+\s+deny.*log", RegexOptions.IgnoreCase).Match(c).Success);
    }
  }
}