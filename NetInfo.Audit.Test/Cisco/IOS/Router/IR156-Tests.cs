using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR156_Tests {

    [Test]
    public void IR156_should_return_true_when_a_correct_domain_name_is_found() {
      var blob = new AssetBlob {
        Body = @"Key name: PRLH-UD0-AS-01.NMCI-ISF.com"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR156(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR156_should_return_false_when_an_incorrect_domain_name_is_found() {
      var blob = new AssetBlob {
        Body = @"Key name: PRLH-UD0-AS-01.FAIL-ISF.com"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR156(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}