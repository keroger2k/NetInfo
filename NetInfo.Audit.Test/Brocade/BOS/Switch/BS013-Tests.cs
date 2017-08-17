using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS013_Tests {

    [Test]
    public void BS013_should_return_true_ssh_setting_is_correctly_set() {
      var blob = new AssetBlob {
        Body = @"ip ssh authentication-retries 3"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS013(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS013_should_return_false_when_ssh_setting_is_incorrectly_set() {
      var blob = new AssetBlob {
        Body = @"ip ssh authentication-retries 4"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS013(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS013_should_return_true_when_ssh_setting_is_not_found_because_this_is_the_default() {
      var blob = new AssetBlob {
        Body = @""
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS013(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}