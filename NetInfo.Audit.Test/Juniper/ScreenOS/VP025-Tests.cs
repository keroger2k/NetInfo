using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP025_Tests {

    [Test]
    public void VP025_should_return_true_when_use_interface_ip_config_port_80_is_found() {
      var blob = new AssetBlob {
        Body = @"Use interface IP, Config Port: 80"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP025(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP025_should_return_false_when_use_interface_ip_config_port_81_is_found() {
      var blob = new AssetBlob {
        Body = @"Use interface IP, Config Port: 81"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP025(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP025_should_return_false_when_no_use_interface_ip_config_port_80_is_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP025(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}