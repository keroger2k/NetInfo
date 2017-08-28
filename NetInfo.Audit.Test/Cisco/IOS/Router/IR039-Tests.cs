using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR039_Tests {

    [Test]
    public void IR039_should_return_true_when_ip_directed_broadcast_is_not_enabled_on_any_layer_3_interface() {
      var blob = new AssetBlob {
        Body = @"!
interface Vlan1
 description <== Ver.2.3 VSS ==>
 no ip address
 shutdown
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 10.46.4.65 255.255.255.252
 no ip redirects
 no ip proxy-arp
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0790(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR039_should_return_false_when_ip_directed_broadcast_is_enabled_on_any_layer_3_interface() {
      var blob = new AssetBlob {
        Body = @"!
interface Vlan1
 description <== Ver.2.3 VSS ==>
 no ip address
 shutdown
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 10.46.4.65 255.255.255.252
 ip directed-broadcast
 no ip redirects
 no ip proxy-arp
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0790(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}