using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure this command exists: "ip tcp synwait-time 10"
  /// </summary>
  public class IS048 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS048(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.IPSettings.TCP.SynWaitTime == 10;
    }
  }
}