using System.Linq;
using NetInfo.Devices.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Brocade.BOS.Classes {

  [TestFixture]
  public class SNTPSettingsTests {

    [Test]
    public void sntp_object_with_no_settings_should_return_zero_servers_and_zero_poll_interval() {
      var settings = new SNTPSettings();
      settings.Settings = new string[] { };

      Assert.AreEqual(0, settings.PollInterval);
      Assert.IsEmpty(settings.Servers);
    }

    [Test]
    public void sntp_object_should_correct_parse_server_addresses() {
      var setttings = new SNTPSettings();
      setttings.Settings = new string[] {
        "sntp server 1.1.1.1 4",
        "sntp server 2.2.2.2 4",
        "sntp poll-interval 3600"
      };

      Assert.AreEqual(2, setttings.Servers.Count());
      Assert.AreEqual("1.1.1.1", setttings.Servers.ElementAt(0).Address.ToString());
      Assert.AreEqual("2.2.2.2", setttings.Servers.ElementAt(1).Address.ToString());
    }

    [Test]
    public void sntp_object_should_correct_parse_poll_interval() {
      var setttings = new SNTPSettings();
      setttings.Settings = new string[] {
        "sntp server 1.1.1.1 4",
        "sntp server 2.2.2.2 4",
        "sntp poll-interval 3600"
      };

      Assert.AreEqual(3600, setttings.PollInterval);
    }
  }
}