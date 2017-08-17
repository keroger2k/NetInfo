using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure there are no SNMP community strings configured
  /// </summary>
  public class BS032 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS032(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIBOSDevice)Device);
      return !device.SNMP.CommunityStrings.Any();
    }
  }
}