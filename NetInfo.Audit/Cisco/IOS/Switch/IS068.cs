using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure this command exists:  "ip ssh time-out 30"
  /// </summary>
  public class IS068 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS068(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.IPSettings.SSH.Timeout == 30;
    }
  }
}