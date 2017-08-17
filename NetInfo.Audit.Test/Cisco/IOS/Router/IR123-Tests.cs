using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR123_Tests {

    [Test]
    public void IR123_should_return_true_when_all_ports_are_connected_or_disabled() {
      var blob = new AssetBlob {
        Body = @"AKOZ-U00-IR-01#!
AKOZ-U00-IR-01#show interface status
Port Name Status Vlan Duplex Speed Type
Fa1/0 <== NS_VPN_1 (RED) connected 30 full 100 10/100BaseTX
Fa1/1 <== TEST ==> connected 2 full 100 10/100BaseTX
Fa1/2 <== DISABLED ==> disabled 2 full 100 10/100BaseTX
AKOZ-U00-IR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR123(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR123_should_return_false_when_all_ports_are_not_connected() {
      var blob = new AssetBlob {
        Body = @"AKOZ-U00-IR-01#!
AKOZ-U00-IR-01#show interface status
Port Name Status Vlan Duplex Speed Type
Fa1/0 <== NS_VPN_1 (RED) connected 30 full 100 10/100BaseTX
Fa1/1 <== TEST ==> notconnected 2 full 100 10/100BaseTX
Fa1/2 <== DISABLED ==> disabled 2 full 100 10/100BaseTX
AKOZ-U00-IR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR123(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}