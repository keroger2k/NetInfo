using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure all trunk native VLANs are set to Vlan1 unless specifically called out otherwise in the solution
  ///
  /// Per Ticket: 15473; allow everything to pass.
  /// </summary>
  public class IS014 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS014(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return true;
    }
  }
}