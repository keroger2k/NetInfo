using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS125_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IS125_should_return_true_when_ssh_version_is_2() {
      blob = new AssetBlob {
        Body = @"ip ssh version 2"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS125(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS125_should_return_true_when_ssh_version_is_not_2() {
      blob = new AssetBlob {
        Body = @"ip ssh version 3"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS125(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}