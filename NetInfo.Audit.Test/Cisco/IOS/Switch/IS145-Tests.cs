using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS145_Tests {

    [Test]
    public void IS145_should_return_true_when_all_ntp_servers_have_authentication_keys_configured() {
      var blob = new AssetBlob {
        Body = @"!
ntp authentication-key 1 md5 150B434543231A273D7D073B0728432C 7
ntp authenticate
ntp trusted-key 1
ntp clock-period 36029677
ntp source Vlan30
ntp server 10.32.9.254 key 1
ntp server 10.0.16.10 key 1
ntp server 10.16.27.68 key 1
end"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS145(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS145_should_return_false_when_not_all_ntp_servers_have_authentication_keys_configured() {
      var blob = new AssetBlob {
        Body = @"!
ntp authentication-key 1 md5 150B434543231A273D7D073B0728432C 7
ntp authenticate
ntp trusted-key 1
ntp clock-period 36029677
ntp source Vlan30
ntp server 10.32.9.254 key 1
ntp server 10.0.16.10 key 1
ntp server 10.16.27.68
end"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS145(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}