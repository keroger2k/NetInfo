using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure the following command exists: "ip ssh version 2"
  /// </summary>
  public class IS125 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS125(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.IPSettings.SSH.Version == 2;
    }
  }
}