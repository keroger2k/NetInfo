using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR179_Tests {

    [Test]
    public void IR179_should_return_true_when_no_ip_domain_lookup_is_found() {
      var blob = new AssetBlob {
        Body = @"no ip domain-lookup"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR179(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR179_should_return_false_when_no_ip_domain_lookup_is_not_found() {
      var blob = new AssetBlob {
        Body = @"ip domain-lookup"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR179(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}