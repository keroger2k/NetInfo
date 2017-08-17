using System.Linq;
using NetInfo.Devices.Riverbed.RIOS.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.McAfee.Tests {

  [TestFixture]
  public class ShowBootTests {

    [Test]
    public void correctly_returns_the_number_of_paritions_on_device() {
      var route = new ShowBoot(
        @"Installed images:
  Partition 1:
  rbt_sh 4.1.12c-nmci3 #160_8 2012-07-26 17:20:22 i386 root@kaunas:svn://svn/mgmt/tags/tuvalu_160_fix_3_nmci_8

  Partition 2:
  rbt_sh 4.1.12c-nmci3 #160_8 2012-07-26 17:20:22 i386 root@kaunas:svn://svn/mgmt/tags/tuvalu_160_fix_3_nmci_8

Last boot partition: 2
Next boot partition: 2".ToConfig()
      );

      Assert.AreEqual(2, route.Images.Count());
    }
  }
}