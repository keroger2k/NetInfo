using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS061_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IS061_should_return_true_when_ssh_authentication_retries_is_three_or_less() {
      blob = new AssetBlob {
        Body = @"ip ssh authentication-retries 2"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS061(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS061_should_return_false_when_authentication_retries_is_above_thee() {
      blob = new AssetBlob {
        Body = @"ip ssh authentication-retries 4"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS061(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}