using System.Collections.Generic;
using System.Net;
using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS027_Tests {

    private IEnumerable<IPAddress> approvedServers = new List<IPAddress> {
      IPAddress.Parse("1.1.1.1")
    };

    [Test]
    public void BS027_should_return_true_when_the_list_of_approved_servers_matches_the_logging_settings() {
      var blob = new AssetBlob {
        Body = @"logging host 1.1.1.1"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS027(device, approvedServers);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS027_should_ensure_duplicate_entries_are_removed() {
      var blob = new AssetBlob {
        Body = @"logging host 1.1.1.1
logging host 1.1.1.1"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS027(device, approvedServers);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS027_should_return_false_when_there_are_more_logging_servers_than_approved() {
      var blob = new AssetBlob {
        Body = @"logging host 1.1.1.1
logging host 2.2.2.2"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS027(device, approvedServers);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS027_should_return_false_when_no_logging_servers_are_found() {
      var blob = new AssetBlob {
        Body = @""
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS027(device, approvedServers);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}