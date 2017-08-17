using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS029_Tests {

    [Test]
    public void BS029_should_return_true_when_all_the_correct_commands_are_found() {
      var blob = new AssetBlob {
        Body = @"
aaa authentication enable default tacacs+ enable
aaa authentication login default tacacs+ enable
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS029(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS029_should_return_false_when_enable_default_tacacs_is_not_found() {
      var blob = new AssetBlob {
        Body = @"
aaa authentication login default tacacs+ enable
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS029(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS029_should_return_false_when_login_default_tacacs_is_not_found() {
      var blob = new AssetBlob {
        Body = @"
aaa authentication enable default tacacs+ enable
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS029(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS029_should_return_false_when_no_commands_are_found() {
      var blob = new AssetBlob {
        Body = @"
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS029(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}