using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Juniper.ScreenOS;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate only the permitted admin manager-ip's are included per the appropriate hardening script
  /// </summary>
  public class VP027 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<AdminSettings.ManagerAddress> _addresses;

    public VP027(INMCIScreenOSDevice device, IEnumerable<AdminSettings.ManagerAddress> addresses) {
      this.Device = device;
      _addresses = addresses;
    }

    public bool Compliant() {
      var device = ((INMCIScreenOSDevice)Device);
      foreach (var item in device.AdminSettings.ManagerAddresses) {
        if (!_addresses.Contains(item)) {
          return false;
        }
      }
      return device.AdminSettings.ManagerAddresses.Count() == _addresses.Count();
    }
  }
}