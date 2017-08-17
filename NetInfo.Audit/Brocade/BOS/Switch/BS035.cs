using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NetInfo.Devices.NMCI.Infrastructure.ExtensionMethods;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure ACL69 exists and is properly configured according to the ACL template
  /// </summary>
  public class BS035 : ISTIGItem {

    private IEnumerable<string> acl = @"access-list 69 permit 10.16.27.32 0.0.0.31
access-list 69 permit 10.0.18.240 0.0.0.7
access-list 69 permit 10.0.16.128 0.0.0.31
access-list 69 permit 10.32.9.224 0.0.0.31
access-list 69 permit 10.8.119.32 0.0.0.31
access-list 69 permit 10.4.120.0 0.0.0.255
access-list 69 permit 10.19.120.0 0.0.0.255
access-list 69 permit 10.18.57.0 0.0.0.255
access-list 69 permit 10.3.57.0 0.0.0.255
access-list 69 permit 10.27.75.0 0.0.0.255
access-list 69 permit 10.0.195.0 0.0.0.255
access-list 69 permit 10.22.120.0 0.0.0.255
access-list 69 permit 10.18.184.0 0.0.0.255
access-list 69 permit 10.2.248.0 0.0.0.255
access-list 69 permit 10.16.21.0 0.0.0.255
access-list 69 permit 10.6.25.0 0.0.0.255
access-list 69 permit 10.20.184.0 0.0.0.255
access-list 69 permit 10.31.226.0 0.0.1.255
access-list 69 permit 10.22.248.0 0.0.0.255
access-list 69 permit 10.32.119.0 0.0.0.255
access-list 69 permit 10.2.57.0 0.0.0.255
access-list 69 permit 10.0.11.0 0.0.0.255
access-list 69 permit 10.3.248.0 0.0.0.255
access-list 69 permit 10.33.24.0 0.0.0.255
access-list 69 permit 10.1.121.0 0.0.0.255
access-list 69 permit 10.28.249.0 0.0.0.255
access-list 69 deny any log".ToConfig();

    public IDevice Device { get; private set; }

    public BS035(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      var acl69 = device.StandardAccessLists.FirstOrDefault(c => c.Number == 69);
      return acl69 != null && acl.SequenceEqual(acl69.RulesNoComments.Select(c => c.Trim()));
    }
  }
}