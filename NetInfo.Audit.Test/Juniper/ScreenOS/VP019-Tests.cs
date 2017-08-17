using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP019_Tests {

    [Test]
    public void VP019_should_return_true_when_snmp_community_string_has_correct_password_5x() {
      var blob = new AssetBlob {
        Body = @"set snmp community ""jPC$!wEWxs57"" Read-Only Trap-on  version v1"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP019(device, new string[] { "jPC$!wEWxs57", "old" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP019_should_return_true_when_snmp_community_string_has_correct_password_4x() {
      var blob = new AssetBlob {
        Body = @"set snmp community ""jPC$!wEWxs57"" Read-Only Trap-on"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP019(device, new string[] { "jPC$!wEWxs57", "old" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP019_should_return_false_when_snmp_community_string_has_incorrect_password_5x() {
      var blob = new AssetBlob {
        Body = @"set snmp community ""fail"" Read-Only Trap-on  version v1"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP019(device, new string[] { "jPC$!wEWxs57", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP019_should_return_false_when_snmp_community_string_has_incorrect_password_4x() {
      var blob = new AssetBlob {
        Body = @"set snmp community ""fail"" Read-Only Trap-on"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP019(device, new string[] { "jPC$!wEWxs57", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}