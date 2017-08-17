using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate the ISM Config "Alert TCP Port" parameter is "8502"
  /// </summary>
  public class IP009 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IP009(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIMcAfeeDevice)Device).ManagerConfig.AlertTcpPort == 8502;
    }
  }
}