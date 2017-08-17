using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate "SSH inactive timeout" is set to 60 seconds
  /// </summary>
  public class IP020 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IP020(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIMcAfeeDevice)Device;
      return device.SSHInactiveTimeout == 60;
    }
  }
}