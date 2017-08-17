using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure this command exists:  ip ssh authentication-retries 3
  /// </summary>
  public class BS013 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS013(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIBOSDevice)Device).SSH.AuthenticationRetries == 3;
    }
  }
}