using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR015_Tests {

    [Test]
    public void IR015_should_return_true_when_all_intefaces_participating_in_eigrp_are_configured_with_a_md5_key() {
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

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR015(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR015_should_return_false_when_not_all_intefaces_participating_in_eigrp_are_configured_with_a_md5_key() {
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
interface Vlan87
 ip address 138.168.64.173 255.255.255.252
 no ip redirects
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

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR015(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR015_should_return_true_when_all_enabled_intefaces_participating_in_eigrp_are_configured_with_a_md5_key() {
      //TODO: Verify with Kathyrn that is correct behavior.
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
interface Vlan87
 ip address 138.168.64.173 255.255.255.252
 ip authentication mode eigrp 1 md5
 ip authentication key-chain eigrp 1 NMCI_EIGRP
!
interface Vlan700
 ip address 138.168.38.17 255.255.255.255
 shutdown
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

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR015(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}