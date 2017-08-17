using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure two SNTP servers are configured
  /// </summary>
  public class BS025 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS025(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIBOSDevice)Device).SNTP.Servers.Count() >= 2;
    }
  }
}