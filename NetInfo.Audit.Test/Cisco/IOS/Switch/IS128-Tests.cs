using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS128_Tests {

    [Test]
    public void should_return_true_for_device_with_correct_aaa_setting() {
      AssetBlob blob = new AssetBlob {
        Body = @"!
aaa authentication dot1x default group radius
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS128(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void should_return_false_for_device_with_incorrect_aaa_setting() {
      AssetBlob blob = new AssetBlob {
        Body = @"!
aaa authentication dot1x default group fail
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS128(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}