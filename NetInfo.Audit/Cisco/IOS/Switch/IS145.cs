using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure the configured NTP servers use a configured authentication key
  /// </summary>
  public class IS145 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS145(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.NetworkTimeProtocol.Servers.All(c => c.Key.HasValue);
    }
  }
}