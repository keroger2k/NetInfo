using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure the following command does not exist: "no service password-encryption"
  /// </summary>
  public class BS007 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS007(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIBOSDevice)Device).PasswordEncryption;
    }
  }
}