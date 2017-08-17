using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Ensure TACACS is configured for console access authentication
  ///
  /// Required Command:
  ///   aaa authentication login default tacacs+ local
  /// </summary>
  public class RB052 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB052(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      return device.AAA.Authentication.LoginDefaultTacacsConsole;
    }
  }
}