using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure a layer 3 Vlan1 interface is not configured
  /// </summary>
  public class BS017 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS017(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      return device.Interfaces
        .Where(c => !c.Physical && c.Enabled)
        .Where(c => c.Number.Equals("1"))
        .All(c => c.Address == null);
    }
  }
}