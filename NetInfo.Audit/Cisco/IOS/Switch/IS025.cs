using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure this command exists: "service password-encryption"
  /// </summary>
  public class IS025 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS025(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIIOSDevice)Device).ServiceSettings.PasswordEncryption;
    }
  }
}