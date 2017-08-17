using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate the ISM Config "Logging TCP Port" is "8503"
  /// </summary>
  public class IP010 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IP010(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIMcAfeeDevice)Device).ManagerConfig.LoggingTcpPort == 8503;
    }
  }
}