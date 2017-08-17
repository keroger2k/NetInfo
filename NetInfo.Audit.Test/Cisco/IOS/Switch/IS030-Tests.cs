using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS030_Tests {

    [Test]
    public void IS030_should_return_true_when_no_ip_identd_is_found() {
      var blob = new AssetBlob {
        Body = @"no ip identd"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS030(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS030_should_return_false_when_ip_idented_is_found() {
      var blob = new AssetBlob {
        Body = @"ip identd"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS030(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS030_should_return_false_when_no_ip_identd_is_not_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS030(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}