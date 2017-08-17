using System.Collections.Generic;
using System.Net;
using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS083_Tests {

    [Test]
    public void IS083_should_return_true_when_the_correct_logging_hosts_are_configured() {
      var blob = new AssetBlob {
        Body = @"logging host 10.16.27.42"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS083(device, new List<IPAddress> { IPAddress.Parse("10.16.27.42") });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS083_should_return_true_when_the_correct_logging_hosts_are_configured_dot1x() {
      var blob = new AssetBlob {
        Body = @"logging host 10.16.27.42 discriminator BLOCK"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS083(device, new List<IPAddress> { IPAddress.Parse("10.16.27.42") });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS083_should_return_false_when_the_incorrect_logging_hosts_are_configured() {
      var blob = new AssetBlob {
        Body = @"logging host 10.16.27.42"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS083(device, new List<IPAddress> { IPAddress.Parse("10.16.27.1") });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS083_should_return_false_when_the_incorrect_logging_hosts_are_configured_dot1x() {
      var blob = new AssetBlob {
        Body = @"logging host 10.16.27.42 discriminator BLOCK"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS083(device, new List<IPAddress> { IPAddress.Parse("10.16.27.1") });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}