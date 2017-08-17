using System.Linq;
using NetInfo.Devices.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Riverbed.RIOS.Classes {

  [TestFixture]
  public class NTPSettingsTests {

    [Test]
    public void ntp_object_with_no_settings_should_return_zero_servers_and_zero_poll_interval() {
      var ntp = new NTPServerSettings();
      ntp.Settings = new string[] { };

      Assert.IsEmpty(ntp.Servers);
    }

    [Test]
    public void ntp_object_should_correct_parse_server_addresses() {
      var ntp = new NTPServerSettings();
      ntp.Settings = new string[] {
        "ntp server 1.1.1.1 version \"4\"",
        "ntp server 2.2.2.2 version \"4\"",
      };

      Assert.AreEqual(2, ntp.Servers.Count());
      Assert.AreEqual("1.1.1.1", ntp.Servers.ElementAt(0).Address.ToString());
      Assert.AreEqual(4, ntp.Servers.ElementAt(0).Version);
      Assert.AreEqual("2.2.2.2", ntp.Servers.ElementAt(1).Address.ToString());
      Assert.AreEqual(4, ntp.Servers.ElementAt(1).Version);
    }
  }
}