using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices.NMCI.Infrastructure.ExtensionMethods;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  ///
  /// Ensure the uTB WAN Outbound ACL is properly configured according to the ACL template.
  /// </summary>
  public class IR162 : ISTIGItem {

    private readonly IEnumerable<string> approvedAcl = @" deny   ip 0.0.0.0 0.255.255.255 any log
 deny   ip 10.0.0.0 0.255.255.255 any log
 deny   ip 14.0.0.0 0.255.255.255 any log
 deny   ip 24.0.0.0 0.255.255.255 any log
 deny   ip 39.0.0.0 0.255.255.255 any log
 deny   ip 127.0.0.0 0.255.255.255 any log
 deny   ip 128.0.0.0 0.0.255.255 any log
 deny   ip 169.254.0.0 0.0.255.255 any log
 deny   ip 172.16.0.0 0.15.255.255 any log
 deny   ip 191.255.0.0 0.0.255.255 any log
 deny   ip 192.0.0.0 0.0.0.255 any log
 deny   ip 192.0.2.0 0.0.0.255 any log
 deny   ip 192.88.99.0 0.0.0.255 any log
 deny   ip 192.168.0.0 0.0.255.255 any log
 deny   ip 198.18.0.0 0.1.255.255 any log
 deny   ip 198.51.100.0 0.0.0.255 any log
 deny   ip 203.0.113.0 0.0.0.255 any log
 deny   ip 223.255.255.0 0.0.0.255 any log
 deny   ip 224.0.0.0 15.255.255.255 any log
 deny   ip 240.0.0.0 15.255.255.255 any log
 deny   ip 255.255.255.255 0.0.0.0 any log
 deny   ip host 255.255.255.255 any log
 permit icmp any any echo
 permit icmp any any echo-reply
 permit icmp any any packet-too-big
 permit icmp any any source-quench
 permit icmp any any time-exceeded
 permit icmp any any parameter-problem
 deny   icmp any any log
 deny   udp any any eq 3544 log
 permit ip any any
 deny   ip any any log".ToConfig();

    public IDevice Device { get; private set; }

    private readonly Regex aclRegex = new Regex(@"OR0[1|2]_NMCI_UTB_OUT", RegexOptions.IgnoreCase);

    public IR162(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var currentAcl = device.ExtendedAccessLists
        .Where(c => aclRegex.Match(c.Name).Success);
      if (currentAcl.Count() > 1) return false;
      var aclToCheck = currentAcl.FirstOrDefault();
      if (aclToCheck != null && aclToCheck.Rules.Any()) {
        //check to make sure all the lines are the ACL to be checked are in the approved ACL
        foreach (var item in aclToCheck.RulesNoComments) {
          if (!approvedAcl.Contains(item)) {
            return false;
          }
        }

        //Now check that the number of lines are equal.
        //Approved ACL contains an extra line for devices that convert 255.255.255.255/0.0.0.0 to host deny statement.
        if (approvedAcl.Count() - 1 != aclToCheck.RulesNoComments.Count()) {
          return false;
        }
      }

      return true;
    }
  }
}