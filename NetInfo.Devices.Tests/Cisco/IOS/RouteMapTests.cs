using System.Linq;
using NetInfo.Devices.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class RouteMapTests {

    [Test]
    public void route_map_should_be_able_to_give_me_all_unique_standard_access_lists_applied_to_match_statements() {
      var rtm = new RouteMap();

      rtm.Matches = new[] { "match ip address 101" };

      Assert.AreEqual(1, rtm.GetMatchStandardAccessLists().Count());
      Assert.AreEqual(101, rtm.GetMatchStandardAccessLists().ElementAt(0));
    }
  }
}