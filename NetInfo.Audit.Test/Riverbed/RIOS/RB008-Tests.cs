using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB008_Tests {

    [Test]
    public void RB008_should_return_true_when_server_listen_enable_is_found() {
      var blob = new AssetBlob {
        Body = @"
 ssh server listen enable
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB008(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB008_should_return_false_when_server_listen_enable_is_not_found() {
      var blob = new AssetBlob {
        Body = @"
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB008(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}