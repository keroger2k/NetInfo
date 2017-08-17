using System.Collections.Generic;
using System.Net;
using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB021_Tests {
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
    public void RB021_should_return_false_when_they_are_not_in_same_order() {
      var blob = new AssetBlob {
        Body = @"
 ntp server 10.0.16.152 version ""1""
 ntp server 10.32.9.233 version ""1""
 ntp server 10.16.27.44 version ""1""
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB021(device, address);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void RB021_should_return_true_when_all_the_nms_servers_match_configured_servers() {
      var blob = new AssetBlob {
        Body = @"
 ntp server 10.32.9.233 version ""1""
 ntp server 10.0.16.152 version ""1""
 ntp server 10.16.27.44 version ""1""
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB021(device, address);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB021_should_return_false_when_not_all_tacacs_servers_have_been_configured() {
      var blob = new AssetBlob {
        Body = @"
 ntp server 10.32.9.233 version ""1""
 ntp server 10.16.27.44 version ""1""
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB021(device, address);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void RB021_should_return_false_when_extrac_tacacs_servers_have_been_configured() {
      var blob = new AssetBlob {
        Body = @"
 ntp server 10.32.9.233 version ""1""
 ntp server 10.0.16.152 version ""1""
 ntp server 10.16.27.44 version ""1""
 ntp server 1.1.1.1 version ""1""
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB021(device, address);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}