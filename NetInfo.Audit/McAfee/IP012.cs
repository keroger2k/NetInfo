using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate the Peer ISM Config "Install TCP Port" setting is "8501"
  /// </summary>
  public class IP012 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IP012(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIMcAfeeDevice)Device).ManagerConfig.InstallTcpPort == 8501;
    }
  }
}