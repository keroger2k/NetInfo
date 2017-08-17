using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IR088_Tests {

    [Test]
    public void IR088_should_return_true_when_the_major_version_matches_the_alias_exec_command() {
      var blob = new AssetBlob {
        Body = @"alias exec nms uNRFK-INSIDE-SNMPV3-IOSRTR-v7_1_0"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR088(device, nmsMajorVersion: 7);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR088_should_return_false_when_the_major_version_does_not_match_the_alias_exec_command() {
      var blob = new AssetBlob {
        Body = @"alias exec nms uNRFK-INSIDE-SNMPV3-IOSRTR-v7_1_0"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR088(device, nmsMajorVersion: 8);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR088_should_return_false_when_no_alias_exec_commands_are_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR088(device, nmsMajorVersion: 8);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}