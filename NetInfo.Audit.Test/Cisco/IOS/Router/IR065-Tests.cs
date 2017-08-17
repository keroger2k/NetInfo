using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR065_Tests {

    [Test]
    public void IR065_should_return_true_when_a_device_has_the_correct_community_strings_configured() {
      var blob = new AssetBlob {
        Body = @"snmp-server community string1 RO 99
snmp-server community string2 RO 99"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR065(device, new[] { "string1", "string2" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR065_should_return_false_when_a_device_has_a_missing_community_string() {
      var blob = new AssetBlob {
        Body = @"snmp-server community string1 RO 99"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR065(device, new[] { "string1", "string2" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR065_should_return_false_when_a_device_has_an_extra_community_string() {
      var blob = new AssetBlob {
        Body = @"snmp-server community string1 RO 99
snmp-server community string2 RO 99
snmp-server community string3 RO 99"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR065(device, new[] { "string1", "string2" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}