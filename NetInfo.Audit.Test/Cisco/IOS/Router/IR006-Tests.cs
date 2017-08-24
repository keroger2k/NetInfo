using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR006_Tests {

    [Test]
    public void IR006_should_return_true_when_no_rcp_is_found() {
      var blob = new AssetBlob {
        Body = @"no ip rcmd rcp-enable"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0744(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR006_should_return_false_when_rcp_is_found() {
      var blob = new AssetBlob {
        Body = @"ip rcmd rcp-enable"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0744(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR006_should_return_true_when_no_setting_is_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0744(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}