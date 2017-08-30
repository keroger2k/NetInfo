using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR180_Tests {

    [Test]
    public void IR180_should_return_true_when_ssh_version_is_2() {
      var blob = new AssetBlob {
        Body = @"ip ssh version 2"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1647(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR180_should_return_true_when_ssh_version_is_not_2() {
      var blob = new AssetBlob {
        Body = @"ip ssh version 3"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1647(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}