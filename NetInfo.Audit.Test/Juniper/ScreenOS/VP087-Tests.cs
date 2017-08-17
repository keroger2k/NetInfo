using System.Collections.Generic;
using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP087_Tests {
    private IEnumerable<string> users;

    [SetUp]
    public void Init() {
      this.users = new string[] {
        "NRFK-ADMIN",
        "PRLH-ADMIN",
        "SDNI-ADMIN",
        "RD-ADMIN"
      };
    }

    [Test]
    public void test() {
      var blob = new AssetBlob {
        Body = @"set admin user ""NRFK-ADMIN"" password ""nDugLMr8B42GcueEAswOo4Nt+zKzqn"" privilege ""all""
set admin user ""PRLH-ADMIN"" password ""nN0wI8r4Ft/LcsMGSsQHBwFtsGMIHn"" privilege ""all""
set admin user ""SDNI-ADMIN"" password ""nNwBKWrlI2cLcc0B9sqOryItPpGCzn"" privilege ""all""
set admin user ""RD-ADMIN"" password ""nIrHBarBIbaEcnyABsBNxHNtFKHQ1n"" privilege ""read-only"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP087(device, users);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP087_should_return_true_when_the_approved_user_list_matches_the_admin_users() {
      var blob = new AssetBlob {
        Body = @"set admin user ""NRFK-ADMIN"" password ""nD44DjrDCLiNcgQAXsxMCsAticNsun"" privilege ""all""
set admin user ""PRLH-ADMIN"" password ""nNUyHMr4PAOJcw/MDsmDj2HtrrGKFn"" privilege ""all""
set admin user ""SDNI-ADMIN"" password ""nJ7cLZrSOtuHc3mMgsNIWJOtnzKvXn"" privilege ""all""
set admin user ""RD-ADMIN"" password ""nND8E7r6E1dCcMhCdsxOfLEtUwMbnn"" privilege ""read-only"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP087(device, users);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP087_should_return_false_when_additional_users_are_added() {
      var blob = new AssetBlob {
        Body = @"set admin user ""NRFK-ADMIN"" password ""nD44DjrDCLiNcgQAXsxMCsAticNsun"" privilege ""all""
set admin user ""PRLH-ADMIN"" password ""nNUyHMr4PAOJcw/MDsmDj2HtrrGKFn"" privilege ""all""
set admin user ""SDNI-ADMIN"" password ""nJ7cLZrSOtuHc3mMgsNIWJOtnzKvXn"" privilege ""all""
set admin user ""RD-ADMIN"" password ""nND8E7r6E1dCcMhCdsxOfLEtUwMbnn"" privilege ""read-only""
set admin user ""RD-ADMIN1"" password ""nND8E7r6E1dCcMhCdsxOfLEtUwMbnn"" privilege ""read-only"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP087(device, users);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP087_should_return_false_when_not_all_users_are_found() {
      var blob = new AssetBlob {
        Body = @"set admin user ""NRFK-ADMIN"" password ""nD44DjrDCLiNcgQAXsxMCsAticNsun"" privilege ""all""
set admin user ""PRLH-ADMIN"" password ""nNUyHMr4PAOJcw/MDsmDj2HtrrGKFn"" privilege ""all""
set admin user ""RD-ADMIN"" password ""nND8E7r6E1dCcMhCdsxOfLEtUwMbnn"" privilege ""read-only"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP087(device, users);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP087_should_return_false_when_no_admin_users_settings_are_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP087(device, users);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}