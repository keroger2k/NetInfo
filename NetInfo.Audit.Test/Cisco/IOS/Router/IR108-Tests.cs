using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR108_Tests {

    public void IR108_should_return_true_when_the_correct_major_version_of_the_odmn_acl_is_applied() {
      AssetBlob blob = new AssetBlob {
        Body = @"
!
interface Vlan23
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 out
!
ip classless
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR108(device, 2);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR108_should_return_false_when_no_interfaces_have_odmn_acl_applied() {
      AssetBlob blob = new AssetBlob {
        Body = @"
!
interface Vlan23
!
ip classless
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR108(device, 2);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR108_should_return_false_if_odmn_interface_is_assign_to_an_unapproved_interface() {
      AssetBlob blob = new AssetBlob {
        Body = @"
!
interface Vlan99
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 out
!
ip classless
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR108(device, 2);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR108_should_return_false_if_acl_is_only_partially_applied() {
      AssetBlob blob = new AssetBlob {
        Body = @"
!
interface Vlan23
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 out
!
ip classless
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR108(device, 2);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR108_should_return_false_if_odmn_interface_is_applied_twice_even_if_it_is_the_correct_version_scenario1() {
      AssetBlob blob = new AssetBlob {
        Body = @"
!
interface Vlan23
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 out
!
interface Vlan99
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 out
!
ip classless
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR108(device, 2);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR108_should_return_false_if_odmn_interface_is_applied_twice_even_if_it_is_the_correct_version_scenario2() {
      AssetBlob blob = new AssetBlob {
        Body = @"
!
interface Vlan23
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V1-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V1-0-0 out
!
interface Vlan99
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 out
!
ip classless
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR108(device, 2);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR108_should_return_false_if_odmn_interface_is_applied_twice_even_if_it_is_the_correct_version_scenario3() {
      AssetBlob blob = new AssetBlob {
        Body = @"
!
interface Vlan23
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 out
!
interface Vlan99
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V1-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V1-0-0 out
!
ip classless
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR108(device, 2);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR108_checks_any_interface_when_the_device_has_an_odmn_device() {
      AssetBlob blob = new AssetBlob {
        Body = @"!
interface GigabitEthernet1/0
 description <== Internal EtherSwitch Service Module G0/26 ==>
 no ip address
 no ip redirects
 no ip unreachables
 no ip proxy-arp
!
interface GigabitEthernet1/0.23
 description <== U_ODMN_TB ==>
 encapsulation dot1Q 23
 ip address 172.24.45.130 255.255.255.248
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 out
 no ip redirects
 no ip unreachables
 no ip proxy-arp
!
interface GigabitEthernet1/0.40
 description <== Public Side IA Transport Boundary ==>
 encapsulation dot1Q 40
 ip address 138.162.13.225 255.255.255.240
 ip access-group OR01_NMCI_UTB_OUT_V2-0-1 in
 no ip redirects
 no ip unreachables
 no ip proxy-arp
 no cdp enable
!
ip classless
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR108(device, 2);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR108_should_return_false_when_the_incorrect_major_version_of_the_odmn_acl_is_applied_in_both_directions() {
      AssetBlob blob = new AssetBlob {
        Body = @"
!
interface Vlan23
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 out
!
ip classless
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR108(device, 3);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR108_should_return_false_when_the_incorrect_major_version_of_the_odmn_acl_is_applied_in_one_direction() {
      AssetBlob blob = new AssetBlob {
        Body = @"
!
interface Vlan23
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V1-0-0 out
!
ip classless
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR108(device, 2);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}