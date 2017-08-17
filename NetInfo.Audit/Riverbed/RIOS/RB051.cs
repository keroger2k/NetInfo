using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// DEPRECATED!
  /// </summary>
  public class RB051 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB051(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      return device.SNMP.Location != null;
    }
  }
}