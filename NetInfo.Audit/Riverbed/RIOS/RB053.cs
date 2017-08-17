using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Ensure TACACS is configured for VTY access authentication
  ///
  /// Required Command:
  ///   aaa authentication login default tacacs+ local
  /// </summary>
  public class RB053 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB053(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      return device.AAA.Authentication.LoginDefaultTacacsLocal;
    }
  }
}