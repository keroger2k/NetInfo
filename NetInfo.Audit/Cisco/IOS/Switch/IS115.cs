using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure these commands do NOT exist: "boot network"
  /// </summary>
  public class IS115 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS115(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return !device.BootNetwork;
    }
  }
}