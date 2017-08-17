using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure 802.1x the reauthentication timeout is set to the default 3600 seconds or shorter.
  /// </summary>
  public class BS040 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS040(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      return device.ShowDot1xResults.Dot1x.ReAuthPeriod <= 3600;
    }
  }
}