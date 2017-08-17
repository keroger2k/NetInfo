using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR001_Tests {

    [Test]
    public void IR001_should_return_true_when_an_interface_that_usually_has_ospf_on_it_is_just_misconfigured_and_should_not_fail_ir001() {
      var blob = new AssetBlob {
        Body = @"!
hostname ABCD-U00-OR-01
!
interface Vlan90
 description <== Router Backbone 1 ==>
 ip address 158.236.224.171 255.255.255.240
 no ip redirects
 no ip proxy-arp
 ip ospf message-digest-key 1 md5 7 052C131B1E7C47051C
!
interface Vlan91
 description <== Router Backbone 1 ==>
 ip address 158.236.224.171 255.255.255.240
 no ip redirects
 no ip proxy-arp
 ip ospf message-digest-key 1 md5 7 052C131B1E7C47051C
!
interface Vlan190
 no ip address
!
!
router ospf 1001
 log-adjacency-changes
 area 0 authentication message-digest
 passive-interface default
 no passive-interface Vlan90
 no passive-interface Vlan91
 network 158.236.224.160 0.0.0.15 area 0
!
ip classless"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR001(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR001_should_return_true_when_ospf_is_configured_and_all_applicable_interfaces_are_configured_with_md5_keys() {
      var blob = new AssetBlob {
        Body = @"!
hostname ABCD-U00-OR-01
!
interface Vlan90
 description <== Router Backbone 1 ==>
 ip address 158.236.224.171 255.255.255.240
 no ip redirects
 no ip proxy-arp
 ip ospf message-digest-key 1 md5 7 052C131B1E7C47051C
!
!
router ospf 1001
 log-adjacency-changes
 area 0 authentication message-digest
 passive-interface default
 no passive-interface Vlan90
 no passive-interface Vlan91
 network 158.236.224.160 0.0.0.15 area 0
!
ip classless"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR001(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR001_should_return_true_when_ospf_is_configured_and_all_enabled_applicable_interfaces_are_configured_with_md5_keys() {
      var blob = new AssetBlob {
        Body = @"!
interface Vlan90
 description <== Router Backbone 1 ==>
 ip address 158.236.224.171 255.255.255.240
 no ip redirects
 no ip proxy-arp
 ip ospf message-digest-key 1 md5 7 052C131B1E7C47051C
!
interface Vlan91
 no ip address
 shutdown
!
!
router ospf 1001
 log-adjacency-changes
 area 0 authentication message-digest
 passive-interface default
 no passive-interface Vlan90
 no passive-interface Vlan91
 network 158.236.224.160 0.0.0.15 area 0
!
ip classless"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR001(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR001_should_return_false_when_ospf_is_configured_and_not_all_enabled_applicable_interfaces_are_configured_with_md5_keys() {
      var blob = new AssetBlob {
        Body = @"!
interface Vlan90
 description <== Router Backbone 1 ==>
 ip address 158.236.224.171 255.255.255.240
 no ip redirects
 no ip proxy-arp
!
!
router ospf 1001
 log-adjacency-changes
 area 0 authentication message-digest
 passive-interface default
 no passive-interface Vlan90
 no passive-interface Vlan91
 network 158.236.224.160 0.0.0.15 area 0
!
ip classless"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR001(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR001_should_ignore_any_interfaces_that_are_not_virtual() {
      var blob = new AssetBlob {
        Body = @"!
interface GigabitEthernet4/13
 description <== U00_BE01_G1/0 ==>
 switchport
 switchport access vlan 90
 switchport mode access
 no ip address
!
interface Vlan90
 description <== Router Backbone 1 ==>
 ip address 10.34.2.1 255.255.255.192
 no ip redirects
 no ip proxy-arp
 ip ospf message-digest-key 1 md5 7 112E0C112822020001
!
router ospf 1001
 log-adjacency-changes
 area 0 authentication message-digest
 redistribute static metric 50 subnets
 passive-interface default
 no passive-interface Vlan90
 no passive-interface Vlan99
 network 10.34.2.0 0.0.0.63 area 0
 default-information originate
!
ip classless"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR001(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}