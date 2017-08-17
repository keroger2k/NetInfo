using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure the following command exists: "no ip domain-lookup"
  ///
  /// The STIG stipulates that "ip domain-lookup" can be enabled if, and only if an "ip name-server" has been defined.
  /// </summary>
  public class IR179 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR179(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return !device.IPSettings.DomainLookup;
    }
  }
}