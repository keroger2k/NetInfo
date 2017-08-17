using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS032_Tests {

    [Test]
    public void BS032_should_true_if_no_community_strings_are_found() {
      var blob = new AssetBlob {
        Body = @""
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS032(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS032_should_return_false_if_a_community_string_is_found() {
      var blob = new AssetBlob {
        Body = @"
snmp-server community test ro
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS032(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}