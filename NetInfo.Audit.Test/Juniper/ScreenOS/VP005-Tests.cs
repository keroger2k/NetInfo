using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {
  /// <summary>
  /// Updates:
  ///   Bug: 15138: Changed from "all" to "read-only"
  /// </summary>

  [TestFixture]
  public class VP005_Tests {

    [Test]
    public void VP005_should_return_true_when_nrfk_admin_password_is_correct_and_priv_mode_is_set_to_read_only() {
      var blob = new AssetBlob {
        Body = @"set admin user ""RD-ADMIN"" password ""good"" privilege ""read-only"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP005(device, new[] { "good", "old" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP005_should_return_false_when_nrfk_admin_password_is_incorrect_and_priv_mode_is_set_to_read_only() {
      var blob = new AssetBlob {
        Body = @"set admin user ""RD-ADMIN"" password ""fail"" privilege ""read-only"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP005(device, new[] { "good", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP005_should_return_false_when_nrfk_admin_password_is_correct_and_priv_mode_is_not_set_to_read_only() {
      var blob = new AssetBlob {
        Body = @"set admin user ""RD-ADMIN"" password ""good"" privilege ""all"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP005(device, new[] { "good", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}