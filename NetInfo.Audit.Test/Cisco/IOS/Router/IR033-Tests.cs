using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR033_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IR033_should_return_true_when_no_ip_http_server_is_found_with_hyphen() {
      blob = new AssetBlob {
        Body = @"no ip http-server"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0740(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR033_should_return_false_when_ip_http_server_is_found_with_hyphen() {
      blob = new AssetBlob {
        Body = @"ip http-server"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0740(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR033_should_return_true_when_no_ip_http_server_is_found() {
      blob = new AssetBlob {
        Body = @"no ip http server"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0740(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR033_should_return_false_when_ip_http_server_is_found() {
      blob = new AssetBlob {
        Body = @"ip http server"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0740(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR033_should_return_false_when_no_ip_http_server_is_not_found() {
      blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0740(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}