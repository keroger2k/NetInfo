using NetInfo.Devices.NMCI.Infrastructure.Implementations.Juniper.ScreenOS;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Devices.NMCI.Tests.Brocade.BOS {

  [TestFixture]
  public class HostnameTests {

    [Test]
    public void hostname_correctly_parses() {
      var hostname = new Hostname(new ScreenOSHostname("MCUSBANDVP00"));

      Assert.AreEqual("BAND", hostname.Site);
    }
  }
}