using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR100_Tests {
    private AssetBlob blob;

    [Test]
    public void IR100_should_return_true_for_devices_with_printer_vlans_that_have_printer_acls() {
      blob = new AssetBlob {
        Body = @"!
interface Vlan210
description <== User VLAN 210 ==>
ip address 10.46.4.97 255.255.255.224
ip helper-address 10.16.25.23
ip helper-address 10.16.25.24
no ip redirects
no ip proxy-arp
!
interface Vlan500
description <== Printer VLAN 500 ==>
ip address 10.46.4.73 255.255.255.248
ip access-group NMCI_Printer_VLAN_ACL_IN_V17-2-0 in
ip access-group NMCI_Printer_VLAN_ACL_OUT_V17-2-0 out
no ip redirects
no ip proxy-arp
!
ip classless"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR100(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR100_should_return_false_for_devices_with_printer_vlans_that_have_zero_printer_acls() {
      blob = new AssetBlob {
        Body = @"!
interface Vlan210
description <== User VLAN 210 ==>
ip address 10.46.4.97 255.255.255.224
ip helper-address 10.16.25.23
ip helper-address 10.16.25.24
no ip redirects
no ip proxy-arp
!
interface Vlan500
description <== Printer VLAN 500 ==>
ip address 10.46.4.73 255.255.255.248
no ip redirects
no ip proxy-arp
!
ip classless"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR100(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR100_should_return_false_for_devices_with_printer_vlans_that_have_one_printer_acls() {
      blob = new AssetBlob {
        Body = @"!
interface Vlan210
description <== User VLAN 210 ==>
ip address 10.46.4.97 255.255.255.224
ip helper-address 10.16.25.23
ip helper-address 10.16.25.24
no ip redirects
no ip proxy-arp
!
interface Vlan500
description <== Printer VLAN 500 ==>
ip address 10.46.4.73 255.255.255.248
ip access-group NMCI_Printer_VLAN_ACL_IN_V17-2-0 in
no ip redirects
no ip proxy-arp
!
ip classless"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR100(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}