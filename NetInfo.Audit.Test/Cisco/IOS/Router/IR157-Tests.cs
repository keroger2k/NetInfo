using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR157_Tests {

    [Test]
    public void IR157_should_return_true_when_no_crypto_pki_trustpoints_or_certification_chains_exist() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR157(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR157_should_return_false_when_crypto_pki_certification_chains_exist() {
      var blob = new AssetBlob {
        Body = @"
!
crypto pki certificate chain TP-self-signed-1975892096
 certificate self-signed 01 nvram:IOS-Self-Sig#3636.cer
 dot1x system-auth-control
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR157(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR157_should_return_false_when_crypto_pki_trustpoints_exist() {
      var blob = new AssetBlob {
        Body = @"crypto pki trustpoint TP-self-signed-1975892096
 enrollment selfsigned
 subject-name cn=IOS-Self-Signed-Certificate-1975892096
 revocation-check none
 rsakeypair TP-self-signed-1975892096
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR157(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR157_should_return_false_when_crypto_pki_trustpoints_or_certification_chains_exist() {
      var blob = new AssetBlob {
        Body = @"crypto pki trustpoint TP-self-signed-1975892096
 enrollment selfsigned
 subject-name cn=IOS-Self-Signed-Certificate-1975892096
 revocation-check none
 rsakeypair TP-self-signed-1975892096
!
!
crypto pki certificate chain TP-self-signed-1975892096
 certificate self-signed 01 nvram:IOS-Self-Sig#3636.cer
 dot1x system-auth-control
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR157(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}