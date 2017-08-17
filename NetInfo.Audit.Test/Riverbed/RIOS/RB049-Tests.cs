using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Riverbed.RIOS {

  [TestFixture]
  public class RB049_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void rb049_should_return_true_for_riverbed_complaint_device() {
      blob = new AssetBlob {
        Body = @"clock timezone UTC"
      };

      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB049(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void bs009_should_return_false_for_riverbed_noncomplaint_device() {
      blob = new AssetBlob {
        Body = @"clock timezone GMT"
      };

      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB049(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}