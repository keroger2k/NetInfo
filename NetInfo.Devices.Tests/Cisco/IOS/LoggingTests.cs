using System.Linq;
using NetInfo.Devices.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS.Classes {

  [TestFixture]
  public class LoggingTests {

    [Test]
    public void logging_object_with_no_settings_should_default_results() {
      var logging = new LoggingSettings();

      Assert.AreEqual(@"informational", logging.TrapLevel);
      Assert.IsNull(logging.SourceInterface);
      Assert.IsEmpty(logging.Servers);
    }

    [Test]
    public void logging_object_with_trap_notifications_found_should_return_true() {
      var logging = new LoggingSettings();
      logging.Settings = new string[] {
        @"logging trap notifications"
      };

      Assert.AreEqual("notifications", logging.TrapLevel);
    }

    [Test]
    public void logging_object_should_correctly_parse_source_interface() {
      var logging = new LoggingSettings();
      logging.Settings = new string[] {
        @"logging source-interface Vlan30"
      };

      Assert.AreEqual("Vlan30", logging.SourceInterface);
    }

    [Test]
    public void logging_object_should_correctly_parse_server() {
      var logging = new LoggingSettings();
      logging.Settings = new string[] {
        @"logging 1.1.1.1"
      };

      Assert.AreEqual("1.1.1.1", logging.Servers.ElementAt(0).ToString());
    }
  }
}