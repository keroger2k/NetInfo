using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP083_Tests {

    [Test]
    public void VP083_should_return_true_for_juniper_that_has_ntp_server_set_as_gateway_in_route_table() {
      var blob = new AssetBlob {
        Body = @"
set ntp server ""138.168.52.1""
MCJPFSTRFWZ00->
MCJPFSTRFWZ00-> get route

IPv4 Dest-Routes for <untrust-vr> (0 entries)
--------------------------------------------------------------------------------
H: Host C: Connected S: Static A: Auto-Exported
I: Imported R: RIP P: Permanent D: Auto-Discovered
iB: IBGP eB: EBGP O: OSPF E1: OSPF external type 1
E2: OSPF external type 2

IPv4 Dest-Routes for <trust-vr> (5 entries)
--------------------------------------------------------------------------------
   ID          IP-Prefix      Interface         Gateway   P Pref    Mtr     Vsys
--------------------------------------------------------------------------------
*   5          0.0.0.0/0         eth0/0    138.168.52.1   S   20      1     Root
*   2   138.168.52.14/32         eth0/0         0.0.0.0   H    0      0     Root
    3   138.168.181.0/27        bgroup0         0.0.0.0   C    0      0     Root
    4   138.168.181.1/32        bgroup0         0.0.0.0   H    0      0     Root
*   1    138.168.52.0/24         eth0/0         0.0.0.0   C    0      0     Root

MCJPFSTRFWZ00->
"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP083(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP083_should_return_false_for_juniper_that_has_ntp_server_not_set_as_gateway_in_route_table() {
      var blob = new AssetBlob {
        Body = @"
set ntp server ""138.168.52.1""
MCJPFSTRFWZ00->
MCJPFSTRFWZ00-> get route

IPv4 Dest-Routes for <untrust-vr> (0 entries)
--------------------------------------------------------------------------------
H: Host C: Connected S: Static A: Auto-Exported
I: Imported R: RIP P: Permanent D: Auto-Discovered
iB: IBGP eB: EBGP O: OSPF E1: OSPF external type 1
E2: OSPF external type 2

IPv4 Dest-Routes for <trust-vr> (5 entries)
--------------------------------------------------------------------------------
   ID          IP-Prefix      Interface         Gateway   P Pref    Mtr     Vsys
--------------------------------------------------------------------------------
*   5          0.0.0.0/0         eth0/0    138.168.52.2   S   20      1     Root
*   2   138.168.52.14/32         eth0/0         0.0.0.0   H    0      0     Root
    3   138.168.181.0/27        bgroup0         0.0.0.0   C    0      0     Root
    4   138.168.181.1/32        bgroup0         0.0.0.0   H    0      0     Root
*   1    138.168.52.0/24         eth0/0         0.0.0.0   C    0      0     Root

MCJPFSTRFWZ00->
"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP083(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}