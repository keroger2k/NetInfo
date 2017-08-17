using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure the following command exists: dot1x-enable.
  /// </summary>
  public class BS020 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS020(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIBOSDevice)Device);
      return device.GlobalDot1xSettings != null && device.GlobalDot1xSettings.Enabled;
    }
  }
}