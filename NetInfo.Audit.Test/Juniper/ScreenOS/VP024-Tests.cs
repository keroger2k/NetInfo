using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP024_Tests {

    [Test]
    public void VP024_should_return_true_when_there_is_no_ip_configured_for_vlan1() {
      var blob = new AssetBlob {
        Body = @"unset interface vlan1 ip"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP024(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP024_should_return_false_when_there_is_an_ip_configured_for_vlan1() {
      var blob = new AssetBlob {
        Body = @"set interface vlan1 ip 70.91.148.89/30"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP024(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}