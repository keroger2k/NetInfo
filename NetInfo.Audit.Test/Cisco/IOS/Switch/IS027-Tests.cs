using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS027_Tests {

    [Test]
    public void IS027_should_return_true_when_neither_udp_tcp_small_servers_are_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS027(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS027_should_return_false_if_tcp_small_server_is_found() {
      var blob = new AssetBlob {
        Body = @"service tcp-small-servers"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS027(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS027_should_return_false_if_udp_small_server_is_found() {
      var blob = new AssetBlob {
        Body = @"service udp-small-servers"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS027(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS027_should_return_false_if_both_tcp_udp_small_server_are_found() {
      var blob = new AssetBlob {
        Body = @"service udp-small-servers
service tcp-small-servers"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS027(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS027_should_return_true_if_both_tcp_udp_small_server_are_not_enabled() {
      var blob = new AssetBlob {
        Body = @"no service udp-small-servers
no service tcp-small-servers"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS027(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}