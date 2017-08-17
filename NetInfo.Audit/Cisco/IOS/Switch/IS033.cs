using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure this command does NOT exist: "ip http server"
  /// </summary>
  public class IS033 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS033(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return !device.IPSettings.HttpServer;
    }
  }
}