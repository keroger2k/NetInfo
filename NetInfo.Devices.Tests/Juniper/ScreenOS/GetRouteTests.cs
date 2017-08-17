using System.Linq;
using NetInfo.Devices.Juniper.ScreenOS.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class GetRouteTests {

    [Test]
    public void get_route_correctly_parses_routes_example_1() {
      var route = new GetRoute(
        @"C - Connected, S - Static, A - Auto-Exported, I - Imported, R - RIP
iB - IBGP, eB - EBGP, O - OSPF, E1 - OSPF external type 1
E2 - OSPF external type 2
untrust-vr (8 entries)
--------------------------------------------------------------------------------
ID IP-Prefix Interface Gateway P Pref Mtr Vsys
--------------------------------------------------------------------------------
* 2 138.162.0.0/15 n/a trust-vr S 20 0 Root
* 4 10.16.27.32/27 n/a trust-vr S 20 0 Root
* 8 10.29.168.0/23 n/a trust-vr S 20 0 Root
* 6 10.0.16.128/27 n/a trust-vr S 20 0 Root
* 1 10.29.208.16/28 untrust 0.0.0.0 C 0 0 Root
* 7 10.32.9.224/27 n/a trust-vr S 20 0 Root
* 3 10.16.6.0/24 n/a trust-vr S 20 0 Root
* 5 10.0.18.0/24 n/a trust-vr S 20 0 Root
trust-vr (5 entries)
--------------------------------------------------------------------------------
ID IP-Prefix Interface Gateway P Pref Mtr Vsys
--------------------------------------------------------------------------------
* 4 0.0.0.0/0 trust 10.29.199.97 S 20 1 Root
* 1 10.29.199.96/28 trust 0.0.0.0 C 0 0 Root
* 2 10.29.200.24/30 tunnel.1 0.0.0.0 S 20 1 Root
* 3 10.29.208.19/32 tunnel.1 0.0.0.0 S 20 1 Root
* 5 10.29.208.16/28 n/a untrust-vr S 20 0 Root".ToConfig()
      );

      Assert.AreEqual(8, route.Untrusted.Count());
      Assert.AreEqual(5, route.Trusted.Count());
    }

    [Test]
    public void get_route_correctly_parses_routes_example_2() {
      var route = new GetRoute(
        @"get route
H: Host C: Connected S: Static A: Auto-Exported
I: Imported R: RIP P: Permanent D: Auto-Discovered
iB: IBGP eB: EBGP O: OSPF E1: OSPF external type 1
E2: OSPF external type 2

IPv4 Dest-Routes for <untrust-vr> (3 entries)
--------------------------------------------------------------------------------
   ID          IP-Prefix      Interface         Gateway   P Pref    Mtr     Vsys
--------------------------------------------------------------------------------
*   3          0.0.0.0/0         eth0/0    70.91.148.90   S   20      1     Root
*   2    70.91.148.89/32         eth0/0         0.0.0.0   H    0      0     Root
*   1    70.91.148.88/30         eth0/0         0.0.0.0   C    0      0     Root

IPv4 Dest-Routes for <trust-vr> (4 entries)
--------------------------------------------------------------------------------
   ID          IP-Prefix      Interface         Gateway   P Pref    Mtr     Vsys
--------------------------------------------------------------------------------
*   4          0.0.0.0/0            n/a      untrust-vr   S   20      0     Root
*   3      10.46.4.64/26        bgroup0      10.46.4.65   S   20      1     Root
*   2      10.46.4.66/32        bgroup0         0.0.0.0   H    0      0     Root
*   1      10.46.4.64/30        bgroup0         0.0.0.0   C    0      0     Root
".ToConfig()
      );

      Assert.AreEqual(3, route.Untrusted.Count());
      Assert.AreEqual(4, route.Trusted.Count());
    }
  }
}