using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure all trunk native VLANs are set to Vlan1 unless specifically called out otherwise in the solution
  ///
  /// Exceptions:
  ///   1. NOLA MARFORRES LMR-RNET;
  ///   2. CNIC/SPAWAR Wireless Bridge Extension  uplinks/downlinks  (see Dev Docs D403.38048.01 and D403.31347.05);
  ///   3. Ports connected to legacy VOIP devices at PRLH.
  /// Per Ticket: 15474; allow everything to pass.
  /// </summary>
  public class IR014 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR014(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return true;
    }
  }
}