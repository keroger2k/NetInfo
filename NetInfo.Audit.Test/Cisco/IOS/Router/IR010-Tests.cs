using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR010_Tests {

    [Test]
    public void IR010_should_return_true_when_there_is_either_a_deny_ip_any_any_or_a_permit_ip_any_any_extended() {
      var blob = new AssetBlob {
        Body = @"ip access-list extended NMCI_Printer_VLAN_ACL_IN_V17-2-0
 permit udp any gt 1023 host 239.255.255.250 eq 1900
 permit   ip any any log
ip access-list extended NMCI_Printer_VLAN_ACL_OUT_V17-2-0
 permit udp any eq ntp any gt 1023
 deny   ip any any log
!
!
interface Vlan1
 no ip address
 shutdown
!
interface Vlan98
 ip access-group NMCI_Printer_VLAN_ACL_OUT_V17-2-0 in
!
interface Vlan99
 ip access-group NMCI_Printer_VLAN_ACL_IN_V17-2-0 in
!
logging trap notifications
no cdp run
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1020(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR010_should_return_true_when_there_is_either_a_deny_any_or_a_permit_any_standard() {
      var blob = new AssetBlob {
        Body = @"
!
logging trap notifications
logging source-interface Vlan30
logging 10.16.27.43
access-list 69 remark Norfolk NOC
access-list 69 permit 10.28.249.0 0.0.0.255
access-list 69 deny   any log
access-list 99 remark Norfolk NOC
access-list 99 permit 10.0.11.0 0.0.0.255
access-list 99 deny   any log
no cdp run
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1020(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR010_should_return_false_when_there_is_no_deny_ip_any_any_log_at_the_end_extended() {
      var blob = new AssetBlob {
        Body = @"ip access-list extended NMCI_Printer_VLAN_ACL_IN_V17-2-0
 permit udp any gt 1023 host 239.255.255.250 eq 1900
 permit   ip any any log
ip access-list extended NMCI_Printer_VLAN_ACL_OUT_V17-2-0
 permit udp any eq ntp any gt 1023
!
no cdp run
!
interface Vlan1
 no ip address
 shutdown
!
interface Vlan98
 ip access-group NMCI_Printer_VLAN_ACL_OUT_V17-2-0 in
!
interface Vlan99
 ip access-group NMCI_Printer_VLAN_ACL_IN_V17-2-0 in
!

logging trap notifications
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1020(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR010_should_return_false_when_there_is_no_deny_ip_any_any_log_at_the_end_standard() {
      var blob = new AssetBlob {
        Body = @"!
logging trap notifications
logging source-interface Vlan30
logging 10.16.27.43
access-list 69 remark Norfolk NOC
access-list 69 permit 10.28.249.0 0.0.0.255
access-list 69 deny   any log
access-list 99 remark Norfolk NOC
access-list 99 permit 10.0.11.0 0.0.0.255
no cdp run
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1020(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR010_should_return_false_when_there_is_no_deny_ip_any_any_log_at_the_end_both_fail() {
      var blob = new AssetBlob {
        Body = @"ip access-list extended NMCI_Printer_VLAN_ACL_IN_V17-2-0
 permit udp any gt 1023 host 239.255.255.250 eq 1900
 permit   ip any any log
ip access-list extended NMCI_Printer_VLAN_ACL_OUT_V17-2-0
 permit udp any eq ntp any gt 1023
!
!
interface Vlan1
 no ip address
 shutdown
!
interface Vlan98
 ip access-group NMCI_Printer_VLAN_ACL_OUT_V17-2-0 in
!
interface Vlan99
 ip access-group NMCI_Printer_VLAN_ACL_IN_V17-2-0 in
!
logging trap notifications
logging source-interface Vlan30
logging 10.16.27.43
access-list 69 remark Norfolk NOC
access-list 69 permit 10.28.249.0 0.0.0.255
access-list 69 deny   any log
access-list 99 remark Norfolk NOC
access-list 99 permit 10.0.11.0 0.0.0.255
no cdp run
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1020(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR010_should_return_false_when_there_is_no_deny_ip_any_any_log_at_the_end_extended_pass_standard_fail() {
      var blob = new AssetBlob {
        Body = @"ip access-list extended NMCI_Printer_VLAN_ACL_IN_V17-2-0
 permit udp any gt 1023 host 239.255.255.250 eq 1900
 permit   ip any any log
ip access-list extended NMCI_Printer_VLAN_ACL_OUT_V17-2-0
 permit udp any eq ntp any gt 1023
 deny ip any any log
!
!
interface Vlan1
 no ip address
 shutdown
!
interface Vlan98
 ip access-group NMCI_Printer_VLAN_ACL_OUT_V17-2-0 in
!
interface Vlan99
 ip access-group NMCI_Printer_VLAN_ACL_IN_V17-2-0 in
!
logging trap notifications
logging source-interface Vlan30
logging 10.16.27.43
access-list 69 remark Norfolk NOC
access-list 69 permit 10.28.249.0 0.0.0.255
access-list 69 deny   any log
access-list 99 remark Norfolk NOC
access-list 99 permit 10.0.11.0 0.0.0.255
no cdp run
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1020(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR010_should_return_false_when_there_is_no_deny_ip_any_any_log_at_the_end_standard_pass_extended_fail() {
      var blob = new AssetBlob {
        Body = @"ip access-list extended NMCI_Printer_VLAN_ACL_IN_V17-2-0
 permit udp any gt 1023 host 239.255.255.250 eq 1900
 permit   ip any any log
ip access-list extended NMCI_Printer_VLAN_ACL_OUT_V17-2-0
 permit udp any eq ntp any gt 1023
!
!
interface Vlan1
 no ip address
 shutdown
!
interface Vlan98
 ip access-group NMCI_Printer_VLAN_ACL_OUT_V17-2-0 in
!
interface Vlan99
 ip access-group NMCI_Printer_VLAN_ACL_IN_V17-2-0 in
!
logging trap notifications
logging source-interface Vlan30
logging 10.16.27.43
access-list 69 remark Norfolk NOC
access-list 69 permit 10.28.249.0 0.0.0.255
access-list 69 deny   any log
access-list 99 remark Norfolk NOC
access-list 99 permit 10.0.11.0 0.0.0.255
access-list 99 deny   any log
no cdp run
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1020(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR010_should_return_true_when_existing_non_compliant_acl_is_only_used_for_route_maps() {
      var blob = new AssetBlob {
        Body = @"
!
hostname ALBY-U00-IR-01
!
interface Vlan51
 description <== U_ODMN_B2 ==>
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 out
!
ip nat pool Legacy_1_Pool 164.221.61.3 164.221.61.3 prefix-length 24
ip nat inside source route-map Legacy_1_Pool pool Legacy_1_Pool overload
!
access-list 68 remark Norfolk NOC
access-list 68 permit 172.18.1.0 0.0.0.31
access-list 68 deny   any log
access-list 98 remark Norfolk NOC
access-list 98 permit 172.18.1.0 0.0.0.31 log
access-list 98 deny   any log
access-list 101 permit ip 10.0.0.0 0.255.255.255 10.8.133.0 0.0.0.255
access-list 101 permit ip 10.0.0.0 0.255.255.255 10.8.134.128 0.0.0.127
!
route-map Legacy_1_Pool permit 10
 match ip address 101
!
ip access-list extended ODMN_ONLY_TRAFFIC_ACL_V2-0-0
 permit ip 172.16.0.0 0.15.255.255 172.16.0.0 0.15.255.255
 deny   ip any any log
!
line con 0
 exec-timeout 3 0
line vty 0 4
 access-class 98 in
 exec-timeout 3 0
 transport input ssh
line vty 5 15
 exec-timeout 3 0
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1020(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR010_should_return_true_when_route_map_is_used_but_no_acls_are_attached() {
      var blob = new AssetBlob {
        Body = @"
!
hostname ALBY-U00-IR-01
!
interface Vlan51
 description <== U_ODMN_B2 ==>
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 out
!
ip nat pool Legacy_1_Pool 164.221.61.3 164.221.61.3 prefix-length 24
ip nat inside source route-map Legacy_1_Pool pool Legacy_1_Pool overload
!
access-list 68 remark Norfolk NOC
access-list 68 permit 172.18.1.0 0.0.0.31
access-list 68 deny   any log
access-list 98 remark Norfolk NOC
access-list 98 permit 172.18.1.0 0.0.0.31 log
access-list 98 deny   any log
!
route-map Legacy_1_Pool permit 10
 match tag 6
 match route-type external type-2
!
ip access-list extended ODMN_ONLY_TRAFFIC_ACL_V2-0-0
 permit ip 172.16.0.0 0.15.255.255 172.16.0.0 0.15.255.255
 deny   ip any any log
!
line con 0
 exec-timeout 3 0
line vty 0 4
 access-class 98 in
 exec-timeout 3 0
 transport input ssh
line vty 5 15
 exec-timeout 3 0
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1020(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}