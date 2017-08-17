using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS012_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void BS012_should_return_true_ssh_setting_is_correctly_set() {
      blob = new AssetBlob {
        Body = @"ip ssh timeout 60"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS012(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS012_should_return_false_when_ssh_setting_is_incorrectly_set() {
      blob = new AssetBlob {
        Body = @"ip ssh timeout 61"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS012(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS012_should_return_false_when_ssh_setting_is_not_found() {
      blob = new AssetBlob {
        Body = @""
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS012(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}