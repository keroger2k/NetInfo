using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP078_Tests {

    [Test]
    public void VP078_should_return_true_when_vlan_1_has_bypass_others_ipsec_configured() {
      var blob = new AssetBlob {
        Body = @"unset interface vlan1 bypass-others-ipsec"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP078(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP078_should_return_false_when_vlan_1_doe_not_have_bypass_others_ipsec_configured() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP078(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}