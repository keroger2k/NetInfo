using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR068_Tests {

    [Test]
    public void IR068_should_return_true_when_ssh_timeout_is_equal_to_30() {
      var blob = new AssetBlob {
        Body = @"ip ssh time-out 30"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1645(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR068_should_return_false_when_ssh_timeout_is_not_equal_to_30() {
      var blob = new AssetBlob {
        Body = @"ip ssh time-out 40"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1645(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}