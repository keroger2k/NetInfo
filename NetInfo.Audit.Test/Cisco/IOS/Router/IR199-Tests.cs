using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR199_Tests {

    [Test]
    public void IR199_should_return_true_when_the_ntp_servers_are_configured_with_keys_that_have_matching_authentications_keys() {
      var blob = new AssetBlob {
        Body = @"!
ntp authentication-key 1 md5 150B434543231A273D7D073B0728432C 7
ntp authenticate
ntp trusted-key 1
ntp server 10.0.16.10 key 1
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR199(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR199_should_return_false_when_the_ntp_servers_are_not_configured_with_keys_that_have_matching_authentications_keys() {
      var blob = new AssetBlob {
        Body = @"!
ntp authentication-key 2 md5 150B434543231A273D7D073B0728432C 7
ntp authenticate
ntp trusted-key 1
ntp server 10.0.16.10 key 1
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR199(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR199_should_return_false_when_the_ntp_servers_are_not_configured_with_keys() {
      var blob = new AssetBlob {
        Body = @"!
ntp server 10.0.16.10
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR199(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR199_should_return_false_when_ntp_servers_are_not_configured() {
      var blob = new AssetBlob {
        Body = @"!
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR199(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}