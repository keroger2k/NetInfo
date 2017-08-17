using System.Collections.Generic;
using System.Net;
using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP067_Tests {
    private IEnumerable<IPAddress> addresses;

    [SetUp]
    public void Init() {
      addresses = new List<IPAddress> {
        IPAddress.Parse("10.16.27.62"),
        IPAddress.Parse("10.0.16.138"),
        IPAddress.Parse("10.16.27.33"),
        IPAddress.Parse("10.0.16.134")
      };
    }

    [Test]
    public void VP067_should_return_true_when_all_the_correct_snmp_hosts_are_configured() {
      var blob = new AssetBlob {
        Body = @"
set snmp host ""jPC$!wEWxs57"" 10.16.27.62 255.255.255.255 src-interface ethernet0/0 trap v1
set snmp host ""jPC$!wEWxs57"" 10.0.16.138 255.255.255.255 src-interface ethernet0/0 trap v1
set snmp host ""jPC$!wEWxs57"" 10.16.27.33 255.255.255.255 src-interface ethernet0/0 trap v1
set snmp host ""jPC$!wEWxs57"" 10.0.16.134 255.255.255.255 src-interface ethernet0/0 trap v1
"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP067(device, addresses);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP067_should_return_false_when_hosts_are_not_in_correct_sequence() {
      var blob = new AssetBlob {
        Body = @"
set snmp host ""jPC$!wEWxs57"" 10.0.16.138 255.255.255.255 src-interface ethernet0/0 trap v1
set snmp host ""jPC$!wEWxs57"" 10.16.27.62 255.255.255.255 src-interface ethernet0/0 trap v1
set snmp host ""jPC$!wEWxs57"" 10.16.27.33 255.255.255.255 src-interface ethernet0/0 trap v1
set snmp host ""jPC$!wEWxs57"" 10.0.16.134 255.255.255.255 src-interface ethernet0/0 trap v1
"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP067(device, addresses);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP067_should_return_false_when_device_is_missing_host() {
      var blob = new AssetBlob {
        Body = @"
set snmp host ""jPC$!wEWxs57"" 10.16.27.62 255.255.255.255 src-interface ethernet0/0 trap v1
set snmp host ""jPC$!wEWxs57"" 10.16.27.33 255.255.255.255 src-interface ethernet0/0 trap v1
set snmp host ""jPC$!wEWxs57"" 10.0.16.134 255.255.255.255 src-interface ethernet0/0 trap v1
"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP067(device, addresses);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP067_should_return_false_when_device_has_an_extra_host() {
      var blob = new AssetBlob {
        Body = @"
sset snmp host ""jPC$!wEWxs57"" 10.16.27.62 255.255.255.255 src-interface ethernet0/0 trap v1
set snmp host ""jPC$!wEWxs57"" 10.0.16.138 255.255.255.255 src-interface ethernet0/0 trap v1
set snmp host ""jPC$!wEWxs57"" 10.16.27.33 255.255.255.255 src-interface ethernet0/0 trap v1
set snmp host ""jPC$!wEWxs57"" 10.0.16.134 255.255.255.255 src-interface ethernet0/0 trap v1
set snmp host ""jPC$!wEWxs57"" 1.1.1.1 255.255.255.255 src-interface ethernet0/0 trap v1
"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP067(device, addresses);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP067_should_return_false_when_no_hosts_are_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP067(device, addresses);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}