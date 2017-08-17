using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate the MGMT port Link Status is "link ok" or "link up"
  /// </summary>
  public class IP016 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IP016(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIMcAfeeDevice)Device;
      return device.MgmtLinkStatus.Equals("link ok") || device.MgmtLinkStatus.Equals("link up");
    }
  }
}