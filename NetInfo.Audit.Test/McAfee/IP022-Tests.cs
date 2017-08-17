using System.Collections.Generic;
using System.Net;
using NetInfo.Audit.McAfee;
using NetInfo.Devices;
using NetInfo.Devices.McAfee;
using NetInfo.Devices.NMCI.McAfee;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.McAfee {

  [TestFixture]
  public class IP022_Tests {

    [Test]
    public void IP022_should_return_true_when_all_the_networks_match_the_ssh_access_control_networks() {
      var blob = new AssetBlob {
        Body = @"Network 1: 10.16.6.64/27
Network 2: 10.16.27.32/27
Network 3: 10.0.18.240/29
Network 4: 10.0.16.128/27
Network 5: 10.32.9.224/27"
      };

      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP022(device, new List<McAfeeDevice.Network> {
        new McAfeeDevice.Network { Address = IPAddress.Parse("10.16.6.64"),  Netmask = 27, Number = 1 },
        new McAfeeDevice.Network { Address = IPAddress.Parse("10.16.27.32"), Netmask = 27, Number = 2 },
        new McAfeeDevice.Network { Address = IPAddress.Parse("10.0.18.240"), Netmask = 29, Number = 3 },
        new McAfeeDevice.Network { Address = IPAddress.Parse("10.0.16.128"), Netmask = 27, Number = 4 },
        new McAfeeDevice.Network { Address = IPAddress.Parse("10.32.9.224"), Netmask = 27, Number = 5 },
      });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IP022_should_return_false_when_networks_are_missing() {
      var blob = new AssetBlob {
        Body = @"Network 1: 10.16.6.64/27
Network 2: 10.16.27.32/27
Network 3: 10.0.18.240/29
Network 5: 10.32.9.224/27"
      };

      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP022(device, new List<McAfeeDevice.Network> {
        new McAfeeDevice.Network { Address = IPAddress.Parse("10.16.6.64"),  Netmask = 27, Number = 1 },
        new McAfeeDevice.Network { Address = IPAddress.Parse("10.16.27.32"), Netmask = 27, Number = 2 },
        new McAfeeDevice.Network { Address = IPAddress.Parse("10.0.18.240"), Netmask = 29, Number = 3 },
        new McAfeeDevice.Network { Address = IPAddress.Parse("10.0.16.128"), Netmask = 27, Number = 4 },
        new McAfeeDevice.Network { Address = IPAddress.Parse("10.32.9.224"), Netmask = 27, Number = 5 },
      });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IP022_should_return_false_when_there_are_extra_networks() {
      var blob = new AssetBlob {
        Body = @"Network 1: 10.16.6.64/27
Network 2: 10.16.27.32/27
Network 3: 10.0.18.240/29
Network 4: 10.0.16.128/27
Network 5: 10.32.9.224/27
Network 6: 1.1.1.1/32"
      };

      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP022(device, new List<McAfeeDevice.Network> {
        new McAfeeDevice.Network { Address = IPAddress.Parse("10.16.6.64"),  Netmask = 27, Number = 1 },
        new McAfeeDevice.Network { Address = IPAddress.Parse("10.16.27.32"), Netmask = 27, Number = 2 },
        new McAfeeDevice.Network { Address = IPAddress.Parse("10.0.18.240"), Netmask = 29, Number = 3 },
        new McAfeeDevice.Network { Address = IPAddress.Parse("10.0.16.128"), Netmask = 27, Number = 4 },
        new McAfeeDevice.Network { Address = IPAddress.Parse("10.32.9.224"), Netmask = 27, Number = 5 },
      });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}