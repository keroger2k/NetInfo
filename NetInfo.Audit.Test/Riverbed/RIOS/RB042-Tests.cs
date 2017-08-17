using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB042_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void RB042_should_return_true_when_optimization_service_is_running() {
      blob = new AssetBlob {
        Body = @"Optimization Service: Running"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB042(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB042_should_return_false_when_optimization_service_has_been_stopped() {
      blob = new AssetBlob {
        Body = @"Optimization Service: Stopped"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB042(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}