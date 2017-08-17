using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate the system name follows the naming standards
  /// </summary>
  public class IP001 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IP001(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIMcAfeeDevice)Device;
      return device.Hostname != null;
    }
  }
}