using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR178_Tests {

    [Test]
    public void IR178_should_return_true_when_ntp_is_configured_with_a_key_authenticate_and_a_trusted_key() {
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
      ISTIGItem item = new NET0813(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR178_should_return_false_when_no_trusted_key_is_configured() {
      var blob = new AssetBlob {
        Body = @"!
ntp authentication-key 1 md5 150B434543231A273D7D073B0728432C 7
ntp authenticate
ntp server 10.0.16.10 key 1
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0813(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR178_should_return_false_when_no_authentication_key_is_configured() {
      var blob = new AssetBlob {
        Body = @"!
ntp server 10.0.16.10
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0813(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR178_should_return_false_when_ntp_authenticate_is_missing() {
      var blob = new AssetBlob {
        Body = @"!
ntp authentication-key 1 md5 150B434543231A273D7D073B0728432C 7
ntp trusted-key 1
ntp server 10.0.16.10 key 1
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0813(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}