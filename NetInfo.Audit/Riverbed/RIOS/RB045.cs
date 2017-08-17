using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Validate the hostname follows the naming standards
  /// </summary>
  public class RB045 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB045(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      return device.Hostname != null;
    }
  }
}