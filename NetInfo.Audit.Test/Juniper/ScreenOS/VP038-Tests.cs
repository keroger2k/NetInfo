using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP038_Tests {

    [Test]
    public void VP038_should_return_true_when_netowrk_time_is_disabled() {
      var blob = new AssetBlob {
        Body = @"The Network Time Protocol is Disabled"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP038(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP038_should_return_false_when_network_time_is_enbled() {
      var blob = new AssetBlob {
        Body = @"The Network Time Protocol is Enabled"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP038(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP038_should_return_false_no_configuration_is_found_for_network_time() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP038(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}