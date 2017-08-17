using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR040_Tests {

    [Test]
    public void IR040_should_return_true_when_all_external_interfaces_have_no_ip_redirects_configured() {
      var blob = new AssetBlob {
        Body = @"!
!
interface Multilink1
 description <== DISA MLPPP Connection See Serial Interfaces for Circuit IDs ==>
 no ip redirects
!
interface Multilink2
 description <== DISA MLPPP Connection See Serial Interfaces for Circuit IDs ==>
 no ip redirects
!
interface GigabitEthernet3/1
 description <== UB1_OR01_G2/5 ==>
!
interface GigabitEthernet3/2
 description <== ODMN_UB1_OS02_f3/22 ==>
!
QUAN-UB1-EO-01#show cdp interface
GigabitEthernet3/1 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
GigabitEthernet3/2 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
Control Plane Interface is down, line protocol is down
  Encapsulation UNKNOWN
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
QUAN-UB1-EO-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR040(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR040_should_return_false_when_not_all_external_interfaces_have_no_ip_redirects_configured() {
      var blob = new AssetBlob {
        Body = @"!
!
interface Multilink1
 description <== DISA MLPPP Connection See Serial Interfaces for Circuit IDs ==>
 no ip redirects
!
interface Multilink2
 description <== DISA MLPPP Connection See Serial Interfaces for Circuit IDs ==>
!
interface GigabitEthernet3/1
 description <== UB1_OR01_G2/5 ==>
!
interface GigabitEthernet3/2
 description <== ODMN_UB1_OS02_f3/22 ==>
!
QUAN-UB1-EO-01#show cdp interface
GigabitEthernet3/1 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
GigabitEthernet3/2 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
Control Plane Interface is down, line protocol is down
  Encapsulation UNKNOWN
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
QUAN-UB1-EO-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR040(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}