using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR079_Tests {

    [Test]
    public void IR079_should_return_true_when_there_is_only_vpn_descriptions_on_enabled_ports() {
      var blob = new AssetBlob {
        Body = @"
!
interface FastEthernet0/1
 description <== NETSCREEN VPN ==>
!
interface FastEthernet0/2
 description <== DISABLED ==>
 shutdown
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR079(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR079_should_return_false_when_there_is_a_vpn_description_on_a_shutdown_port() {
      var blob = new AssetBlob {
        Body = @"
!
interface FastEthernet0/1
 description <== NETSCREEN VPN ==>
!
interface FastEthernet0/2
 description <== NETSCREEN VPN ==>
 shutdown
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR079(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}