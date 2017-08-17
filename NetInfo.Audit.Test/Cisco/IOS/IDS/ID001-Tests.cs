using NetInfo.Audit.Cisco.IOS.IDS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS.IDS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.IDS {

  [TestFixture]
  public class ID001_Tests {

    [Test]
    public void ID001_should_return_true_when_name_falls_between_sn20_and_sn24() {
      var blob = new AssetBlob {
        Body = @"host-name mcusquansn20"
      };

      INMCIIDSDevice device = new NMCIIDSDevice(blob);
      ISTIGItem item = new ID001(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void ID001_should_return_false_when_name_does_not_falls_between_sn20_and_sn24() {
      var blob = new AssetBlob {
        Body = @"host-name mcusquansn25"
      };

      INMCIIDSDevice device = new NMCIIDSDevice(blob);
      ISTIGItem item = new ID001(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}