using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Validate the hostname follows the naming standards
  /// </summary>
  public class IR127 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR127(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.Hostname != null;
    }
  }
}