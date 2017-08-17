using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR052_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IR052_should_return_true_when_vlan1_does_not_have_an_ip_address() {
      blob = new AssetBlob {
        Body = @"!
interface GigabitEthernet0/2
 description <== DISABLED ==>
 switchport access vlan 2
 switchport mode access
 no logging event link-status
 shutdown
 no snmp trap link-status
 no cdp enable
 spanning-tree portfast
!
interface Vlan1
 description <== Ver.2.3 VSS ==>
 no ip address
 shutdown
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 10.46.4.65 255.255.255.252
 no ip redirects
 no ip proxy-arp
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR052(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR052_should_return_false_when_vlan1_does_not_have_an_ip_address_but_is_not_shutdown() {
      blob = new AssetBlob {
        Body = @"!
interface GigabitEthernet0/2
 description <== DISABLED ==>
 switchport access vlan 2
 switchport mode access
 no logging event link-status
 shutdown
 no snmp trap link-status
 no cdp enable
 spanning-tree portfast
!
interface Vlan1
 description <== Ver.2.3 VSS ==>
 no ip address
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 10.46.4.65 255.255.255.252
 no ip redirects
 no ip proxy-arp
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR052(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR052_should_return_false_when_vlan1_has_an_ip_address() {
      blob = new AssetBlob {
        Body = @"!
interface GigabitEthernet0/2
 description <== DISABLED ==>
 switchport access vlan 2
 switchport mode access
 no logging event link-status
 shutdown
 no snmp trap link-status
 no cdp enable
 spanning-tree portfast
!
interface Vlan1
 description <== Ver.2.3 VSS ==>
 ip address 1.1.1.1 255.255.255.255
 shutdown
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 10.46.4.65 255.255.255.252
 no ip redirects
 no ip proxy-arp
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR052(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}