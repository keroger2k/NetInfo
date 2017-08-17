using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Validate all NTP servers are using version "3"
  /// </summary>
  public class RB043 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB043(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIRIOSDevice)Device).NTP.Servers.All(server => server.Version == 3);
    }
  }
}