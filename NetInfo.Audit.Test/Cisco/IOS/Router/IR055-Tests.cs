using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR055_Tests {

    [Test]
    public void IR055_should_return_true_when_both_enable_and_login_for_tacacs_are_enabled() {
      var blob = new AssetBlob {
        Body = @"aaa new-model
!
!
aaa authentication login default group tacacs+ enable
aaa authentication enable default group tacacs+ enable
!
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR055(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR055_should_return_false_when_login_is_not_present() {
      var blob = new AssetBlob {
        Body = @"aaa new-model
!
!
aaa authentication enable default group tacacs+ enable
!
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR055(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR055_should_return_false_when_enable_is_not_present() {
      var blob = new AssetBlob {
        Body = @"aaa new-model
!
!
aaa authentication login default group tacacs+ enable
!
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR055(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR055_should_return_false_when_neither_login_or_enable_are_not_present() {
      var blob = new AssetBlob {
        Body = @"aaa new-model
!
!
!
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR055(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}