using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure 802.1x reauthentication is enabled.
  /// </summary>
  public class BS022 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS022(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIBOSDevice)Device);
      return device.GlobalDot1xSettings != null && device.GlobalDot1xSettings.ReAuthentication;
    }
  }
}