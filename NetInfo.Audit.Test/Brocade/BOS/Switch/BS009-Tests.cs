using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS009_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void bs009_should_return_true_for_brocade_switch_complaint_device() {
      blob = new AssetBlob {
        Body = @"console timeout 3"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS009(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void bs009_should_return_false_for_brocade_switch_noncomplaint_device() {
      blob = new AssetBlob {
        Body = @"console timeout 0"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS009(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void bs009_should_return_false_when_it_doesnt_find_timeout() {
      blob = new AssetBlob {
        Body = @""
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS009(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}