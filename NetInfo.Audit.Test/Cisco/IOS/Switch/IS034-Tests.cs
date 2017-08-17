using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS034_Tests {

    [Test]
    public void IS034_should_return_true_when_no_ip_bootp_server_is_found() {
      var blob = new AssetBlob {
        Body = @"no ip bootp server"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS034(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS034_should_return_true_when_no_ip_bootp_server_settting_is_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS034(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS034_should_return_false_when_ip_bootp_server_is_found() {
      var blob = new AssetBlob {
        Body = @"ip bootp server"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS034(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}