using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS038_Tests {

    [Test]
    public void IS038_should_return_true_when_no_ip_gratuitous_arps_is_found() {
      var blob = new AssetBlob {
        Body = @"no ip gratuitous-arps"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS038(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS038_should_return_false_when_ip_gratuitous_arps_is_found() {
      var blob = new AssetBlob {
        Body = @"ip gratuitous-arps"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS038(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS038_should_return_false_when_no_ip_identd_is_not_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS038(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}