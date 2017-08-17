using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS017_Tests {

    [Test]
    public void BS017_should_return_true_when_there_is_no_vlan_1_configured_with_an_ip_address() {
      var blob = new AssetBlob {
        Body = @"!
interface ve 99
 port-name <== Management VLAN ==>
 ip address 10.16.26.74 255.255.254.0
 ip proxy-arp disable
 no ip redirect
!"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS017(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS017_should_return_false_when_there_is_a_vlan_1_configured_with_an_ip_address() {
      var blob = new AssetBlob {
        Body = @"!
interface ve 1
 port-name <== Management VLAN ==>
 ip address 10.16.26.74 255.255.254.0
 ip proxy-arp disable
 no ip redirect
!"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS017(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}