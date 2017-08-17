using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure "ip dhcp" commands do NOT exist and the following command exists: "no service dhcp"
  /// </summary>
  public class IR031 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR031(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return !device.IPSettings.Dhcp && !device.ServiceSettings.Dhcp;
    }
  }
}