using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure all interfaces participating in OSPF are configured with MD5 keys when OSPF is configured
  ///
  /// Unclassified:
  ///   Vlan99 and Vlan98: These Vlans should not be enforced yet.
  ///   Vlan90, Vlan91, Vlan191 and Vlan190: Only these Vlans should be enforced.
  /// </summary>
  public class IR001 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<int> approvedVlans = new int[] { 90, 91, 190, 191 };
    private IEnumerable<string> approvedNonPassiveInterfaces = new string[] { "Vlan90", "Vlan91", "Vlan190", "Vlan191" };

    public IR001(IIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (IIOSDevice)Device;
      var nonPassiveInterfaces = approvedNonPassiveInterfaces.Where(c => device.OSPF.NonPassiveInterfaces.Contains(c));

      return device.IsOSPFConfigured && device.Interfaces
        .Where(c => !c.Physical && approvedVlans.Contains(c.Vlan) && nonPassiveInterfaces.Contains(c.ShortName) && !c.Shutdown)
        .All(c => c.IP.OSPF.Enabled && !string.IsNullOrEmpty(c.IP.OSPF.MessageDigest));
    }
  }
}