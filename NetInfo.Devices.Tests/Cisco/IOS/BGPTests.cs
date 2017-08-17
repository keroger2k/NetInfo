using System.Linq;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class BGPTests {

    [Test]
    public void bgp_should_be_able_to_parse_its_autonomous_system_number() {
      var bgp = new BorderGatewayProtocol();
      bgp.Settings = @"router bgp 616
 bgp log-neighbor-changes
".ToConfig();

      Assert.AreEqual(616, bgp.ASN);
    }

    [Test]
    public void bgp_can_correctly_determine_the_number_of_unqiue_neighbors() {
      var bgp = new BorderGatewayProtocol();
      bgp.Settings = @"router bgp 65000
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
".ToConfig();

      Assert.AreEqual(2, bgp.Neighbors.Count());
    }

    [Test]
    public void bgp_can_correctly_determine_the_password_for_each_neighbor() {
      var bgp = new BorderGatewayProtocol();
      bgp.Settings = @"router bgp 65000
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
".ToConfig();

      Assert.AreEqual("02310C62342D5E2A1D", bgp.Neighbors.ElementAt(0).Password);
      Assert.AreEqual("132634212F413C011C1C", bgp.Neighbors.ElementAt(1).Password);
    }
  }
}