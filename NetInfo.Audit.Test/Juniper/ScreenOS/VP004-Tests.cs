using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP004_Tests {

    [Test]
    public void VP004_should_return_true_when_nrfk_admin_password_is_correct_and_priv_mode_is_set_to_all() {
      var blob = new AssetBlob {
        Body = @"set admin user ""PRLH-ADMIN"" password ""good"" privilege ""all"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP004(device, new[] { "good", "old" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP004_should_return_false_when_nrfk_admin_password_is_incorrect_and_priv_mode_is_set_to_all() {
      var blob = new AssetBlob {
        Body = @"set admin user ""PRLH-ADMIN"" password ""fail"" privilege ""all"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP004(device, new[] { "good", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP004_should_return_false_when_nrfk_admin_password_is_correct_and_priv_mode_is_not_set_to_all() {
      var blob = new AssetBlob {
        Body = @"set admin user ""PRLH-ADMIN"" password ""good"" privilege ""fail"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP004(device, new[] { "good", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}