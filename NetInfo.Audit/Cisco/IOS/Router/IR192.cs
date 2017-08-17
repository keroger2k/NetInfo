using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure ACL68 is properly configured according to the ACL template
  /// </summary>
  public class IR192 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> _approvedAcl;

    public IR192(INMCIIOSDevice device, IEnumerable<string> approvedAcl) {
      this.Device = device;
      this._approvedAcl = approvedAcl;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      IEnumerable<string> acl68 = device.StandardAccessLists.FirstOrDefault(c => c.Number == 68).Rules.Select(c => c.Trim());
      return _approvedAcl.SequenceEqual(acl68);
    }
  }
}