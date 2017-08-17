using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure these commands do not exist: "ip rcmd rcp-enable" and "ip rcmd rsh-enable"
  /// </summary>
  public class IS006 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS006(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.IPSettings != null && !device.IPSettings.RCMD.IsRCPEnabled && !device.IPSettings.RCMD.IsRSHEnabled;
    }
  }
}