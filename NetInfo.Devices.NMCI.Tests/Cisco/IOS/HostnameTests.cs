using NetInfo.Devices.NMCI.Cisco.IOS;
using NetInfo.Devices.NMCI.Infrastructure.Implementations.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Devices.NMCI.Tests.Cisco.IOS {

  [TestFixture]
  public class HostnameTests {

    [Test]
    public void hostname_correctly_parses() {
      var hostname = new Hostname(new IOSHostname("ABCD-U00-IR-01"));

      Assert.AreEqual("ABCD", hostname.Site);
      Assert.AreEqual("00", hostname.Zone);
      Assert.AreEqual("IR", hostname.DeviceType);
    }

    //[Test]
    //public void hostname_correctly_maps_zone_types_for_inner_device() {
    //  var hostname = new Hostname("ABCD-U00-IR-01");

    //  Assert.AreEqual("Inner", hostname.Zone);
    //}

    //[Test]
    //public void hostname_correctly_maps_zone_types_for_outer_device() {
    //  var hostname = new Hostname("ABCD-U00-OR-01");

    //  Assert.AreEqual("Outer", hostname.Zone);
    //}
  }
}