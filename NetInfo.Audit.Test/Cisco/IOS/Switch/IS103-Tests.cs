using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS103_Tests {

    [Test]
    public void IS103_should_return_true_when_tacacs_key_matches_correctly() {
      var blob = new AssetBlob {
        Body = @"tacacs-server host 10.32.9.233
tacacs-server directed-request
tacacs-server key 7 091E5D584A0D20370E4D400C0D
radius-server attribute 32 include-in-access-req
radius-server host 10.0.11.149 auth-port 1812 acct-port 1813 key 7 133F1D0723165379180838"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS103(device, new[] { "2s13hWEe!$FF", "old" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS103_should_return_false_when_tacacs_key_does_not_match() {
      var blob = new AssetBlob {
        Body = @"tacacs-server host 10.32.9.233
tacacs-server directed-request
tacacs-server key 7 091E5D584A0D20370E4D400C0D
radius-server attribute 32 include-in-access-req
radius-server host 10.0.11.149 auth-port 1812 acct-port 1813 key 7 133F1D0723165379180838"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS103(device, new[] { "FAIL", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}