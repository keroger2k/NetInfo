using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate the ISM Config "Install TCP Port" parameter is "8501"
  /// </summary>
  public class IP008 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IP008(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIMcAfeeDevice)Device).ManagerConfig.InstallTcpPort == 8501;
    }
  }
}