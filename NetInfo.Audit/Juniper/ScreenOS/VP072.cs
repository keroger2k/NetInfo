using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set clock timezone 0 (ScreenOS 5.x only)
  /// </summary>
  public class VP072 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP072(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var line = ((INMCIScreenOSDevice)Device).GetClockTimezone();
      if (string.IsNullOrEmpty(line)) { return false; }
      return line.Trim().Equals("set clock timezone 0", System.StringComparison.InvariantCultureIgnoreCase);
    }
  }
}