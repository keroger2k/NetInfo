using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  ///
  /// DEPRECATED
  ///
  /// </summary>
  public class IS109 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS109(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIIOSDevice)Device).SNMPSettings.Location != null;
    }
  }
}