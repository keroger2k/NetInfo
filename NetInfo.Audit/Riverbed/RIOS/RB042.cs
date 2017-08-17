using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Validate device does not need services restarted
  /// </summary>
  public class RB042 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB042(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIRIOSDevice)Device).OptimizationServiceEnabled;
    }
  }
}