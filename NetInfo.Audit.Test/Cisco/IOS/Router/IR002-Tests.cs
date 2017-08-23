using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR002_Tests {

    [Test]
    public void IR002_should_return_true_when_all_bgp_neighbors_have_passwords_configured() {
      var blob = new AssetBlob {
        Body = @"!
router bgp 65000
 no synchronization
 bgp log-neighbor-changes
 network 138.163.128.0 mask 255.255.252.0
 network 138.163.132.0 mask 255.255.255.0
 network 138.163.132.48 mask 255.255.255.240
 network 138.163.146.0 mask 255.255.255.0
 neighbor 138.163.132.6 remote-as 65000
 neighbor 138.163.132.6 description <== iBGP to U00_OR02 ==>
 neighbor 138.163.132.6 password 7 02310C62342D5E2A1D
 neighbor 138.163.132.6 next-hop-self
 neighbor 214.40.148.53 remote-as 27066
 neighbor 214.40.148.53 description <== DISA Primary eBGP ==>
 neighbor 214.40.148.53 password 7 132634212F413C011C1C
 neighbor 214.40.148.53 allowas-in 5
 neighbor 214.40.148.53 soft-reconfiguration inbound
 neighbor 214.40.148.53 prefix-list PRLH-PREFIXES out
 neighbor 214.40.148.53 route-map LOC_DISA1 in
 neighbor 214.40.148.53 filter-list 10 out
 no auto-summary
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0408(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR002_should_return_false_when_not_all_bgp_neighbors_have_passwords_configured() {
      var blob = new AssetBlob {
        Body = @"!
router bgp 65000
 no synchronization
 bgp log-neighbor-changes
 network 138.163.128.0 mask 255.255.252.0
 network 138.163.132.0 mask 255.255.255.0
 network 138.163.132.48 mask 255.255.255.240
 network 138.163.146.0 mask 255.255.255.0
 neighbor 138.163.132.6 remote-as 65000
 neighbor 138.163.132.6 description <== iBGP to U00_OR02 ==>
 neighbor 138.163.132.6 password 7 02310C62342D5E2A1D
 neighbor 138.163.132.6 next-hop-self
 neighbor 214.40.148.53 remote-as 27066
 neighbor 214.40.148.53 description <== DISA Primary eBGP ==>
 neighbor 214.40.148.53 allowas-in 5
 neighbor 214.40.148.53 soft-reconfiguration inbound
 neighbor 214.40.148.53 prefix-list PRLH-PREFIXES out
 neighbor 214.40.148.53 route-map LOC_DISA1 in
 neighbor 214.40.148.53 filter-list 10 out
 no auto-summary
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0408(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR002_should_return_true_when_remote_as_701_is_not_configured_with_a_password() {
      var blob = new AssetBlob {
        Body = @"!
router bgp 65000
 no synchronization
 bgp log-neighbor-changes
 network 138.163.128.0 mask 255.255.252.0
 network 138.163.132.0 mask 255.255.255.0
 network 138.163.132.48 mask 255.255.255.240
 network 138.163.146.0 mask 255.255.255.0
 neighbor 138.163.132.6 remote-as 65000
 neighbor 138.163.132.6 description <== iBGP to U00_OR02 ==>
 neighbor 138.163.132.6 password 7 02310C62342D5E2A1D
 neighbor 138.163.132.6 next-hop-self
 neighbor 214.40.148.53 remote-as 701
 neighbor 214.40.148.53 description <== DISA Primary eBGP ==>
 neighbor 214.40.148.53 soft-reconfiguration inbound
 neighbor 214.40.148.53 prefix-list PRLH-PREFIXES out
 neighbor 214.40.148.53 route-map LOC_DISA1 in
 neighbor 214.40.148.53 filter-list 10 out
 no auto-summary
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0408(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR002_should_return_true_when_peer_groups_are_used_and_a_password_is_correctly_configured() {
      var blob = new AssetBlob {
        Body = @"!
!
router bgp 65204
 no synchronization
 bgp log-neighbor-changes
 neighbor VPN_MESH_eBGP_PEERS peer-group
 neighbor VPN_MESH_eBGP_PEERS remote-as 65012
 neighbor VPN_MESH_eBGP_PEERS password 7 0329084F0E30161C5C314D2B1A314A5E
 neighbor VPN_MESH_eBGP_PEERS ebgp-multihop 3
 neighbor 10.8.64.138 peer-group VPN_MESH_eBGP_PEERS
 neighbor 10.8.64.138 description NAWEBREMVP00
 neighbor 10.8.64.139 peer-group VPN_MESH_eBGP_PEERS
 neighbor 10.8.64.139 description NAWEBREMVP01
 neighbor 172.31.128.1 peer-group VPN_MESH_eBGP_PEERS
 neighbor 172.31.128.1 description <== NAEANRFKVP00 ==>
 neighbor 172.31.128.2 peer-group VPN_MESH_eBGP_PEERS
 neighbor 172.31.128.2 description <== NAEANRFKVP01 ==>
 no auto-summary
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0408(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR002_should_return_false_when_peer_groups_are_used_and_a_password_is_not_correctly_configured() {
      var blob = new AssetBlob {
        Body = @"!
!
router bgp 65204
 no synchronization
 bgp log-neighbor-changes
 neighbor VPN_MESH_eBGP_PEERS peer-group
 neighbor VPN_MESH_eBGP_PEERS remote-as 65012
 neighbor VPN_MESH_eBGP_PEERS ebgp-multihop 3
 neighbor 10.8.64.138 peer-group VPN_MESH_eBGP_PEERS
 neighbor 10.8.64.138 description NAWEBREMVP00
 neighbor 10.8.64.139 peer-group VPN_MESH_eBGP_PEERS
 neighbor 10.8.64.139 description NAWEBREMVP01
 neighbor 172.31.128.1 peer-group VPN_MESH_eBGP_PEERS
 neighbor 172.31.128.1 description <== NAEANRFKVP00 ==>
 neighbor 172.31.128.2 peer-group VPN_MESH_eBGP_PEERS
 neighbor 172.31.128.2 description <== NAEANRFKVP01 ==>
 no auto-summary
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0408(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}