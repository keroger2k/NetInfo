using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR023_Tests {

    [Test]
    public void IR023_should_return_true_for_when_user_password_is_in_valid_password_list() {
      var blob = new AssetBlob {
        Body = @"
username localadmin privilege 0 password 7 GOODPASSWORD
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR023(device, new[] { "GOODPASSWORD", "old" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR023_should_return_false_when_there_are_two_users_configured() {
      var blob = new AssetBlob {
        Body = @"
username localadmin privilege 0 password 7 GOODPASSWORD
username nogooduser privilege 0 password 7 GOODPASSWORD
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR023(device, new[] { "GOODPASSWORD", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR023_should_return_false_for_when_user_password_is_not_in_valid_password_list() {
      var blob = new AssetBlob {
        Body = @"
username localadmin privilege 0 password 7 BADPASSWORD
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR023(device, new[] { "GOODPASSWORD", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}