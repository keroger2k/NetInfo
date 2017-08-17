using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure all permitted and denied entries in ACL97, ACL98 or ACL99 are logged
  /// </summary>
  public class IR174 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR174(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var acls = device.StandardAccessLists
        .Where(c => new int[] { 99 }
          .Contains(c.Number))
          .SelectMany(c => c.RulesNoComments)
          .ToList();
      return acls.All(c => new Regex(@"^access-list\s+\d+.*log", RegexOptions.IgnoreCase).Match(c).Success);
    }
  }
}