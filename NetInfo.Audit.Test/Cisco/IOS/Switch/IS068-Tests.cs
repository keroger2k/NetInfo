using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS068_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IS068_should_return_true_when_ssh_timeout_is_equal_to_30() {
      blob = new AssetBlob {
        Body = @"ip ssh time-out 30"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS068(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS068_should_return_false_when_ssh_timeout_is_not_equal_to_30() {
      blob = new AssetBlob {
        Body = @"ip ssh time-out 40"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS068(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}