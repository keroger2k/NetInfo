using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure TACACS is configured for SSH access authentication
  ///
  /// Required Commands:
  ///   aaa authentication login default tacacs+ enable
  ///   aaa authentication enable default tacacs+ enable
  /// </summary>
  public class BS029 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS029(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIBOSDevice)Device);
      return device.AAA != null &&
        device.AAA.Authentication.LoginGroupTacacsEnable &&
        device.AAA.Authentication.EnableGroupTacacsEnable;
    }
  }
}