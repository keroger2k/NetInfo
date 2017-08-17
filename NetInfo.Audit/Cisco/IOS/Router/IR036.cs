using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command does NOT exist: "ip source-route"
  /// </summary>
  public class IR036 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR036(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return !device.IPSettings.SourceRoute;
    }
  }
}