using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS124_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IS124_should_return_true_when_no_ip_domain_lookup_is_found() {
      blob = new AssetBlob {
        Body = @"no ip domain-lookup"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS124(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS124_should_return_false_when_no_ip_domain_lookup_is_not_found() {
      blob = new AssetBlob {
        Body = @"ip domain-lookup"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS124(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}