using System.Linq;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS.Classes {

  [TestFixture]
  public class MonitorSettingsTests {

    [Test]
    public void monitor_object_with_no_settings_should_default_results() {
      var mon = new MonitorSettings();

      Assert.AreEqual(0, mon.Commands.Count());
      Assert.AreEqual(0, mon.Sessions.Count());
    }

    [Test]
    public void monitor_object_should_correctly_parse_all_settings_into_session_settings() {
      var mon = new MonitorSettings();
      mon.Settings = @"monitor session 1 source vlan 30 both
monitor session 1 destination remote vlan 901
".ToConfig();

      Assert.AreEqual(2, mon.Commands.Count());
    }

    [Test]
    public void monitor_object_should_correctly_parse_all_session_commands_into_individual_sessions() {
      var mon = new MonitorSettings();
      mon.Settings = @"monitor session 1 destination interface Gi2/5
monitor session 1 source remote vlan 901
monitor session 2 destination interface Gi2/6
monitor session 2 source remote vlan 901
".ToConfig();

      Assert.AreEqual(2, mon.Sessions.Count());
    }
  }
}