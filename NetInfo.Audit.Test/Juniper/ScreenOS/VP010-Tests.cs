using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP010_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void VP010_should_return_true_for_juniper_complaint_device() {
      blob = new AssetBlob {
        Body = @"set admin name ""NS-ADMIN"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP010(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP010_should_return_false_for_juniper_noncomplaint_device() {
      blob = new AssetBlob {
        Body = @"set admin name ""FAIL"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP010(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}