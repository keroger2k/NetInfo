using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure TACACS is configured for console access authentication (see comment)
  ///
  /// require commands:
  ///   aaa authentication login default group tacacs+ enable
  ///   aaa authentication enable default group tacacs+ enable
  /// </summary>
  public class IS021 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS021(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.AAA.Authentication.EnableGroupTacacsEnable && device.AAA.Authentication.LoginGroupTacacsEnable;
    }
  }
}