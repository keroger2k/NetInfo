using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR101_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IR101_should_return_true_for_complaint_device() {
      blob = new AssetBlob {
        Body = @"!
interface Vlan500
 description <== Printer VLAN 500 ==>
 ip address 10.46.4.73 255.255.255.248
 ip access-group NMCI_Printer_VLAN_ACL_IN_V17-2-0 in
 ip access-group NMCI_Printer_VLAN_ACL_OUT_V17-2-0 out
 no ip redirects
 no ip proxy-arp
!
ip classless
i"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR101(device, new[] { "V17", "old" }, new[] { "V17", "old" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR101_should_return_false_when_inbound_acl_is_incorrect() {
      blob = new AssetBlob {
        Body = @"!
interface Vlan500
 description <== Printer VLAN 500 ==>
 ip address 10.46.4.73 255.255.255.248
 ip access-group NMCI_Printer_VLAN_ACL_IN_V16-2-0 in
 ip access-group NMCI_Printer_VLAN_ACL_OUT_V17-2-0 out
 no ip redirects
 no ip proxy-arp
!
ip classless
i"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR101(device, new[] { "V17", "old" }, new[] { "V17", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR101_should_return_false_when_outbound_acl_is_incorrect() {
      blob = new AssetBlob {
        Body = @"!
interface Vlan500
 description <== Printer VLAN 500 ==>
 ip address 10.46.4.73 255.255.255.248
 ip access-group NMCI_Printer_VLAN_ACL_IN_V17-2-0 in
 ip access-group NMCI_Printer_VLAN_ACL_OUT_V16-2-0 out
 no ip redirects
 no ip proxy-arp
!
ip classless
i"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR101(device, new[] { "V17", "old" }, new[] { "V17", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR101_should_return_false_when_both_acls_are_incorrect() {
      blob = new AssetBlob {
        Body = @"!
interface Vlan500
 description <== Printer VLAN 500 ==>
 ip address 10.46.4.73 255.255.255.248
 ip access-group NMCI_Printer_VLAN_ACL_IN_V16-2-0 in
 ip access-group NMCI_Printer_VLAN_ACL_OUT_V16-2-0 out
 no ip redirects
 no ip proxy-arp
!
ip classless
i"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR101(device, new[] { "V17", "old" }, new[] { "V17", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}