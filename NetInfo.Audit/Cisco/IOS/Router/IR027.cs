using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure these commands do NOT exist:  "service udp-small-servers" & "service tcp-small-servers"
  /// </summary>
  public class IR027 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR027(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return !(device.ServiceSettings.TcpSmallServers || device.ServiceSettings.UdpSmallServers);
    }
  }
}