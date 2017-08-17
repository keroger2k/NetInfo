namespace NetInfo.Devices.Cisco.IOS {

  public class IOSClock {

    public IOSClock() {
      this.Timezone = string.Empty;
      this.HourOffset = -1;
      this.MinuteOffset = -1;
    }

    public IOSClock(string timezone, int hourOffset) {
      this.Timezone = timezone;
      this.HourOffset = hourOffset;
      this.MinuteOffset = 0;
    }

    public IOSClock(string timezone, int hourOffset, int minuteOffset) {
      this.Timezone = timezone;
      this.HourOffset = hourOffset;
      this.MinuteOffset = minuteOffset;
    }

    public string Timezone { get; set; }

    public int HourOffset { get; set; }

    public int MinuteOffset { get; set; }
  }
}