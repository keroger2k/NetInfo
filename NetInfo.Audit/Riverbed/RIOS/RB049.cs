using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Validate the timezone is UTC
  /// </summary>
  public class RB049 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB049(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var line = ((INMCIRIOSDevice)Device).GetClock();
      if (string.IsNullOrEmpty(line)) return false;
      return line.Trim().Equals("clock timezone UTC", System.StringComparison.InvariantCultureIgnoreCase);
    }
  }
}