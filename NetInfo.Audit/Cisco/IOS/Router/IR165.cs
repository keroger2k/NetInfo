using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure these commands do NOT exist: "boot network"
  /// </summary>
  public class IR165 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR165(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return !device.BootNetwork;
    }
  }
}