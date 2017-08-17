using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure the configured NTP servers use a configured authentication key
  /// </summary>
  public class IR199 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR199(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var ntpServerKeys = device.NetworkTimeProtocol.Servers.Select(c => c.Key).Distinct();
      var ntpConfiguredAuthenticationKeys = device.NetworkTimeProtocol.Keys.Select(c => c.Number).Distinct();

      return device.NetworkTimeProtocol.Servers.Any() &&
        ntpServerKeys.All(c => c.HasValue) &&
        ntpConfiguredAuthenticationKeys.All(c => ntpServerKeys.Contains(c));
    }
  }
}