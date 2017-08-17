using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS026_Tests {

    [Test]
    public void BS026_should_return_false_when_no_snmp_server_group_found() {
      var blob = new AssetBlob {
        Body = @"
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS026(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS026_should_return_true_when_all_snmp_server_groups_are_configured_with_access_69() {
      var blob = new AssetBlob {
        Body = @"
snmp-server group nmcigroup v3 priv access 69 read all write all notify all
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS026(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS026_should_return_true_when_all_snmp_server_groups_are_configured_with_access_69_in_short_format() {
      var blob = new AssetBlob {
        Body = @"
snmp-server group nmcigroup v3 priv access 69 read all write all
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS026(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS026_should_return_true_when_all_snmp_server_groups_are_configured_with_access_69_multiple() {
      var blob = new AssetBlob {
        Body = @"
snmp-server group nmcigroup v3 priv access 69 read all write all notify all
snmp-server group nmcigroup v3 priv access 69 read all write all notify all
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS026(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS026_should_return_false_when_not_all_snmp_server_groups_are_configured_with_access_69() {
      var blob = new AssetBlob {
        Body = @"
snmp-server group nmcigroup v3 priv access 1 read all write all notify all
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS026(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS026_should_return_false_when_not_all_snmp_server_groups_are_configured_with_access_69_multiple() {
      var blob = new AssetBlob {
        Body = @"
snmp-server group nmcigroup v3 priv access 1 read all write all notify all
snmp-server group nmcigroup v3 priv access 69 read all write all notify all
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS026(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}