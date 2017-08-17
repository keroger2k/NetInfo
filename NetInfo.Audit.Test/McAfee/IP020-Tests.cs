using NetInfo.Audit.McAfee;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.McAfee {

  [TestFixture]
  public class IP020_Tests {

    [Test]
    public void IP020_should_return_true_when_ssh_inactive_timeout_is_sixty_seconds() {
      var blob = new AssetBlob {
        Body = @"SSH inactive timeout	: 60 sec"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP020(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IP020_should_return_false_when_ssh_inactive_timeout_is_not_sixty_seconds() {
      var blob = new AssetBlob {
        Body = @"SSH inactive timeout	: 0 sec"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP020(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}