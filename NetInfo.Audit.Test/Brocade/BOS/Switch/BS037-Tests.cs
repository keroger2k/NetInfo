using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS037_Tests {

    [Test]
    public void BS037_should_return_true_when_radius_is_default_authentication_for_dot1x() {
      var blob = new AssetBlob {
        Body = @"
aaa authentication dot1x default radius
"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS037(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS037_should_return_false_when_radius_is_not_default_authentication_for_dot1x() {
      var blob = new AssetBlob {
        Body = @"
aaa authentication dot1x default tacacs
"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS037(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS037_should_return_false_no_authentication_is_configured_for_dot1x() {
      var blob = new AssetBlob {
        Body = @"
"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS037(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}