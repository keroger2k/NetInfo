using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure at least 2 AAA authentication TACACS servers are configured
  /// </summary>
  public class BS023 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS023(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIBOSDevice)Device);
      return device.TacacsServer.Hosts != null && device.TacacsServer.Hosts.Count() >= 2;
    }
  }
}