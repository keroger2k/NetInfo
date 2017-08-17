using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command does NOT exist:  "ip bootp server"
  /// </summary>
  public class IR034 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR034(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return !device.IPSettings.BootPServer;
    }
  }
}