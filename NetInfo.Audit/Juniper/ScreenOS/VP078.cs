using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate unset interface vlan1 bypass-others-ipsec
  /// </summary>
  public class VP078 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP078(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return !device.InterfaceSettings.IsVlan1BypassOthersIpsec;
    }
  }
}