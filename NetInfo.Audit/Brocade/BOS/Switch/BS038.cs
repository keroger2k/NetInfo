using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure at least 2 AAA authentication RADIUS servers are configured
  /// </summary>
  public class BS038 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS038(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIBOSDevice)Device).RadiusServers.Count() > 1;
    }
  }
}