using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Validate the hostname follows the naming standards
  /// </summary>
  public class IS101 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS101(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.Hostname != null;
    }
  }
}