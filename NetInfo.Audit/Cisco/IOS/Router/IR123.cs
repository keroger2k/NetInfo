using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Shutdown all "notconnected" ports
  ///
  /// Validate with the LSE that the port can be shutdown.  This check is for all outer devices.
  /// </summary>
  public class IR123 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR123(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var result = device.ShowInterfaceStatus.Interfaces.Where(c => c.Status == ShowInterfaceStatus.InterfaceStatus.notconnect ||
        c.Status == ShowInterfaceStatus.InterfaceStatus.notconnected);
      return (result == null) ? true : !result.Any();
    }
  }
}