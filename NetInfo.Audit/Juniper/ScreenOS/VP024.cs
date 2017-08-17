using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate unset interface vlan1 ip
  /// </summary>
  public class VP024 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP024(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return !device.InterfaceSettings.IsVlan1IpSet;
    }
  }
}