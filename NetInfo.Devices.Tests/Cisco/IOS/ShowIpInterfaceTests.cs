using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class ShowIpInterfaceTests {

    #region Example show ip interface

    private IEnumerable<string> config = @"Vlan1 is administratively down, line protocol is down
  Internet protocol processing disabled
Vlan30 is up, line protocol is up
  Internet address is 10.46.4.65/30
  Broadcast address is 255.255.255.255
  Address determined by non-volatile memory
  MTU is 1500 bytes
  Helper address is not set
  Directed broadcast forwarding is disabled
  Outgoing access list is not set
  Inbound  access list is not set
  Proxy ARP is disabled
  Local Proxy ARP is disabled
  Security level is default
  Split horizon is enabled
  ICMP redirects are never sent
  ICMP unreachables are always sent
  ICMP mask replies are never sent
  IP fast switching is enabled
  IP CEF switching is enabled
  IP CEF switching turbo vector
  IP Null turbo vector
  IP multicast fast switching is enabled
  IP multicast distributed fast switching is disabled
  IP route-cache flags are Fast, CEF
  Router Discovery is disabled
  IP output packet accounting is disabled
  IP access violation accounting is disabled
  TCP/IP header compression is disabled
  RTP/IP header compression is disabled
  Probe proxy name replies are disabled
  Policy routing is disabled
  Network address translation is disabled
  BGP Policy Mapping is disabled
  Output features: Check hwidb
  WCCP Redirect outbound is disabled
  WCCP Redirect inbound is disabled
  WCCP Redirect exclude is disabled
Vlan210 is up, line protocol is up
  Internet address is 10.46.4.97/27
  Broadcast address is 255.255.255.255
  Address determined by non-volatile memory
  MTU is 1500 bytes
  Helper addresses are 10.16.25.23
                       10.16.25.24
  Directed broadcast forwarding is disabled
  Outgoing access list is not set
  Inbound  access list is not set
  Proxy ARP is disabled
  Local Proxy ARP is disabled
  Security level is default
  Split horizon is enabled
  ICMP redirects are never sent
  ICMP unreachables are always sent
  ICMP mask replies are never sent
  IP fast switching is enabled
  IP CEF switching is enabled
  IP CEF switching turbo vector
  IP Null turbo vector
  IP multicast fast switching is enabled
  IP multicast distributed fast switching is disabled
  IP route-cache flags are Fast, CEF
  Router Discovery is disabled
  IP output packet accounting is disabled
  IP access violation accounting is disabled
  TCP/IP header compression is disabled
  RTP/IP header compression is disabled
  Probe proxy name replies are disabled
  Policy routing is disabled
  Network address translation is disabled
  BGP Policy Mapping is disabled
  Output features: Check hwidb
  WCCP Redirect outbound is disabled
  WCCP Redirect inbound is disabled
  WCCP Redirect exclude is disabled
Vlan500 is up, line protocol is up
  Internet address is 10.46.4.73/29
  Broadcast address is 255.255.255.255
  Address determined by non-volatile memory
  MTU is 1500 bytes
  Helper address is not set
  Directed broadcast forwarding is disabled
  Outgoing access list is NMCI_Printer_VLAN_ACL_OUT_V17-2-0
  Inbound  access list is NMCI_Printer_VLAN_ACL_IN_V17-2-0
  Proxy ARP is disabled
  Local Proxy ARP is disabled
  Security level is default
  Split horizon is enabled
  ICMP redirects are never sent
  ICMP unreachables are always sent
  ICMP mask replies are never sent
  IP fast switching is enabled
  IP CEF switching is enabled
  IP CEF switching turbo vector
  IP Null turbo vector
  IP multicast fast switching is enabled
  IP multicast distributed fast switching is disabled
  IP route-cache flags are Fast, CEF
  Router Discovery is disabled
  IP output packet accounting is disabled
  IP access violation accounting is disabled
  TCP/IP header compression is disabled
  RTP/IP header compression is disabled
  Probe proxy name replies are disabled
  Policy routing is disabled
  Network address translation is disabled
  BGP Policy Mapping is disabled
  Input features: Access List
  Output features: Access List, Check hwidb
  WCCP Redirect outbound is disabled
  WCCP Redirect inbound is disabled
  WCCP Redirect exclude is disabled
FastEthernet0/1 is up, line protocol is up
  Inbound  access list is not set
FastEthernet0/2 is administratively down, line protocol is down
  Inbound  access list is not set
GigabitEthernet0/1 is administratively down, line protocol is down
  Inbound  access list is not set
GigabitEthernet0/2 is administratively down, line protocol is down
  Inbound  access list is not set".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

    #endregion Example show ip interface

    [Test]
    public void show_ip_interface_should_correctly_determine_the_number_of_interfaces() {
      var show = new ShowIpInterface(config);

      Assert.AreEqual(8, show.Interfaces.Count());
    }

    [Test]
    public void show_ip_interface_can_correctly_identify_all_interface_names() {
      var show = new ShowIpInterface(config);

      Assert.AreEqual("Vlan1", show.Interfaces.First().Name);
    }

    [Test]
    public void show_ip_interface_can_correctly_determine_if_interface_is_shutdown() {
      var show = new ShowIpInterface(config);

      Assert.True(show.Interfaces.First().Shutdown);
      Assert.False(show.Interfaces.ElementAt(1).Shutdown);
    }

    [Test]
    public void show_ip_interface_can_correctly_determine_which_interfaces_have_ip_addresses_configured() {
      var show = new ShowIpInterface(config);

      Assert.IsNull(show.Interfaces.First().InternetAddress);
      Assert.AreEqual("10.46.4.65", show.Interfaces.ElementAt(1).InternetAddress.ToString());
    }

    [Test]
    public void show_ip_interface_can_correctly_determine_if_interface_has_cef_switching_enabled() {
      var show = new ShowIpInterface(config);

      Assert.False(show.Interfaces.ElementAt(0).IpCefSwitchingEanbled);
      Assert.True(show.Interfaces.ElementAt(1).IpCefSwitchingEanbled);
    }
  }
}