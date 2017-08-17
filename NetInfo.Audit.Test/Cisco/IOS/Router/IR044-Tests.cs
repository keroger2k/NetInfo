using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR044_Tests {

    [Test]
    public void IR044_should_return_true_when_vlan30_is_return_when_no_loopback_or_vlan99_interface_exist() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ABCD-U00-IR-01
!
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 1.1.1.1 255.255.255.255
!
snmp-server trap-source Vlan30
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR044(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR044_should_return_true_when_loopback_is_tacacs_source_and_device_has_loopback_interface_configured() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ABCD-U00-IR-01
!
!
interface Loopback0
 description <== OSPF Router ID ==>
 ip address 1.1.1.1 255.255.255.255
!
interface Vlan99
 description <== Management VLAN ==>
 ip address 1.1.1.1 255.255.255.255
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 1.1.1.1 255.255.255.255
!
snmp-server trap-source Loopback0
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR044(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR044_should_return_true_when_vlan99_is_tacacs_source_and_device_has_no_loopback_interface_configured() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ABCD-U00-IR-01
!
!
interface Vlan99
 description <== Management VLAN ==>
 ip address 1.1.1.1 255.255.255.255
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 1.1.1.1 255.255.255.255
!
snmp-server trap-source Vlan99
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR044(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR044_should_return_true_when_vlan30_is_tacacs_source_and_device_has_no_loopback_interface_and_vlan99_is_shutdown() {
      var blob = new AssetBlob {
        Body = @"
!
hostname ABCD-U00-IR-01
!
!
!
interface Vlan99
 description <== Management VLAN ==>
 shutdown
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 1.1.1.1 255.255.255.255
!
snmp-server trap-source Vlan30
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR044(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR044_should_return_false_when_logging_source_interface_is_not_found() {
      var blob = new AssetBlob {
        Body = @"
!
hostname ABCD-U00-IR-01
!
!
interface Loopback0
 description <== OSPF Router ID ==>
 ip address 1.1.1.1 255.255.255.255
!
interface Vlan99
 description <== Management VLAN ==>
 shutdown
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 1.1.1.1 255.255.255.255
!
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR044(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void prod1() {
      var blob = new AssetBlob {
        Body = @"!
hostname ALBY-UC1-CR-01
!
!
interface GigabitEthernet0/0
 description <== U03_AS01_F2/4 ==>
 no ip address
 no ip redirects
 no ip proxy-arp
 duplex full
 speed 100
!
interface GigabitEthernet0/0.99
 description <== NMCI Management VLAN ==>
 encapsulation dot1Q 99
 no ip redirects
 no ip proxy-arp
 bridge-group 99
!
interface GigabitEthernet0/0.929
 description <== Connection to uBAN ==>
 encapsulation dot1Q 929
 ip address 158.236.247.84 255.255.255.248
 no ip redirects
 no ip proxy-arp
!
interface GigabitEthernet0/1
 description <== UC1_AS01_F3/1 ==>
 no ip address
 no ip redirects
 no ip proxy-arp
 duplex full
 speed 100
!
interface GigabitEthernet0/1.99
 description <== NMCI Management VLAN ==>
 encapsulation dot1Q 99
 no ip redirects
 no ip proxy-arp
 bridge-group 99
!
interface GigabitEthernet0/1.930
 description <== SDP COI ==>
 encapsulation dot1Q 930
 ip address 158.236.0.1 255.255.255.192
 ip access-group uB3-COI_Inbound_v1-0-0 in
 ip access-group uB3-COI_Outbound_v1-0-0 out
 no ip redirects
 no ip proxy-arp
!
ip forward-protocol nd
no ip http server
no ip http secure-server
!
!
ip route 0.0.0.0 0.0.0.0 158.236.247.81
ip tacacs source-interface GigabitEthernet0/0.929
!
ip access-list extended uB3-COI_Outbound_v1-0-0
 remark *THIS ACL IS TO BE APPLIED TO THE SITE COI EDGE ROUTER OUTBOUND*
 remark
 remark ###Section 1 - Standard VLAN setup###
 remark PERMIT ESTABLISHED
 permit tcp any any established
 remark
 remark - Allow COI INTERNAL VLAN COMMUNICATION
 permit ip 158.236.0.0 0.0.0.63 any
 remark
 remark ###Section 2 - Standard Services###
 remark - Allow ICMP access and type codes to local COI
 permit icmp any any host-unreachable
 permit icmp any any port-unreachable
 permit icmp any any packet-too-big
 remark
 remark ###Section 3 - COI Server Specifics###
 remark PERMIT SERVICES TO CLIN27 SERVERS
 permit tcp any host 158.236.0.4 eq 443
 permit udp any eq domain host 158.236.0.4
 remark
 remark DENY ALL OTHER TRAFFIC
 deny   ip any any log
 remark DENY ALL OTHER TRAFFIC
 remark
!
logging trap notifications
logging source-interface GigabitEthernet0/0.929
logging 138.156.125.236
logging 205.110.245.151
!
snmp-server trap-source GigabitEthernet0/0.929
snmp-server source-interface informs GigabitEthernet0/0.929
access-list 69 remark FSTR NNMI NMS"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR044(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}