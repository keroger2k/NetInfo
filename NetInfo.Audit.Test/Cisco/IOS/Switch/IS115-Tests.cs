using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS115_Tests {

    [Test]
    public void IS115_should_return_true_when_boot_network_and_service_config_are_not_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS115(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS115_should_return_false_when_boot_network_is_found() {
      var blob = new AssetBlob {
        Body = @"boot network"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS115(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS115_should_return_false_when_service_network_and_boot_config_are_found() {
      var blob = new AssetBlob {
        Body = @"no boot network"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS115(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}