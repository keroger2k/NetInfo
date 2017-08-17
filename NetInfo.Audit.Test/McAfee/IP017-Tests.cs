using NetInfo.Audit.McAfee;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.McAfee {

  [TestFixture]
  public class IP017_Tests {

    [Test]
    public void IP017_should_return_true_when_console_timeout_is_ten_minutes() {
      var blob = new AssetBlob {
        Body = @"Console timeout : 10 mins"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP017(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IP017_should_return_false_when_console_timeout_is_not_ten_minutes() {
      var blob = new AssetBlob {
        Body = @"Console timeout : 0 mins"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP017(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}