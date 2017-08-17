using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure TACACS is configured for console access authentication
  ///
  /// Required Commands:
  ///   aaa authentication login default tacacs+ enable
  ///   aaa authentication enable default tacacs+ enable
  ///   enable aaa console
  /// </summary>
  public class BS028 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS028(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIBOSDevice)Device);
      return device.AAA != null &&
        device.AAA.Authentication.LoginGroupTacacsEnable &&
        device.AAA.Authentication.EnableGroupTacacsEnable &&
        device.AAA.ConsoleEnabled;
    }
  }
}