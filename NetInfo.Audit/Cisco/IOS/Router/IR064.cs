using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure there are no SNMP community strings configured
  ///
  /// Pertains to only devices that are unmanaged or running SNMPv3
  /// </summary>
  public class IR064 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR064(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.SNMPSettings.Communities.Count() == 0;
    }
  }
}