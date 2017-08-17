using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure all permitted and denied entries in ACL97 are logged
  /// </summary>
  public class IS141 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS141(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var acls = device.StandardAccessLists
        .Where(c => new int[] { 97 }
          .Contains(c.Number))
          .SelectMany(c => c.RulesNoComments)
          .ToList();
      return acls
        .All(c => new Regex(@"^access-list\s+\d+.*log", RegexOptions.IgnoreCase).Match(c).Success);
    }
  }
}