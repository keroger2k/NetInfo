using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Cisco.IDS.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IDS {

  [TestFixture]
  public class ShowUsersAllTests {

    private IEnumerable<string> config = @"* 27412 cids_admin administrator
arccids viewer
(cisco) administrator
quan_admin administrator".ToConfig();

    [Test]
    public void show_version_correctly_identifies_the_number_of_units() {
      var su = new ShowUsersAll(config);

      Assert.AreEqual(4, su.Users.Count());
    }
  }
}