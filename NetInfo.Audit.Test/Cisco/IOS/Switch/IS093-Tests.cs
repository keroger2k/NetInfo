using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS093_Tests {

    [Test]
    public void IR093_should_return_true_when_the_major_version_matches_the_alias_exec_command() {
      var blob = new AssetBlob {
        Body = @"alias exec harden uNAVY-INSIDE-SSH-IOSRTR-v24_0_0"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS093(device, hardeningMajorVersion: 24);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR093_should_return_false_when_the_major_version_does_not_matches_the_alias_exec_command() {
      var blob = new AssetBlob {
        Body = @"alias exec harden uNAVY-INSIDE-SSH-IOSRTR-v24_0_0"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS093(device, hardeningMajorVersion: 25);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS093_should_return_false_when_alias_exec_command_can_not_be_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS093(device, hardeningMajorVersion: 25);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}