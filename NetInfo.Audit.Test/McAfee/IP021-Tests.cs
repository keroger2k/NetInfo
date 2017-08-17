using NetInfo.Audit.McAfee;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.McAfee {

  [TestFixture]
  public class IP021_Tests {

    [Test]
    public void IP021_should_return_true_when_ssh_access_control_is_enabled() {
      var blob = new AssetBlob {
        Body = @"[SSH AccessControl is Enabled]"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP021(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IP021_should_return_false_when_ssh_access_control_is_not_enabled() {
      var blob = new AssetBlob {
        Body = @"[SSH AccessControl is Disabled]"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP021(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}