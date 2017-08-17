using System.Linq;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class SwitchPortSettingsTests {

    [Test]
    public void should_correct_parse_allowed_vlans() {
      var setting = new IOSInterface.SwitchPortSettings();
      setting.Settings = @" switchport trunk allowed vlan 91,99
 switchport mode trunk".ToConfig();

      Assert.AreEqual(2, setting.AllowedVlans.Count());
      Assert.AreEqual(91, setting.AllowedVlans.ElementAt(0));
      Assert.AreEqual(99, setting.AllowedVlans.ElementAt(1));
    }

    [Test]
    public void should_be_considered_an_access_port() {
      var setting = new IOSInterface.SwitchPortSettings();
      setting.Settings = @" description User-90-100-01
 switchport access vlan 223
 no ip address
 duplex full
 speed 100
 authentication port-control auto
 authentication periodic
 authentication timer restart 30
 authentication timer reauthenticate server
 no snmp trap link-status
 dot1x pae authenticator
 dot1x timeout quiet-period 30
 no cdp enable
 spanning-tree portfast".ToConfig();

      Assert.AreEqual(IOSInterface.SwitchPortSettings.PortType.Access, setting.Type);
    }

    [Test]
    public void ios_interface_will_return_true_if_port_is_access_port() {
      var inter = new IOSInterface.SwitchPortSettings();
      inter.Settings = @" switchport mode access
!".ToConfig();

      Assert.True(inter.Type == IOSInterface.SwitchPortSettings.PortType.Access);
    }

    [Test]
    public void ios_interface_will_return_true_if_port_is_trunk_port() {
      var inter = new IOSInterface.SwitchPortSettings();
      inter.Settings = @" switchport access vlan 2
 switchport trunk encapsulation dot1q
 switchport trunk allowed vlan 99-1005,1025-4094
 switchport mode dynamic desirable
!".ToConfig();

      Assert.True(inter.Type == IOSInterface.SwitchPortSettings.PortType.Trunk);
    }

    [Test]
    public void ios_interface_will_return_true_if_port_is_trunk_port_and_not_explicitly_set() {
      var inter = new IOSInterface.SwitchPortSettings();
      inter.Settings = @" description <== U01_DR01_G2/16 ==>
 switchport
 switchport trunk encapsulation dot1q
 switchport trunk allowed vlan 1,91,99, 100-200
!".ToConfig();

      Assert.True(inter.Type == IOSInterface.SwitchPortSettings.PortType.Trunk);
    }
  }
}