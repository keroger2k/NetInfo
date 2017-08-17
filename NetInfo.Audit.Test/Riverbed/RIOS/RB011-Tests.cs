using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB011_Tests {

    [Test]
    public void RB011_should_return_true_http_server_is_not_enabled() {
      var blob = new AssetBlob {
        Body = @"
 no web http enable
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB011(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB011_should_return_false_http_server_is_enabled() {
      var blob = new AssetBlob {
        Body = @"
 web http enable
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB011(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}