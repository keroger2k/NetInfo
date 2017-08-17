using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate "SSH Remote Logins" are "enabled"
  /// </summary>
  public class IP006 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IP006(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIMcAfeeDevice)Device;
      return device.SensorNetworkConfig.SSHRemoteLoginsEanbled;
    }
  }
}