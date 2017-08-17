using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate the Peer ISM Config "Alert TCP Port" setting is "8502"
  /// </summary>
  public class IP013 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IP013(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIMcAfeeDevice)Device).ManagerConfig.AlertTcpPort == 8502;
    }
  }
}