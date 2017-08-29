using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR061_Tests {

    [Test]
    public void IR061_should_return_true_when_ssh_authentication_retries_is_three_or_less() {
      var blob = new AssetBlob {
        Body = @"ip ssh authentication-retries 2"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1646(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR061_should_return_false_when_authentication_retries_is_above_thee() {
      var blob = new AssetBlob {
        Body = @"ip ssh authentication-retries 4"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1646(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}