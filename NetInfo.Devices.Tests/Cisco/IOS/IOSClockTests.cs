using NetInfo.Devices.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class IOSClockTests {

    [Test]
    public void creating_a_new_ios_clock_with_default_constructor_should_have_default_values() {
      var clock = new IOSClock();

      Assert.AreEqual(string.Empty, clock.Timezone);
      Assert.AreEqual(-1, clock.HourOffset);
      Assert.AreEqual(-1, clock.MinuteOffset);
    }

    [Test]
    public void create_a_new_clock_with_timezone_and_hour_offset_should_assign_values_and_have_default_value_for_minute_offset() {
      var clock = new IOSClock("utc", 0);

      Assert.AreEqual("utc", clock.Timezone);
      Assert.AreEqual(0, clock.HourOffset);
      Assert.AreEqual(0, clock.MinuteOffset);
    }

    [Test]
    public void create_a_new_clock_with_timezone_and_hour_and_minute_offset_should_assign_values() {
      var clock = new IOSClock("utc", 0, 0);

      Assert.AreEqual("utc", clock.Timezone);
      Assert.AreEqual(0, clock.HourOffset);
      Assert.AreEqual(0, clock.MinuteOffset);
    }
  }
}