using System.Linq;
using NetInfo.Devices.Brocade.BOS.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Brocade.BOS {

  [TestFixture]
  public class ShowInterfaceBriefTests {

    [Test]
    public void show_interface_correctly_parses_this_line_1() {
      var status = new ShowInterfaceBrief(new string[] {
        @"1/1/1   Up      Forward Full 1G    None  Yes N/A  0   748e.f82e.30c0  U00_IR01",
      });

      var result = status.Interfaces.First();
      Assert.AreEqual("1/1/1", result.Port);
      Assert.AreEqual(ShowInterfaceBrief.LinkStatus.Up, result.Link);
      Assert.AreEqual(ShowInterfaceBrief.StateStatus.Forward, result.State);
      Assert.AreEqual(ShowInterfaceBrief.DuplexStatus.Full, result.Duplex);
      Assert.AreEqual(ShowInterfaceBrief.SpeedStatus.Gigabit, result.Speed);
      Assert.False(result.Trunk);
      Assert.True(result.Tag.Value);
      Assert.AreEqual("N/A", result.Pvid);
      Assert.AreEqual("0", result.Pri);
      Assert.AreEqual("748e.f82e.30c0", result.MAC);
      Assert.AreEqual("U00_IR01", result.Name);
    }

    [Test]
    public void show_interface_correctly_parses_this_line_2() {
      var status = new ShowInterfaceBrief(new string[] {
        @"mgmt1   Disable None    None None  None  No  None 0   748e.f82e.30c0          ",
      });

      var result = status.Interfaces.First();
      Assert.AreEqual("mgmt1", result.Port);
      Assert.AreEqual(ShowInterfaceBrief.LinkStatus.Disable, result.Link);
      Assert.AreEqual(ShowInterfaceBrief.StateStatus.None, result.State);
      Assert.AreEqual(ShowInterfaceBrief.DuplexStatus.None, result.Duplex);
      Assert.AreEqual(ShowInterfaceBrief.SpeedStatus.None, result.Speed);
      Assert.False(result.Trunk);
      Assert.False(result.Tag.Value);
      Assert.AreEqual("None", result.Pvid);
      Assert.AreEqual("0", result.Pri);
      Assert.AreEqual("748e.f82e.30c0", result.MAC);
      Assert.AreEqual("", result.Name);
    }

    [Test]
    public void show_interface_correctly_parses_this_line_3() {
      var status = new ShowInterfaceBrief(new string[] {
        @"ve99    Up      N/A     N/A  N/A   None  N/A N/A  N/A 748e.f82e.30c0  <== Mana",
      });

      var result = status.Interfaces.First();
      Assert.AreEqual("ve99", result.Port);
      Assert.AreEqual(ShowInterfaceBrief.LinkStatus.Up, result.Link);
      Assert.AreEqual(ShowInterfaceBrief.StateStatus.NA, result.State);
      Assert.AreEqual(ShowInterfaceBrief.DuplexStatus.NA, result.Duplex);
      Assert.AreEqual(ShowInterfaceBrief.SpeedStatus.NA, result.Speed);
      Assert.False(result.Trunk);
      Assert.IsNull(result.Tag);
      Assert.AreEqual("N/A", result.Pvid);
      Assert.AreEqual("N/A", result.Pri);
      Assert.AreEqual("748e.f82e.30c0", result.MAC);
      Assert.AreEqual("<== Mana", result.Name);
    }

    [Test]
    public void show_interface_correctly_parses_16GB_interface_with_blank_names() {
      var status = new ShowInterfaceBrief(@"1/1/48  Up      Forward Full 100M  None  No  430  0   748e.f87f.5f2f  239A-45D
1/2/1   Up      Forward Full 16G   None  No  N/A  0   748e.f87f.5f31
1/2/2   Up      Forward Full 16G   None  No  N/A  0   748e.f87f.5f32
2/1/1   Up      Forward Full 1G    None  Yes N/A  0   748e.f87f.5f00  U03_DR02".ToConfig());

      Assert.AreEqual(4, status.Interfaces.Where(c => c != null).Count());
    }
  }
}