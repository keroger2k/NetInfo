using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR030_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IR030_should_return_true_when_no_ip_identd_is_found() {
      blob = new AssetBlob {
        Body = @"no ip identd"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0726(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR030_should_return_false_when_ip_idented_is_found() {
      blob = new AssetBlob {
        Body = @"ip identd"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0726(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR030_should_return_false_when_no_ip_identd_is_not_found() {
      blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0726(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}