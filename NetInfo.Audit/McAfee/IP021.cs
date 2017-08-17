using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate "SSH AccessControl is Enabled"
  /// </summary>
  public class IP021 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IP021(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIMcAfeeDevice)Device;
      return device.SSHAccessControl;
    }
  }
}