using System.Linq;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class ShowIpRouteTests {

        [Test]
        public void should_correctly_identify_when_no_default_gateway_has_been_set()
        {
            var sv = new ShowIpRoute(@"Codes: L - local, C - connected, S - static, R - RIP, M - mobile, B - BGP
       D - EIGRP, EX - EIGRP external, O - OSPF, IA - OSPF inter area 
       N1 - OSPF NSSA external type 1, N2 - OSPF NSSA external type 2
       E1 - OSPF external type 1, E2 - OSPF external type 2
       i - IS-IS, su - IS-IS summary, L1 - IS-IS level-1, L2 - IS-IS level-2
       ia - IS-IS inter area, * - candidate default, U - per-user static route
       o - ODR, P - periodic downloaded static route, H - NHRP, l - LISP
       a - application route
       + - replicated route, % - next hop override
Gateway of last resort is not set
      10.0.0.0/8 is variably subnetted, 34 subnets, 3 masks
O        10.1.1.0/24 [110/19] via 172.16.136.8, 1w3d, TenGigabitEthernet1/10
C        10.1.1.101/32 is directly connected, Loopback10
O        10.2.1.0/24 [110/1014] via 172.16.136.8, 1w3d, TenGigabitEthernet1/10
O IA     10.29.14.0/24 
           [110/1002] via 172.16.253.34, 22:26:04, TenGigabitEthernet1/12
O IA     10.29.100.40/32".ToConfig());

            Assert.AreEqual(null, sv.GatewayLastResort);
         
        }

        [Test]
        public void should_correctly_identify_gateway_of_last_resort()
        {
            var sv = new ShowIpRoute(@"Codes: L - local, C - connected, S - static, R - RIP, M - mobile, B - BGP
       D - EIGRP, EX - EIGRP external, O - OSPF, IA - OSPF inter area 
       N1 - OSPF NSSA external type 1, N2 - OSPF NSSA external type 2
       E1 - OSPF external type 1, E2 - OSPF external type 2
       i - IS-IS, su - IS-IS summary, L1 - IS-IS level-1, L2 - IS-IS level-2
       ia - IS-IS inter area, * - candidate default, U - per-user static route
       o - ODR, P - periodic downloaded static route, H - NHRP, l - LISP
       + - replicated route, % - next hop override
Gateway of last resort is 172.16.215.149 to network 0.0.0.0
S*    0.0.0.0/0 [1/0] via 172.16.215.149
      10.0.0.0/8 is variably subnetted, 33 subnets, 3 masks".ToConfig());

            Assert.AreEqual("0.0.0.0", sv.GatewayLastResort.Network.ToString());
            Assert.AreEqual("172.16.215.149", sv.GatewayLastResort.NextHop.ToString());

        }

    }
}