using NetInfo.Devices.Riverbed.RIOS.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.McAfee.Tests {

  [TestFixture]
  public class ShowVersionTests {

    [Test]
    public void correctly_returns_the_model_and_os_release_version() {
      var route = new ShowVersion(
        @"Product name:      rbt_cmc
Product release:   5.0.4a
Build ID:          #57_4
Build date:        2009-10-08 18:58:52
Build arch:        i386
Built by:          root@mostar

Uptime:            253d 12h 10m 54s

Product model:     8000
System memory:     2021 MB used / 4 MB free / 2026 MB total
Number of CPUs:    1
CPU load averages: 0.03 / 0.03 / 0.00".ToConfig()
      );

      Assert.AreEqual("5.0.4a", route.OSRelease);
      Assert.AreEqual("8000", route.Model);
    }
  }
}