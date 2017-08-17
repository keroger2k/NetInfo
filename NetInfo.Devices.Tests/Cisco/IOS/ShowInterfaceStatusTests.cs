using System.Linq;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class ShowInterfaceStatusTests {

    [Test]
    public void show_interface_status_correctly_parses_gigabit_interface_types() {
      var status = new ShowInterfaceStatus(new string[] {
        @"Gi1/1 <== DISABLED ==> disabled 2 full 1000 No Gbic",
      });

      var result = status.Interfaces.First();
      Assert.AreEqual(ShowInterfaceStatus.InterfaceTypes.Gi,
        result.InterfaceType);
      Assert.AreEqual("<== DISABLED ==>", result.Description);
      Assert.AreEqual(ShowInterfaceStatus.InterfaceStatus.disabled, result.Status);
      Assert.AreEqual("2", result.Vlan);
      Assert.AreEqual(ShowInterfaceStatus.InterfaceDuplex.full, result.Duplex);
      Assert.AreEqual("1000", result.Speed);
      Assert.AreEqual("No Gbic", result.Type);
    }

    [Test]
    public void show_interface_status_correctly_parses_fastethernet_interface_types() {
      var status = new ShowInterfaceStatus(new string[] {
        @"Fa3/10 <== DISABLED ==> disabled 2 auto auto 10/100BaseTX",
      });

      var result = status.Interfaces.First();
      Assert.AreEqual(ShowInterfaceStatus.InterfaceTypes.Fa,
        result.InterfaceType);
      Assert.AreEqual("<== DISABLED ==>", result.Description);
      Assert.AreEqual(ShowInterfaceStatus.InterfaceStatus.disabled, result.Status);
      Assert.AreEqual("2", result.Vlan);
      Assert.AreEqual(ShowInterfaceStatus.InterfaceDuplex.auto, result.Duplex);
      Assert.AreEqual("auto", result.Speed);
      Assert.AreEqual("10/100BaseTX", result.Type);
    }

    [Test]
    public void show_interface_status_correctly_parses_ten_gigabit_interface_types() {
      var status = new ShowInterfaceStatus(new string[] {
        @"Te3/1 <== DISABLED ==> connected routed full 10G 10Gbase-ZR",
      });

      var result = status.Interfaces.First();
      Assert.AreEqual(ShowInterfaceStatus.InterfaceTypes.Te,
        result.InterfaceType);
      Assert.AreEqual("<== DISABLED ==>", result.Description);
      Assert.AreEqual(ShowInterfaceStatus.InterfaceStatus.connected, result.Status);
      Assert.AreEqual("routed", result.Vlan);
      Assert.AreEqual(ShowInterfaceStatus.InterfaceDuplex.full, result.Duplex);
      Assert.AreEqual("10G", result.Speed);
      Assert.AreEqual("10Gbase-ZR", result.Type);
    }

    [Test]
    public void show_interface_status_correctly_parses_port_channel_interface_types() {
      var status = new ShowInterfaceStatus(new string[] {
        @"Po1 <== DISABLED ==> connected trunk a-full a-1000",
      });

      var result = status.Interfaces.First();
      Assert.AreEqual(ShowInterfaceStatus.InterfaceTypes.Po,
        result.InterfaceType);
      Assert.AreEqual("<== DISABLED ==>", result.Description);
      Assert.AreEqual(ShowInterfaceStatus.InterfaceStatus.connected, result.Status);
      Assert.AreEqual("trunk", result.Vlan);
      Assert.AreEqual(ShowInterfaceStatus.InterfaceDuplex.full, result.Duplex);
      Assert.AreEqual("1000", result.Speed);
      Assert.AreEqual("", result.Type);
    }

    [Test]
    public void show_interface_status_should_be_able_to_parse_interfaces_that_have_just_a_number_without_a_slash_for_module_slash_port() {
      var status = new ShowInterfaceStatus(new string[] {
        @"Fa0 <== DISABLED ==> connected 272 a-half 100 10/100BaseTX",
      });

      var result = status.Interfaces.First();
      Assert.AreEqual(ShowInterfaceStatus.InterfaceTypes.Fa,
        result.InterfaceType);
      Assert.AreEqual("<== DISABLED ==>", result.Description);
      Assert.AreEqual(ShowInterfaceStatus.InterfaceStatus.connected, result.Status);
      Assert.AreEqual("272", result.Vlan);
      Assert.AreEqual(ShowInterfaceStatus.InterfaceDuplex.half, result.Duplex);
      Assert.AreEqual("100", result.Speed);
      Assert.AreEqual("10/100BaseTX", result.Type);
    }

    [Test]
    public void show_interface_status_should_be_able_to_parse_interfaces_that_have_a_status_of_faulty() {
      var status = new ShowInterfaceStatus(new string[] {
        @"Fa3/25    01-000000227-002D1 faulty       210          auto   auto 10/100BaseTX",
      });

      var result = status.Interfaces.First();
      Assert.AreEqual(ShowInterfaceStatus.InterfaceTypes.Fa,
        result.InterfaceType);
      Assert.AreEqual("01-000000227-002D1", result.Description);
      Assert.AreEqual(ShowInterfaceStatus.InterfaceStatus.faulty, result.Status);
      Assert.AreEqual("210", result.Vlan);
      Assert.AreEqual(ShowInterfaceStatus.InterfaceDuplex.auto, result.Duplex);
      Assert.AreEqual("auto", result.Speed);
      Assert.AreEqual("10/100BaseTX", result.Type);
    }
  }
}