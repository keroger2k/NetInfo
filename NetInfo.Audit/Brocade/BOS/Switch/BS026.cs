using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure the "snmp-server group" command includes "access 69"
  /// </summary>
  public class BS026 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS026(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIBOSDevice)Device);
      return device.SNMP != null && device.SNMP.Groups.Any() && device.SNMP.Groups.All(c => c.AccessGroup == 69);
    }
  }
}