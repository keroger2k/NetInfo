using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IS129_Tests {

    [Test]
    public void should_return_true_for_device_with_correct_system_auth_control_enabled() {
      AssetBlob blob = new AssetBlob {
        Body = @"!
dot1x system-auth-control
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS129(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void should_return_false_for_device_without_correct_system_auth_control_enabled() {
      AssetBlob blob = new AssetBlob {
        Body = @"!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS129(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}