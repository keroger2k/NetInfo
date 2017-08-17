using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP079_Tests {

    [Test]
    public void VP079_should_return_true_when_snmp_vpn_is_found() {
      var blob = new AssetBlob {
        Body = @"set snmp vpn"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP079(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP079_should_return_true_when_snmp_vpn_is_not_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP079(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}