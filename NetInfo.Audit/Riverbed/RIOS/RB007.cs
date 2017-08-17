using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Ensure all SNMP community values are set according to the latest NMS script
  ///
  /// Only the read community is set.  There is no read/write commnunity set.
  /// </summary>
  public class RB007 : ISTIGItem {

    public IDevice Device { get; private set; }

    private string _approvedCommunity;

    public RB007(INMCIRIOSDevice device, string approvedCommunity) {
      this.Device = device;
      this._approvedCommunity = approvedCommunity;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      return device.SNMP.Community.Equals(_approvedCommunity, System.StringComparison.OrdinalIgnoreCase);
    }
  }
}