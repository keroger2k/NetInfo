using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command does NOT exist: "service pad"
  /// </summary>
  public class IR028 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR028(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return !((INMCIIOSDevice)Device).ServiceSettings.Pad;
    }
  }
}