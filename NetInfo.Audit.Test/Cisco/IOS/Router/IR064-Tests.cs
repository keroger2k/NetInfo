using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR064_Tests {

    [Test]
    public void IR064_should_return_true_when_a_device_has_snmp_v3_configured_and_no_community_strings() {
      var blob = new AssetBlob {
        Body = @"snmp-server engineID remote 10.0.16.134 515C41E69D2F0022F21D94FA
snmp-server group nmcigroup v3 priv write NMS notify *tv.FFFFFFFF.FFFFFFFF.FFFFFFFF.FFFFFFFF.FFFFFFFF0F access 69"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1665(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR064_should_return_false_when_a_device_has_snmp_v3_configured_and_community_strings() {
      var blob = new AssetBlob {
        Body = @"snmp-server engineID remote 10.0.16.134 515C41E69D2F0022F21D94FA
snmp-server group nmcigroup v3 priv write NMS notify *tv.FFFFFFFF.FFFFFFFF.FFFFFFFF.FFFFFFFF.FFFFFFFF0F access 69
snmp-server community jPC$!wEWxs57 RO 99
snmp-server community sPWx4$sC9!wE RO 99"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1665(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}