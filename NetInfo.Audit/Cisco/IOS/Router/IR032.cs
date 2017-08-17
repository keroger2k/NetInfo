using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command does NOT exist: "ip finger"
  /// </summary>
  public class IR032 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR032(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return !device.IPSettings.Finger;
    }
  }
}