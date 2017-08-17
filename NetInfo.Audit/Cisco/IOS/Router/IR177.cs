using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure the following command does not exist: "service call-home"
  /// </summary>
  public class IR177 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR177(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return !device.ServiceSettings.CallHome;
    }
  }
}