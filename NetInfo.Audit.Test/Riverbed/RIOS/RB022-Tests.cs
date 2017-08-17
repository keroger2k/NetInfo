using System.Collections.Generic;
using System.Net;
using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB022_Tests {
    private IEnumerable<IPAddress> address;

    [SetUp]
    public void Init() {
      this.address = new List<IPAddress> {
        IPAddress.Parse("10.32.9.233"),
        IPAddress.Parse("10.0.16.152"),
        IPAddress.Parse("10.16.27.44")
      };
    }

    [Test]
    public void RB022_should_return_true_when_they_are_not_in_same_order() {
      var blob = new AssetBlob {
        Body = @"
 snmp-server host 10.32.9.233 traps version 1 LgsA!5!erQ6E
 snmp-server host 10.16.27.44 traps version 1 LgsA!5!erQ6E
 snmp-server host 10.0.16.152 traps version 1 LgsA!5!erQ6E
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB022(device, address);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB022_should_return_true_when_all_the_nms_servers_match_configured_servers() {
      var blob = new AssetBlob {
        Body = @"
 snmp-server host 10.32.9.233 traps version 1 LgsA!5!erQ6E
 snmp-server host 10.0.16.152 traps version 1 LgsA!5!erQ6E
 snmp-server host 10.16.27.44 traps version 1 LgsA!5!erQ6E
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB022(device, address);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB022_should_return_false_when_not_all_snmp_servers_have_been_configured() {
      var blob = new AssetBlob {
        Body = @"
 snmp-server host 10.32.9.233 traps version 1 LgsA!5!erQ6E
 snmp-server host 10.0.16.152 traps version 1 LgsA!5!erQ6E
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB022(device, address);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void RB022_should_return_false_when_extra_snmp_servers_have_been_configured() {
      var blob = new AssetBlob {
        Body = @"
 snmp-server host 10.32.9.233 traps version 1 LgsA!5!erQ6E
 snmp-server host 10.0.16.152 traps version 1 LgsA!5!erQ6E
 snmp-server host 10.16.27.44 traps version 1 LgsA!5!erQ6E
 snmp-server host 1.1.1.1 traps version 1 LgsA!5!erQ6E
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB022(device, address);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}