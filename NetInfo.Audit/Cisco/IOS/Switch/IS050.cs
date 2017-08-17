using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure a remote logging (syslog) server is configured
  /// </summary>
  public class IS050 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS050(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIIOSDevice)Device).SyslogSettings.Servers != null;
    }
  }
}