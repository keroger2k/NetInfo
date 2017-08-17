using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure these commands do not exist: "ip rcmd rcp-enable" and "ip rcmd rsh-enable"
  /// </summary>
  public class IR006 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR006(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return !((INMCIIOSDevice)Device).IPSettings.RCMD.IsRCPEnabled && !((INMCIIOSDevice)Device).IPSettings.RCMD.IsRSHEnabled;
    }

    public override string ToString() {
      string message = string.Empty;
      if (this.Compliant()) {
        message = "Passing";
      } else {
        message = string.Join(", ", ((INMCIIOSDevice)Device).IPSettings.RCMD.Settings);
      }
      return message;
    }
  }
}