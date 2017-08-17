using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP077_Tests {

    [Test]
    public void VP077_should_return_true_when_vlan_1_has_bypass_non_ip_configured() {
      var blob = new AssetBlob {
        Body = @"unset interface vlan1 bypass-non-ip"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP077(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP077_should_return_false_when_vlan_1_doe_not_have_bypass_non_ip_configured() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP077(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}