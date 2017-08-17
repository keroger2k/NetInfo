using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS023_Tests {

    [Test]
    public void IS023_should_return_true_for_when_user_password_is_in_valid_password_list() {
      var blob = new AssetBlob {
        Body = @"
username localadmin privilege 0 password 7 GOODPASSWORD
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS023(device, new[] { "GOODPASSWORD", "old" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS023_should_return_false_when_there_are_two_users_configured() {
      var blob = new AssetBlob {
        Body = @"
username localadmin privilege 0 password 7 GOODPASSWORD
username nogooduser privilege 0 password 7 GOODPASSWORD
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS023(device, new[] { "GOODPASSWORD", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS023_should_return_false_for_when_user_password_is_not_in_valid_password_list() {
      var blob = new AssetBlob {
        Body = @"
username localadmin privilege 0 password 7 BADPASSWORD
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS023(device, new[] { "GOODPASSWORD", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS023_should_return_false_when_config_settings_are_not_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS023(device, new[] { "GOODPASSWORD", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}