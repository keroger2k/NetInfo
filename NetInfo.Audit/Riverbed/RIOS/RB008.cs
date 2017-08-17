using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Ensure this command exists: ssh server listen enable
  /// </summary>
  public class RB008 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB008(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      return device.SSH.ServerListenEnable;
    }
  }
}