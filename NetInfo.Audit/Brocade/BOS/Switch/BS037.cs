using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure this command exists:  aaa authentication dot1x default radius.
  /// </summary>
  public class BS037 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS037(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      return device.AAA.Authentication.Dot1xDefaultRadisuEnable;
    }
  }
}