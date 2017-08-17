using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set ike policy-checking (ScreenOS 4.x only)
  /// </summary>
  public class VP012 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP012(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.IKESettings.PolicyCheckingEnabled;
    }
  }
}