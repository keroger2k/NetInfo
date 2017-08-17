using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Ensure SNMP read and read/write communities are set per the NMS script
  ///
  /// Only the read community is set.  There is no read/write commnunity set.
  /// </summary>
  public class RB009 : ISTIGItem {

    public IDevice Device { get; private set; }

    private string _approvedCommunity;

    public RB009(INMCIRIOSDevice device, string approvedCommunity) {
      this.Device = device;
      this._approvedCommunity = approvedCommunity;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      return device.SNMP.Community.Equals(_approvedCommunity, System.StringComparison.OrdinalIgnoreCase);
    }
  }
}