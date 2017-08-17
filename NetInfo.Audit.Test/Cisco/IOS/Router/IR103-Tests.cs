using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR103_Tests {

    [Test]
    public void IR103_should_return_true_when_all_applied_access_lists_are_actual_configured_access_lists() {
      var blob = new AssetBlob {
        Body = @"!
interface Vlan1
 ip access-group NMCI_Printer_VLAN_ACL_IN_V17-2-0 in
 ip access-group NMCI_Printer_VLAN_ACL_OUT_V17-2-0 out
!
ip access-list extended NMCI_Printer_VLAN_ACL_IN_V17-2-0
 permit udp any gt 1023 host 239.255.255.250 eq 1900
 deny   ip any any log
ip access-list extended NMCI_Printer_VLAN_ACL_OUT_V17-2-0
 permit udp any eq ntp any gt 1023
 deny   ip any any log
!
logging trap notifications
logging source-interface Vlan30
logging 10.16.27.43
access-list 69 remark Norfolk NOC
access-list 69 permit 10.16.27.32 0.0.0.31
access-list 69 deny   any log
access-list 99 remark Norfolk NOC
access-list 99 permit 10.0.11.0 0.0.0.255
access-list 99 deny   any log
no cdp run
snmp-server engineID remote 10.0.16.134 8000000B7F80E3DBEA515C41E69D2F0022F21D94FA
snmp-server group nmcigroup v3 priv write NMS notify *tv.FFFFFFFF.FFFFFFFF.FFFFFFFF.FFFFFFFF7F access 69
!
line con 0
 exec-timeout 3 0
 password 7 0231211A095537394E0A19411C1F1412
line vty 0 4
 access-class 99 in
 exec-timeout 3 0
 password 7 0231211A095537394E0A19411C1F1412
 transport input ssh
line vty 5 15
 exec-timeout 3 0
 transport input none
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR103(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR103_should_return_false_when_not_all_applied_access_lists_are_actual_configured_access_lists() {
      var blob = new AssetBlob {
        Body = @"!
interface Vlan1
 ip access-group NMCI_Printer_VLAN_ACL_IN_V17-2-0 in
 ip access-group NMCI_Printer_VLAN_ACL_OUT_V17-2-0 out
!
ip access-list extended NMCI_Printer_VLAN_ACL_IN_V17-2-0
 permit udp any gt 1023 host 239.255.255.250 eq 1900
 deny   ip any any log
!
logging trap notifications
logging source-interface Vlan30
logging 10.16.27.43
access-list 69 remark Norfolk NOC
access-list 69 permit 10.16.27.32 0.0.0.31
access-list 69 deny   any log
access-list 99 remark Norfolk NOC
access-list 99 permit 10.0.11.0 0.0.0.255
access-list 99 deny   any log
no cdp run
snmp-server engineID remote 10.0.16.134 8000000B7F80E3DBEA515C41E69D2F0022F21D94FA
snmp-server group nmcigroup v3 priv write NMS notify *tv.FFFFFFFF.FFFFFFFF.FFFFFFFF.FFFFFFFF7F access 69
!
line con 0
 exec-timeout 3 0
 password 7 0231211A095537394E0A19411C1F1412
line vty 0 4
 access-class 99 in
 exec-timeout 3 0
 password 7 0231211A095537394E0A19411C1F1412
 transport input ssh
line vty 5 15
 exec-timeout 3 0
 transport input none
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR103(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}