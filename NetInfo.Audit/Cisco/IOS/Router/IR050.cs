using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure a remote logging (syslog) server is configured
  /// </summary>
  public class IR050 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR050(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.SyslogSettings.Servers.Any();
    }
  }
}