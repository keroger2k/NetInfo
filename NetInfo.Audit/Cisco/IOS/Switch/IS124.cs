using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure the following command exists: "no ip domain-lookup"
  /// </summary>
  public class IS124 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS124(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return !device.IPSettings.DomainLookup;
    }
  }
}