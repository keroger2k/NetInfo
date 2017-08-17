using System;
using NetInfo.Devices.Brocade.BOS.Classes;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Brocade.BOS {

  [TestFixture]
  public class BOSInterfaceTests {

    [Test]
    public void bos_interface_will_throw_an_exception_when_it_finds_an_unidentified_interface() {
      try {
        var rinter = new BOSInterface(new string[] {
        "interface Kyle56",
        "!"
      });
      } catch (NotImplementedException ex) {
        Assert.AreEqual("Unknown Interface", ex.Message);
      }
    }

    [Test]
    public void bos_interface_will_correctly_identifies_the_type_of_interface() {
      var inter = new BOSInterface(new string[] {
        "interface ethernet 1/1/29",
        "!"
      });

      Assert.AreEqual("ethernet", inter.Type);
    }

    [Test]
    public void bos_interface_will_correctly_identifies_the_description_of_interface() {
      var inter = new BOSInterface(@"interface ethernet 1/1/30
 port-name DISABLED
 disable
 speed-duplex 100-full
 no snmp-server enable traps link-change
!".ToConfig());

      Assert.AreEqual("DISABLED", inter.PortName);
    }

    [Test]
    public void bos_interface_will_correctly_identifies_if_an_interface_is_shutdown() {
      var inter = new BOSInterface(@"interface ethernet 1/1/30
 port-name DISABLED
 disable
 speed-duplex 100-full
 no snmp-server enable traps link-change
!".ToConfig());

      Assert.False(inter.Enabled);
    }

    [Test]
    public void bos_interface_will_correctly_identifies_if_an_interface_is_enabled() {
      var inter = new BOSInterface(@"interface ethernet 1/1/30
 port-name DISABLED
 speed-duplex 100-full
 no snmp-server enable traps link-change
!".ToConfig());

      Assert.True(inter.Enabled);
    }

    [TestCase(@"interface ve 99")]
    public void bos_interface_knows_if_it_is_not_a_physical_interface(string config) {
      var inter = new BOSInterface(config.ToConfig());
      Assert.False(inter.Physical);
    }

    [Test]
    public void bos_interface_can_get_correct_name() {
      var inter = new BOSInterface(@"interface ve 1".ToConfig());
      Assert.AreEqual("ve", inter.Type);
      Assert.AreEqual("1", inter.Number);
    }

    [TestCase(@"interface ethernet 1/1/1")]
    public void bos_interface_knows_if_it_is_a_physical_interface(string config) {
      var inter = new BOSInterface(config.ToConfig());
      Assert.True(inter.Physical);
    }

    [Test]
    public void bos_interface_will_correctly_identifies_ip_address_on_virtual_interface() {
      var inter = new BOSInterface(@"interface ve 99
 port-name <== Management VLAN ==>
 ip address 10.26.97.20 255.255.255.128
 ip proxy-arp disable
 no ip redirect
!".ToConfig());

      Assert.AreEqual("10.26.97.20", inter.Address.NetworkAddress.ToString());
      Assert.AreEqual("255.255.255.128", inter.Address.NetworkMask.ToString());
    }
  }
}