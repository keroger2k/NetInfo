using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure this command does NOT exist: "service pad"
  /// </summary>
  public class IS028 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS028(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return !((INMCIIOSDevice)Device).ServiceSettings.Pad;
    }
  }
}