using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR176_Tests {

    [Test]
    public void IR176_should_return_false_when_no_vstack_commands_are_not_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR176(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR176_should_return_true_no_vstack_command_is_found() {
      var blob = new AssetBlob {
        Body = @"no vstack"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR176(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR176_should_return_true_when_no_vstack_enable_command_is_found() {
      var blob = new AssetBlob {
        Body = @"no vstack enable"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR176(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}