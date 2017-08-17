using System.Collections.Generic;
using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Audit.Models;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR080_Tests {

    private IEnumerable<CircuitInformation> circuits = new List<CircuitInformation> {
      new CircuitInformation { CircuitId = "1111" },
      new CircuitInformation { CircuitId = "2222" }
    };

    [Test]
    public void IR080_should_return_true_when_all_interfaces_with_circuit_ids_match_site_circuitids_in_netckt() {
      var blob = new AssetBlob {
        Body = @"
!
!
interface POS3/1/0
 description <== DISA/COINs CircuitID=1111 ==>
 ip address 214.40.2.166 255.255.255.252
!
interface POS3/1/1
 description <== DISA/COINs CircuitID=2222 ==>
 ip address 214.40.2.167 255.255.255.252
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR080(device, circuits);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR080_should_return_false_when_not_all_interfaces_with_circuit_ids_match_site_circuitids_in_netckt() {
      var blob = new AssetBlob {
        Body = @"
!
!
interface POS3/1/0
 description <== DISA/COINs CircuitID=XXXX ==>
 ip address 214.40.2.166 255.255.255.252
!
interface POS3/1/1
 description <== DISA/COINs CircuitID=2222 ==>
 ip address 214.40.2.166 255.255.255.252
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR080(device, circuits);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR080_should_be_able_account_for_multiple_circuit_ids_in_an_interface_description() {
      var blob = new AssetBlob {
        Body = @"!
interface Multilink2
 description <== DISA/COINs CircuitID=1111, 2222 ==>
 ip address 164.233.18.74 255.255.255.252
 ip access-group OR01_NMCI_BGP_VPN_FILTER_IN_V8-3-1 in
 ip access-group OR01_NMCI_UTB_OUT_V2-0-1 out
 no ip redirects
 no ip unreachables
 no ip proxy-arp
 no cdp enable
 ppp multilink
 ppp multilink group 2
!
interface Serial0/0:0
 description <== DISA/COINs 1111 ==>
 no ip address
 encapsulation ppp
 no fair-queue
 no cdp enable
 ppp multilink
 ppp multilink group 2
!
interface Serial0/0:0
 description <== DISA/COINs 2222 ==>
 no ip address
 encapsulation ppp
 no fair-queue
 no cdp enable
 ppp multilink
 ppp multilink group 2
!
interface FastEthernet0/1
 description <== DISABLED ==>
 no ip address
 shutdown
 speed 100
 full-duplex
 no cdp enable
!
interface FastEthernet0/1
 description <== CiscoIDS$ - SPAN 40 ==>
 duplex full
 speed 100
 no cdp enable
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR080(device, circuits);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}