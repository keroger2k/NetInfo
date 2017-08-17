using System.Linq;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class ShowCdpInterfaceTests {

    [Test]
    public void should_correctly_count_the_number_of_cdp_enabled_interfaces() {
      var sv = new ShowCdpInterface(@"Serial1/0/4:0 is up, line protocol is up
  Encapsulation PPP
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
Serial1/0/5:0 is up, line protocol is up
  Encapsulation PPP
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
GigabitEthernet3/1 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
GigabitEthernet5/1 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
Control Plane Interface is down, line protocol is down
  Encapsulation UNKNOWN
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds".ToConfig());

      Assert.AreEqual(4, sv.Interfaces.Count());
      Assert.True(sv.Interfaces.ElementAt(0).Protocol);
      Assert.True(sv.Interfaces.ElementAt(1).Protocol);
      Assert.True(sv.Interfaces.ElementAt(2).Protocol);
    }
  }
}