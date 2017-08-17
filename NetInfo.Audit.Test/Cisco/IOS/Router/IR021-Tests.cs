using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR021_Tests {

    [Test]
    public void IR021_should_return_true_when_tacacs_group_is_found() {
      var blob = new AssetBlob {
        Body = @"!
aaa authentication login default group tacacs+ enable
aaa authentication enable default group tacacs+ enable
!"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR021(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR021_should_return_false_when_tacacs_group_is_not_found() {
      var blob = new AssetBlob {
        Body = @"!
!"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR021(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR021_should_return_false_when_login_and_enable_groups_are_not_tacacs() {
      var blob = new AssetBlob {
        Body = @"!
aaa authentication login default group radius enable
aaa authentication enable default group radius enable
!"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR021(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}