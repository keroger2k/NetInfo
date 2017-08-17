using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure ssh idle timeout value is set: "ip ssh  idle-time 3"
  /// </summary>
  public class BS011 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS011(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIBOSDevice)Device).SSH.IdleTime == 3;
    }
  }
}