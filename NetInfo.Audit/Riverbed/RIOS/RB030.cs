using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Shutdown all unused Interfaces and those with Link=No (LAN0_1, WAN0_1, IN-PATH0_1, AUX)
  /// </summary>
  public class RB030 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB030(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      var r = device.ShowInterfaceBrief.Interfaces
        .Where(c => !c.Link);
      return r.All(c => !c.Up);
    }
  }
}