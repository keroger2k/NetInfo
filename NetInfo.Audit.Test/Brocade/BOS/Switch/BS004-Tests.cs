using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS004_Tests {

    [Test]
    public void BS004_should_return_true_access_group_is_correctly_set() {
      var blob = new AssetBlob {
        Body = @"ssh access-group 99"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS004(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS004_should_return_false_access_group_is_incorrectly_set() {
      var blob = new AssetBlob {
        Body = @"ssh access-group 98"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS004(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS004_should_return_false_access_group_is_setting_is_not_found() {
      var blob = new AssetBlob {
        Body = @""
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS004(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}