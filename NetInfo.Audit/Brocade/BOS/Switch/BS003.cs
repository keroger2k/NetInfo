using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure ACL99 only allows sessions from authorized IP addresses according to the ACL template
  /// </summary>
  public class BS003 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> _approvedAcl;

    public BS003(INMCIBOSDevice device, IEnumerable<string> approvedAcl) {
      this.Device = device;
      this._approvedAcl = approvedAcl;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      IEnumerable<string> acl99 = device.StandardAccessLists.FirstOrDefault(c => c.Number == 99).Rules.Select(c => c.Trim());
      return _approvedAcl.SequenceEqual(acl99);
    }
  }
}