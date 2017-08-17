using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure these commands exist: "ip tcp intercept watch-timeout 10" & "ip tcp intercept connection-timeout 60"
  ///
  /// Exceptions:  These commands are not supported on 3550, 3560, and 3750 switches.
  /// The STIG stipulates that the command ""ip tcp intercept list"" command be used, but current architecture does not include this.
  /// </summary>
  public class IR047 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR047(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.IPSettings.TCP.InterceptionConnectionTimeout == 60 && device.IPSettings.TCP.InterceptionWatchTimeout == 10;
    }
  }
}