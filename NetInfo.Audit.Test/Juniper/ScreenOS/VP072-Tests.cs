using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP072_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void vp072_should_return_true_for_juniper4x_complaint_device() {
      blob = new AssetBlob {
        Body = @"set clock timezone 0"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP072(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void vp072_should_return_false_for_juniper4x_noncomplaint_device() {
      blob = new AssetBlob {
        Body = @"set clock timezone 1"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP072(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}