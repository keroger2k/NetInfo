using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS092_Tests {

    [TestCase(@"clock timezone utc 0")]
    [TestCase(@"clock timezone utc 0 0")]
    public void is099_should_return_true_for_complaint_device(string line) {
      var blob = new AssetBlob {
        Body = line
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS092(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [TestCase(@"")]
    [TestCase(@"clock timezone utc -1")]
    [TestCase(@"clock timezone utc -1 -1")]
    [TestCase(@"clock timezone utc 0 -1")]
    [TestCase(@"clock timezone utc -1 0")]
    public void is099_should_return_false_for_noncomplaint_device(string line) {
      var blob = new AssetBlob {
        Body = line
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS092(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void is099_should_return_true_when_clock_setting_is_not_found_in_config_but_in_show_clock() {
      var blob = new AssetBlob {
        Body = @"CLJN-U04-AS-34#!
CLJN-U04-AS-34#show clock
12:00:15.795 UTC Wed Mar 13 2013
CLJN-U04-AS-34#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS092(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}