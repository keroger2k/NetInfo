using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure all permitted and denied entries in ACL99 are logged
  /// </summary>
  public class BS005 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS005(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      var acls = device.StandardAccessLists
        .Where(c => new int[] { 99 }
          .Contains(c.Number))
          .SelectMany(c => c.RulesNoComments)
          .ToList();
      return acls
        .All(c => new Regex(@"^access-list\s+\d+.*log", RegexOptions.IgnoreCase).Match(c).Success);
    }
  }
}