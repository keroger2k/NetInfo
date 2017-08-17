using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure this command exists:  "ip ssh timeout 60"
  /// </summary>
  public class BS012 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS012(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIBOSDevice)Device).SSH.Timeout == 60;
    }
  }
}