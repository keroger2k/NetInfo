using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure ACL98 is properly configured according to the ACL template
  /// </summary>
  public class IR193 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> _approvedAcl;

    public IR193(INMCIIOSDevice device, IEnumerable<string> approvedAcl) {
      this.Device = device;
      this._approvedAcl = approvedAcl;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      IEnumerable<string> acl98 = device.StandardAccessLists.FirstOrDefault(c => c.Number == 98).Rules.Select(c => c.Trim());
      return _approvedAcl.SequenceEqual(acl98);
    }
  }
}