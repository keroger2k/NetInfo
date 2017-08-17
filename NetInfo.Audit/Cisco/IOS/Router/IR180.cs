using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure the following command exists: "ip ssh version 2"
  /// </summary>
  public class IR180 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR180(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.IPSettings.SSH.Version == 2;
    }
  }
}