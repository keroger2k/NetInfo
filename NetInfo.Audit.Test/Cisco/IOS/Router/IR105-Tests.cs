using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Audit.Models;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR105_Tests {

    [Test]
    public void IR105_should_return_true_when_all_acls_applied_are_the_correct_major_version() {
      var blob = new AssetBlob {
        Body = @"!
interface Vlan210
 description <== User VLAN 210 ==>
!
interface Vlan500
 description <== Printer VLAN 500 ==>
 ip access-group NMCI_Printer_VLAN_ACL_IN_V17-2-0 in
 ip access-group NMCI_Printer_VLAN_ACL_OUT_V17-2-0 out
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR105(device, new[] {
        new AccessControlList { FullName = "NMCI_Printer_VLAN_ACL_IN_V17-2-0", ShortName = "NMCI_Printer_VLAN_ACL_IN", MajorVersion = 17, MinorVersion = 2 },
        new AccessControlList { FullName = "NMCI_Printer_VLAN_ACL_OUT_V17-2-0", ShortName = "NMCI_Printer_VLAN_ACL_OUT", MajorVersion = 17, MinorVersion = 2 },
      });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR105_should_return_false_when_not_all_acls_applied_are_the_correct_major_version() {
      var blob = new AssetBlob {
        Body = @"!
interface Vlan210
 description <== User VLAN 210 ==>
!
interface Vlan500
 description <== Printer VLAN 500 ==>
 ip access-group NMCI_Printer_VLAN_ACL_IN_V18-2-0 in
 ip access-group NMCI_Printer_VLAN_ACL_OUT_V17-2-0 out
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR105(device, new[] {
        new AccessControlList { FullName = "NMCI_Printer_VLAN_ACL_IN_V17-2-0", ShortName = "NMCI_Printer_VLAN_ACL_IN", MajorVersion = 17, MinorVersion = 2 },
        new AccessControlList { FullName = "NMCI_Printer_VLAN_ACL_OUT_V17-2-0", ShortName = "NMCI_Printer_VLAN_ACL_OUT", MajorVersion = 17, MinorVersion = 2 },
      });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}