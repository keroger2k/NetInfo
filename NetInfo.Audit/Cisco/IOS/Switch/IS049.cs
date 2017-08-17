using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure this command exists: "logging trap informational"
  ///
  /// The current hardening script has these settings:
  ///   Inner Devices: logging trap notification,
  ///   Outer Devices: logging trap informational.
  /// Until the hardening script is updated, logging trap notification is acceptable.
  /// </summary>
  public class IS049 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS049(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      if (!device.SyslogSettings.isLoggingTrapEnabled) return false;
      if (device.IsManaged && !device.IsODMNEnabled) return new string[] { "notifications" }.Contains(((INMCIIOSDevice)Device).SyslogSettings.TrapLevel);
      return new string[] { "informational" }.Contains(((INMCIIOSDevice)Device).SyslogSettings.TrapLevel);
    }
  }
}