using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR165_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IR165_should_return_true_when_boot_network_and_service_config_are_not_found() {
      blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR165(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR165_should_return_false_when_boot_network_is_found() {
      blob = new AssetBlob {
        Body = @"boot network"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR165(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR165_should_return_true_when_no_service_network_are_found() {
      blob = new AssetBlob {
        Body = @"no boot network"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR165(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}