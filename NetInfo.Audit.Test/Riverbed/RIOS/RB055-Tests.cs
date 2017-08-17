using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Riverbed.RIOS {

  [TestFixture]
  public class RB055_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void RB055_should_return_true_for_riverbed_complaint_device() {
      blob = new AssetBlob {
        Body = @" no web ssl protocol sslv2"
      };

      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB055(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB055_should_return_false_for_riverbed_non_complaint_device() {
      blob = new AssetBlob {
        Body = @" web ssl protocol sslv2"
      };

      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB055(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}