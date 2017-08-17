using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate the Peer ISM Config "Logging TCP Port" setting is "8503"
  /// </summary>
  public class IP014 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IP014(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIMcAfeeDevice)Device).ManagerConfig.LoggingTcpPort == 8503;
    }
  }
}