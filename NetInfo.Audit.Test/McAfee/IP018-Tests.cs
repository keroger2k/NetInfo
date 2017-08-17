using NetInfo.Audit.McAfee;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.McAfee {

  [TestFixture]
  public class IP018_Tests {

    [Test]
    public void IP018_should_return_true_when_aux_port_is_disabled() {
      var blob = new AssetBlob {
        Body = @"Aux Port : Disabled"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP018(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IP018_should_return_false_when_aux_port_is_not_disabled() {
      var blob = new AssetBlob {
        Body = @"Aux Port : Enabled"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP018(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}