using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class ClockSettingsTests {
    private ClockSettings clock;

    [SetUp]
    public void Init() {
      clock = new ClockSettings();
      clock.Settings = genericSettings;
    }

    private IEnumerable<string> genericSettings = @"
set clock dst-off
set clock ntp
set clock timezone 0
".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

    [Test]
    public void can_correctly_clock_timezone() {
      Assert.AreEqual(0, clock.Timezone);
    }

    [Test]
    public void can_correctly_parse_clock_ntp() {
      Assert.True(clock.NTPEnabled);
    }

    [Test]
    public void can_correctly_parse_clock_dst() {
      Assert.True(clock.DaylightSavingsTimeOffEnabled);
    }
  }
}