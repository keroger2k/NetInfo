using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS112_Tests {

    [Test]
    public void IS112_should_return_true_when_a_correct_domain_name_is_found() {
      AssetBlob blob = new AssetBlob {
        Body = @"Key name: PRLH-UD0-AS-01.NMCI-ISF.com"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS112(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS112_should_return_true_when_standards_are_incorrect_but_still_has_NMCI_ISF_dot_com_at_the_end() {
      AssetBlob blob = new AssetBlob {
        Body = @"Key name: PRLH-UD0-AS-01.PRLH-UD0-AS-01.NMCI-ISF.com"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS112(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS112_should_return_false_when_an_incorrect_domain_name_is_found() {
      AssetBlob blob = new AssetBlob {
        Body = @"Key name: PRLH-UD0-AS-01.FAIL-ISF.com"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS112(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS112_should_return_false_key_value_is_not_found_in_expected_format() {
      AssetBlob blob = new AssetBlob {
        Body = @"Key name: JUST_SOME_RANDOM_TEXT"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS112(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS112_should_return_false_key_value_is_not_found() {
      AssetBlob blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS112(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}