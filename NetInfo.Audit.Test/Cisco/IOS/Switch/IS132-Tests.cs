using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS132_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IS132_should_return_false_when_ip_http_secure_server_is_found() {
      blob = new AssetBlob {
        Body = @"ip http secure-server"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS132(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS132_should_return_true_when_no_ip_http_secure_server_is_found() {
      blob = new AssetBlob {
        Body = @"no ip http secure-server"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS132(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS132_should_return_true_when_no_ip_http_secure_server_is_setting_is_found() {
      blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS132(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}