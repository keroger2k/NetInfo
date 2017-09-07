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

        [Test]
        public void should_correctly_get_a_list_of_routes()
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
      10.0.0.0/8 is variably subnetted, 33 subnets, 3 masks
O        10.1.1.0/24 [110/160] via 172.18.3.153, 5d18h, GigabitEthernet0/1/0
O        10.2.1.0/24 [110/1155] via 172.18.3.153, 5d18h, GigabitEthernet0/1/0".ToConfig());

            Assert.AreEqual(2, sv.RouteTableNetorks.Count());
            Assert.AreEqual("O", sv.RouteTableNetorks.ElementAt(0).RouteCode);
            Assert.AreEqual("172.18.3.153", sv.RouteTableNetorks.ElementAt(0).NextHops.ElementAt(0).NextHop.ToString());

        }

        [Test]
        public void should_correctly_parse_route_where_next_hop_and_interface_are_on_same_line()
        {
            var sv = new ShowIpRoute(@"Codes: L - local, C - connected, S - static, R - RIP, M - mobile, B - BGP
      D - EIGRP, EX - EIGRP external, O - OSPF, IA - OSPF inter area 
      N1 - OSPF NSSA external type 1, N2 - OSPF NSSA external type 2
      E1 - OSPF external type 1, E2 - OSPF external type 2
      i - IS-IS, su - IS-IS summary, L1 - IS-IS level-1, L2 - IS-IS level-2
      ia - IS-IS inter area, * - candidate default, U - per-user static route
      o - ODR, P - periodic downloaded static route, H - NHRP, l - LISP
      + - replicated route, % - next hop override
Gateway of last resort is not set
     10.0.0.0/8 is variably subnetted, 33 subnets, 3 masks
O        10.1.1.0/24 [110/160] via 172.18.3.153, 5d18h, GigabitEthernet0/1/0
O        10.2.1.0/24 [110/1155] via 172.18.3.153, 5d18h, GigabitEthernet0/1/0".ToConfig());

            Assert.AreEqual(2, sv.RouteTableNetorks.Count());
            Assert.AreEqual(1, sv.RouteTableNetorks.ElementAt(0).NextHops.Count());

        }

        [Test]
        public void should_correctly_parse_route_where_next_hop_and_interface_are_on_same_line_with_multiple_next_hops()
        {
            var sv = new ShowIpRoute(@"Codes: L - local, C - connected, S - static, R - RIP, M - mobile, B - BGP
      D - EIGRP, EX - EIGRP external, O - OSPF, IA - OSPF inter area 
      N1 - OSPF NSSA external type 1, N2 - OSPF NSSA external type 2
      E1 - OSPF external type 1, E2 - OSPF external type 2
      i - IS-IS, su - IS-IS summary, L1 - IS-IS level-1, L2 - IS-IS level-2
      ia - IS-IS inter area, * - candidate default, U - per-user static route
      o - ODR, P - periodic downloaded static route, H - NHRP, l - LISP
      + - replicated route, % - next hop override
Gateway of last resort is not set
     10.0.0.0/8 is variably subnetted, 33 subnets, 3 masks
O        10.99.1.0/24 [110/61] via 172.18.3.205, 5d18h, GigabitEthernet0/0/0
                    [110/61] via 172.18.3.201, 5d18h, GigabitEthernet0/0
O        10.99.1.0/24 [110/1056] via 172.18.3.205, 5d18h, GigabitEthernet0/0/0
                    [110/1056] via 172.18.3.201, 5d18h, GigabitEthernet0/0".ToConfig());

            Assert.AreEqual(2, sv.RouteTableNetorks.Count());
            Assert.AreEqual(2, sv.RouteTableNetorks.ElementAt(0).NextHops.Count());
        }

        [Test]
        public void should_correctly_parse_route_where_next_hop_and_interface_are_not_on_same_line_with_multiple_next_hops()
        {
            var sv = new ShowIpRoute(@"Codes: L - local, C - connected, S - static, R - RIP, M - mobile, B - BGP
      D - EIGRP, EX - EIGRP external, O - OSPF, IA - OSPF inter area 
      N1 - OSPF NSSA external type 1, N2 - OSPF NSSA external type 2
      E1 - OSPF external type 1, E2 - OSPF external type 2
      i - IS-IS, su - IS-IS summary, L1 - IS-IS level-1, L2 - IS-IS level-2
      ia - IS-IS inter area, * - candidate default, U - per-user static route
      o - ODR, P - periodic downloaded static route, H - NHRP, l - LISP
      + - replicated route, % - next hop override
Gateway of last resort is not set
     10.0.0.0/8 is variably subnetted, 33 subnets, 3 masks
O IA     10.29.14.0/24 
          [110/1054] via 172.18.3.205, 22:27:35, GigabitEthernet0/0/0
          [110/1054] via 172.18.3.201, 22:27:35, GigabitEthernet0/0
O IA     10.29.100.40/32 
          [110/1053] via 172.18.3.205, 00:18:09, GigabitEthernet0/0/0
          [110/1053] via 172.18.3.201, 00:18:09, GigabitEthernet0/0".ToConfig());

            Assert.AreEqual(2, sv.RouteTableNetorks.Count());
            Assert.AreEqual(2, sv.RouteTableNetorks.ElementAt(0).NextHops.Count());
        }

        [Test]
        public void should_correctly_parse_route_where_next_hop_and_interface_are_not_on_same_line()
        {
            var sv = new ShowIpRoute(@"Codes: L - local, C - connected, S - static, R - RIP, M - mobile, B - BGP
      D - EIGRP, EX - EIGRP external, O - OSPF, IA - OSPF inter area 
      N1 - OSPF NSSA external type 1, N2 - OSPF NSSA external type 2
      E1 - OSPF external type 1, E2 - OSPF external type 2
      i - IS-IS, su - IS-IS summary, L1 - IS-IS level-1, L2 - IS-IS level-2
      ia - IS-IS inter area, * - candidate default, U - per-user static route
      o - ODR, P - periodic downloaded static route, H - NHRP, l - LISP
      + - replicated route, % - next hop override
Gateway of last resort is not set
     10.0.0.0/8 is variably subnetted, 33 subnets, 3 masks
O IA     10.29.14.0/24 
           [110/1153] via 172.18.3.153, 22:27:09, GigabitEthernet0/1/0
O IA     10.29.15.0/24 
           [110/1153] via 172.18.3.153, 22:27:09, GigabitEthernet0/1/0".ToConfig());

            Assert.AreEqual(2, sv.RouteTableNetorks.Count());
            Assert.AreEqual(1, sv.RouteTableNetorks.ElementAt(0).NextHops.Count());
        }


    }
}