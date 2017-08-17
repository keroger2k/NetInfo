using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP068_Tests {
    private AssetBlob blob;

    [Test]
    public void VP068_should_return_true_for_juniper_complaint_device() {
      blob = new AssetBlob {
        Body = @"set auth default auth server ""Local"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP068(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP068_should_return_false_for_juniper_noncomplaint_device() {
      blob = new AssetBlob {
        Body = @"set auth default auth server ""NotLocal"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP068(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}