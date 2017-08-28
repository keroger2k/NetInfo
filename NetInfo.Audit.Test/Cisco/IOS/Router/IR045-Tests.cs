using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR045_Tests {

    [Test]
    public void IR045_should_return_true_when_all_ip_address_interfaces_have_cef_enabled() {
      var blob = new AssetBlob {
        Body = @"
ABCD-U00-IR-01#show ip interface
Vlan1 is administratively down, line protocol is down
  Internet protocol processing disabled
Vlan30 is up, line protocol is up
  Internet address is 10.46.4.65/30
  Broadcast address is 255.255.255.255
  Address determined by non-volatile memory
  MTU is 1500 bytes
  IP CEF switching is enabled
  Network address translation is disabled
Vlan210 is up, line protocol is up
  Internet address is 10.46.4.97/27
  Broadcast address is 255.255.255.255
  Address determined by non-volatile memory
  MTU is 1500 bytes
  Helper addresses are 10.16.25.23
  10.16.25.24
  IP CEF switching is enabled
  Network address translation is disabled
Vlan500 is up, line protocol is up
  Internet address is 10.46.4.73/29
  Broadcast address is 255.255.255.255
  Address determined by non-volatile memory
  MTU is 1500 bytes
  Helper address is not set
  IP CEF switching is enabled
  Network address translation is disabled
FastEthernet0/1 is up, line protocol is up
  Inbound access list is not set
ABCD-U00-IR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0949(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR045_should_return_false_when_all_ip_address_interfaces_do_not_have_cef_enabled() {
      var blob = new AssetBlob {
        Body = @"
ABCD-U00-IR-01#show ip interface
Vlan1 is administratively down, line protocol is down
  Internet protocol processing disabled
Vlan30 is up, line protocol is up
  Internet address is 10.46.4.65/30
  Broadcast address is 255.255.255.255
  Address determined by non-volatile memory
  MTU is 1500 bytes
  Helper address is not set
  IP CEF switching is disabled
  Network address translation is disabled
Vlan210 is up, line protocol is up
  Internet address is 10.46.4.97/27
  Broadcast address is 255.255.255.255
  Address determined by non-volatile memory
  MTU is 1500 bytes
  Helper addresses are 10.16.25.23
  10.16.25.24
  IP CEF switching is enabled
  Network address translation is disabled
Vlan500 is up, line protocol is up
  Internet address is 10.46.4.73/29
  Broadcast address is 255.255.255.255
  Address determined by non-volatile memory
  MTU is 1500 bytes
  IP CEF switching is enabled
  Network address translation is disabled
FastEthernet0/1 is up, line protocol is up
  Inbound access list is not set
ABCD-U00-IR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0949(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR045_should_return_true_when_all_ip_address_interfaces_do_not_have_cef_enabled_and_nat_is_enabled() {
      var blob = new AssetBlob {
        Body = @"
BANG-UST-OR-01#show ip interface
Vlan1 is administratively down, line protocol is down
  Internet protocol processing disabled
Vlan51 is up, line protocol is up
  Internet address is 172.16.128.18/28
  Broadcast address is 255.255.255.255
  Address determined by non-volatile memory
  MTU is 1500 bytes
  Helper address is not set
  IP CEF switching is enabled
  Network address translation is disabled
Vlan98 is down, line protocol is down
  Internet address is 164.221.61.2/24
  Broadcast address is 255.255.255.255
  Address determined by non-volatile memory
  MTU is 1500 bytes
  Helper address is not set
  IP CEF switching is disabled
  Network address translation is enabled, interface in domain outside
Vlan141 is up, line protocol is up
  Internet address is 10.8.128.241/28
  Broadcast address is 255.255.255.255
  Address determined by non-volatile memory
  MTU is 1500 bytes
  Helper address is not set
  IP CEF switching is disabled
  Network address translation is enabled, interface in domain inside
BANG-UST-OR-01#!
BANG-UST-OR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0949(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}