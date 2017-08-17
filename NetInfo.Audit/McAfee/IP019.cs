using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate "Audit Logging" is Enabled
  /// </summary>
  public class IP019 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IP019(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIMcAfeeDevice)Device;
      return device.AuditLoggingEnabled;
    }
  }
}