using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Ensure this command exists: ssh server v2-only enable
  /// </summary>
  public class RB054 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB054(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIRIOSDevice)Device).SSH.V2OnlyEnable;
    }
  }
}