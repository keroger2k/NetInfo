using System;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class IOSInterfaceTests {
    private IOSInterface inter;

    [Test]
    public void ios_interface_will_throw_an_exception_when_it_finds_an_unidentified_interface() {
      try {
        inter = new IOSInterface(new string[] {
        "interface Kyle56",
        "!"
      });
      } catch (NotImplementedException ex) {
        Assert.AreEqual("Unknown Interface", ex.Message);
      }
    }

    [Test]
    public void ios_interface_will_correctly_identifies_the_type_of_interface() {
      inter = new IOSInterface(new string[] {
        "interface GigabitEthernet5/6",
        "!"
      });

      Assert.AreEqual("GigabitEthernet", inter.Type);
    }

    [Test]
    public void ios_interface_will_correctly_identifies_the_description_of_interface() {
      inter = new IOSInterface(new string[] {
        "interface GigabitEthernet5/6",
        " description <=== TEST ===>",
        "!"
      });

      Assert.AreEqual("<=== TEST ===>", inter.Description);
    }

    [Test]
    public void ios_interface_will_correctly_identifies_if_an_interface_is_shutdown() {
      var str = @"interface GigabitEthernet5/6
 description <=== TEST ===>
 shutdown
!".ToConfig();

      inter = new IOSInterface(str);

      Assert.AreEqual(true, inter.Shutdown);
    }

    [Test]
    public void ios_interface_will_correctly_identifies_if_an_interface_is_enabled() {
      var str = @"interface GigabitEthernet5/6
 description <=== TEST ===>
!".ToConfig();

      inter = new IOSInterface(str);

      Assert.AreEqual(false, inter.Shutdown);
    }

    [Test]
    public void ios_interface_will_correctly_identifies_ospf_message_digest_key() {
      var str = @"interface Vlan26
  ip ospf message-digest-key 1 md5 7 02205C6D3E52221B15453C2E2E130A5C18
!".ToConfig();

      inter = new IOSInterface(str);

      Assert.AreEqual("02205C6D3E52221B15453C2E2E130A5C18", inter.IP.OSPF.MessageDigest);
    }

    [Test]
    public void ios_interface_can_correctly_identify_ospf_cost() {
      var str = @"interface Vlan26
  ip ospf cost 5
!".ToConfig();

      inter = new IOSInterface(str);

      Assert.AreEqual(5, inter.IP.OSPF.Cost);
    }

    [TestCase(@"interface Vlan0")]
    [TestCase(@"interface Tunnel0")]
    [TestCase(@"interface Port-channel0")]
    [TestCase(@"interface Loopback0")]
    public void ios_interface_knows_if_it_is_not_a_physical_interface(string config) {
      inter = new IOSInterface(config.ToConfig());
      Assert.AreEqual(false, inter.Physical);
    }

    [TestCase(@"interface FastEthernet0/0")]
    [TestCase(@"interface GigabitEthernet0/0")]
    [TestCase(@"interface TenGigabitEthernet0/0")]
    public void ios_interface_knows_if_it_is_a_physical_interface(string config) {
      inter = new IOSInterface(config.ToConfig());
      Assert.AreEqual(true, inter.Physical);
    }

    [Test]
    public void ios_interface_will_return_vlan_1_for_default_vlan_assignment() {
      var str = @"interface FastEthernet1/3
!".ToConfig();

      inter = new IOSInterface(str);

      Assert.AreEqual(1, inter.Vlan);
    }

    [Test]
    public void ios_interface_will_return_correct_vlan_assignment() {
      var str = @"interface FastEthernet1/3
  switchport access vlan 2
!".ToConfig();

      inter = new IOSInterface(str);

      Assert.AreEqual(2, inter.Vlan);
    }

    [Test]
    public void ios_interface_can_identifiy_ip_unreachable_command() {
      var str = @"interface Vlan97
 description <== iBGP VLAN 97 ==>
 ip address 138.163.132.5 255.255.255.252
 no ip redirects
 no ip unreachables
 no ip proxy-arp
!
".ToConfig();

      inter = new IOSInterface(str);

      Assert.False(inter.IP.Unreachables);
    }

    [Test]
    public void ios_interface_can_parse_authentication_commands() {
      var str = @"interface Vlan97
 description <== iBGP VLAN 97 ==>
 authentication port-control auto
 authentication periodic
 authentication timer restart 30
!
".ToConfig();

      inter = new IOSInterface(str);

      Assert.True(inter.Authentication.PortControlAuto);
      Assert.True(inter.Authentication.Periodic);
      Assert.AreEqual(30, inter.Authentication.TimerReauthenticate);
    }

    [Test]
    public void ios_interface_can_parse_dot1x_commands() {
      var str = @"interface Vlan97
 description <== iBGP VLAN 97 ==>
 dot1x pae authenticator
 dot1x timeout quiet-period 30
!
".ToConfig();

      inter = new IOSInterface(str);

      Assert.True(inter.Dot1x.PAEAuthenticator);
      Assert.AreEqual(30, inter.Dot1x.TimeoutQuietPeriod);
    }
  }
}