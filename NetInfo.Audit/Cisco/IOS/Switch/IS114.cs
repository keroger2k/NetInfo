using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure ACL69 is properly configured according to the ACL template
  /// </summary>
  public class IS114 : ISTIGItem {
    private const string DENY_LOG_STATEMENT = @"access-list\s+69\s+deny\s+any\s+log";

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> _approvedAcl;

    public IS114(INMCIIOSDevice device, IEnumerable<string> approvedAcl) {
      this.Device = device;
      this._approvedAcl = approvedAcl;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      IEnumerable<string> acl69 = device.StandardAccessLists.FirstOrDefault(c => c.Number == 69).Rules.Select(c => c.Trim());
      return acl69.Any() &&
        new Regex(DENY_LOG_STATEMENT, RegexOptions.IgnoreCase).Match(acl69.Last()).Success &&
        _approvedAcl.OrderBy(c => c).SequenceEqual(acl69.OrderBy(c => c));
    }
  }
}