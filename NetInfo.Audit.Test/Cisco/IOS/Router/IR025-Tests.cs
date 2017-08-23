using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR025_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IR025_should_return_true_when_service_password_encryption_is_found() {
      blob = new AssetBlob {
        Body = @"service password-encryption"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0600(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR025_should_return_false_when_service_password_encryption_is_not_enabled() {
      blob = new AssetBlob {
        Body = @"no service password-encryption"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0600(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR025_should_return_false_when_service_password_encryption_is_not_found() {
      blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0600(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}