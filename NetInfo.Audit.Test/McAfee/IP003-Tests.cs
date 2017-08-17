using System.Collections.Generic;
using System.Net;
using NetInfo.Audit.McAfee;
using NetInfo.Audit.NMCI.Models;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.McAfee {

  [TestFixture]
  public class IP003_Tests {

    [Test]
    public void IP003_should_return_true_when_device_ip_address_falls_within_subnet_range_of_allowed_addresses() {
      var blob = new AssetBlob {
        Body = @"[Sensor Network Config]
IP Address : 10.24.192.110
Netmask : 255.255.255.224
Default Gateway : 10.24.192.97
SSH Remote Logins : enabled

"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP003(device, allowedAddresses: new List<Subnet> {
        new Subnet { NetworkAddress = IPAddress.Parse("10.24.192.96"), NetworkMask = IPAddress.Parse("255.255.255.224") }
      });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IP003_should_return_false_when_device_ip_address_does_not_falls_within_subnet_range_of_allowed_addresses() {
      var blob = new AssetBlob {
        Body = @"[Sensor Network Config]
IP Address : 10.24.192.135
Netmask : 255.255.255.224
Default Gateway : 10.24.192.129
SSH Remote Logins : enabled

"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP003(device, allowedAddresses: new List<Subnet> {
        new Subnet { NetworkAddress = IPAddress.Parse("10.24.192.96"), NetworkMask = IPAddress.Parse("255.255.255.224") }
      });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}