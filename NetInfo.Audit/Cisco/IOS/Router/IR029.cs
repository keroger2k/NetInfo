using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure these commands exist: "service tcp-keepalives-in"  & "service tcp-keepalives-out"
  /// </summary>
  public class IR029 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR029(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIIOSDevice)Device);
      return device.ServiceSettings.TcpKeepalivesIn && device.ServiceSettings.TcpKeepalivesOut;
    }
  }
}