using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {
  /// <summary>
  /// Remove any VLAN not being used
  ///
  /// -Remove any VLAN that is not being used except for Vlans 31, 32, 41, 42 or any Vlan that will be used when seats are rolled.
  /// -Remove the configuration for both layers 2 and 3.
  /// -Remove Vlan97 in nonredundant sites-Remove Vlan99 in VSSD sites when there are no downstream devices or a WANX.
  /// -Remove Vlan39 if there is no classified network on the Design Visio.
  /// -Remove the layer 3 interface Vlan2. The exception is when Vlan2 is used for another purpose (USMC only).
  /// -Shutdown Vlan31 & Vlan32 unless the Dual sided VPN exists (rare)
  /// -Shutdown Vlan41 & Vlan42 unless the Dual sided VPN exists (rare)

  /// </summary>
  public class IR124 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR124(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return false;
    }
  }
}