using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP056_Tests {

    [Test]
    public void VP056_should_return_true_when_netowrk_time_is_enabled() {
      var blob = new AssetBlob {
        Body = @"set clock ntp"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP056(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP056_should_return_false_no_configuration_is_found_for_network_time() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP056(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}