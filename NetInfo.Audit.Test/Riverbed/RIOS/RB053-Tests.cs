using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB053_Tests {

    [Test]
    public void RB053_should_return_true_when_login_default_tacacs_local_is_configured() {
      var blob = new AssetBlob {
        Body = @"
   aaa authentication login default tacacs+ local
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB053(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB053_should_return_false_when_login_default_tacacs_local_is_not_configured() {
      var blob = new AssetBlob {
        Body = @"
   aaa authentication login default radius local
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB053(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}