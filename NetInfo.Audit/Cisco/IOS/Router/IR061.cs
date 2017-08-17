using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command exists:  ip ssh authentication-retries 2
  /// </summary>
  public class IR061 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR061(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.IPSettings.SSH.AuthenticationRetries == 2;
    }
  }
}