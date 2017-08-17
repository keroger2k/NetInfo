using NetInfo.Audit.Cisco.IOS.IDS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS.IDS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.IDS {

  [TestFixture]
  public class ID006_Tests {

    [TestCase(@"standard-time-zone-name UTC")]
    [TestCase(@"standard-time-zone-name: UTC <defaulted>")]
    [TestCase(@"  standard-time-zone-name: UTC <defaulted>")]
    public void ID006_should_return_true_for_brocade_switch_complaint_device(string line) {
      var blob = new AssetBlob {
        Body = line
      };

      INMCIIDSDevice device = new NMCIIDSDevice(blob);
      ISTIGItem item = new ID006(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [TestCase(@"standard-time-zone-name GMT")]
    [TestCase(@"standard-time-zone-name: GMT <defaulted>")]
    [TestCase(@"  standard-time-zone-name: GMT <defaulted>")]
    public void ID006_should_return_false_for_brocade_switch_noncomplaint_device(string line) {
      var blob = new AssetBlob {
        Body = line
      };

      INMCIIDSDevice device = new NMCIIDSDevice(blob);
      ISTIGItem item = new ID006(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}