using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS007_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void BS007_should_return_true_when_password_encryption_is_not_found() {
      blob = new AssetBlob {
        Body = @""
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS007(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS007_should_return_false_when_password_encryption_is_found() {
      blob = new AssetBlob {
        Body = @"no service password-encryption"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS004(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}