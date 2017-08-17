using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR022_Tests {

    [Test]
    public void IR022_should_return_true_when_2_or_more_tacacs_servers_are_found() {
      var blob = new AssetBlob {
        Body = @"!
tacacs-server host 10.16.27.44
tacacs-server host 10.0.16.152
!"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR022(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR022_should_return_false_when_zero_tacacs_servers_are_found() {
      var blob = new AssetBlob {
        Body = @"!
!"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR022(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR022_should_return_false_when_less_than_two_tacacs_servers_are_found() {
      var blob = new AssetBlob {
        Body = @"!
tacacs-server host 10.16.27.44
!"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR022(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}