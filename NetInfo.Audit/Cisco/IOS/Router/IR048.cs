using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command exists: "ip tcp synwait-time 10"
  /// </summary>
  public class IR048 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR048(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.IPSettings.TCP.SynWaitTime == 10;
    }
  }
}