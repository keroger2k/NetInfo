using System.Linq;
using NetInfo.Devices.Infrastructure.ExtensionMethods;
using NetInfo.Devices.IOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class DeviceExtensionMethodsTests {

    [Test]
    public void extensionmethod_should_return_all_interfaces_that_partcipate_in_eigrp_vlan87_and_vlan700() {
      var blob = new AssetBlob {
        Body = @"!
interface Loopback0
 description <== OSPF Router ID ==>
 ip address 138.168.66.193 255.255.255.255
!
interface Port-channel112
 no ip address
 logging event link-status
!
interface Vlan1
 no ip address
 shutdown
!
interface Vlan86
 ip address 192.168.0.0 255.255.255.252
 ip authentication mode eigrp 1 md5
 ip authentication key-chain eigrp 1 NMCI_EIGRP
!
interface Vlan87
 ip address 138.168.64.173 255.255.255.252
 ip authentication mode eigrp 1 md5
 ip authentication key-chain eigrp 1 NMCI_EIGRP
!
interface Vlan700
 ip address 138.168.38.17 255.255.255.252
 ip authentication mode eigrp 1 md5
 ip authentication key-chain eigrp 1 NMCI_EIGRP
!
router eigrp 1
 redistribute static metric 10000 100 255 1 1486 route-map CRTY_CIDR
 redistribute ospf 1001 metric 10000 100 255 1 1486 route-map CRTY_CIDR
 network 138.168.38.16 0.0.0.3
 network 138.168.64.172 0.0.0.3
 distance eigrp 90 105
 no auto-summary
!"
      };

      IIOSDevice device = new IOSDevice(blob);

      Assert.AreEqual(2, device.GetCoveredInterfaces().Count());
    }

    [Test]
    public void not_for_sure_this_situation_is_even_possible_but_want_to_ensure_get_back_distinct_and_not_duplicate_interfaces() {
      var blob = new AssetBlob {
        Body = @"!
interface Loopback0
 description <== OSPF Router ID ==>
 ip address 138.168.66.193 255.255.255.255
!
interface Port-channel112
 no ip address
 logging event link-status
!
interface Vlan1
 no ip address
 shutdown
!
interface Vlan86
 ip address 192.168.0.0 255.255.255.252
 ip authentication mode eigrp 1 md5
 ip authentication key-chain eigrp 1 NMCI_EIGRP
!
interface Vlan87
 ip address 138.168.64.173 255.255.255.252
 ip authentication mode eigrp 1 md5
 ip authentication key-chain eigrp 1 NMCI_EIGRP
!
interface Vlan700
 ip address 138.168.38.17 255.255.255.252
 ip authentication mode eigrp 1 md5
 ip authentication key-chain eigrp 1 NMCI_EIGRP
!
router eigrp 1
 redistribute static metric 10000 100 255 1 1486 route-map CRTY_CIDR
 redistribute ospf 1001 metric 10000 100 255 1 1486 route-map CRTY_CIDR
 network 138.168.38.16 0.0.0.7
 network 138.168.38.16 0.0.0.3
 network 138.168.64.172 0.0.0.3
 distance eigrp 90 105
 no auto-summary
!"
      };

      IIOSDevice device = new IOSDevice(blob);

      Assert.AreEqual(2, device.GetCoveredInterfaces().Count());
    }
  }
}