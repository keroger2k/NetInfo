using System.Linq;
using NetInfo.Devices.Riverbed.RIOS.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.McAfee.Tests {

  [TestFixture]
  public class ShowInterfaceBriefTests {

    [Test]
    public void correctly_returns_the_model_and_os_release_version() {
      var route = new ShowInterfaceBrief(
        @"Interface wan0_0 state
   Up:                 yes
   Interface type:     ethernet
   Speed:              1000Mb/s (auto)
   Duplex:             full (auto)
   MTU:                1420
   HW address:         00:0E:B6:84:8F:6A
   Link:               yes

Interface lan0_0 state
   Up:                 no
   Interface type:     ethernet
   Speed:              UNKNOWN
   Duplex:             UNKNOWN
   MTU:                1420
   HW address:         00:0E:B6:84:8F:6B
   Link:               no

Interface primary state
   Up:                 yes
   Interface type:     ethernet
   Netmask:            255.255.254.0
   IP address:         10.32.8.65
   Speed:              100Mb/s
   Duplex:             full
   MTU:                1500
   HW address:         00:0E:B6:35:44:78
   Link:               yes

Interface aux state
   Up:                 no
   Interface type:     ethernet
   Netmask:
   IP address:
   Speed:              UNKNOWN
   Duplex:             half (auto)
   MTU:                1500
   HW address:         00:0E:B6:35:44:79
   Link:               no

Interface lo state
   Up:                 yes
   Interface type:     loopback
   Netmask:            255.0.0.0
   IP address:         127.0.0.1
   Speed:              N/A
   Duplex:             N/A
   MTU:                16436
   HW address:         N/A

Interface inpath0_0 state
   Up:                 yes
   Interface type:     ethernet
   Netmask:            255.255.255.240
   IP address:         10.32.1.23
   MTU:                1420
   HW address:         00:0E:B6:84:8F:6A

".ToConfig()
      );

      Assert.AreEqual(6, route.Interfaces.Count());
      Assert.False(route.Interfaces.ElementAt(3).Up);
      Assert.True(route.Interfaces.ElementAt(5).Up);
    }
  }
}