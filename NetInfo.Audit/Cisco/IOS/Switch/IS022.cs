using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure at least 2 AAA authentication TACACS servers are configured
  /// </summary>
  public class IS022 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS022(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIIOSDevice)Device).TacacsServer.Hosts.Count() >= 2;
    }
  }
}