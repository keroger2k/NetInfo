using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Validate the latest major version of the ODMN_ONLY_TRAFFIC ACL is applied to Vlans 23 (uTB), 51 (uB2), 56 (uB1) and 124 (RAS)
  /// </summary>
  public class IR108 : ISTIGItem {

    public IDevice Device { get; private set; }

    private string ODMN_ACL_NAME {
      get {
        return string.Format("ODMN_ONLY_TRAFFIC_ACL_V");
      }
    }

    private string ODMN_ACL {
      get {
        return string.Format("{0}{1}", this.ODMN_ACL_NAME, this._odmnAclMajorVersion);
      }
    }

    private int _odmnAclMajorVersion;
    private string[] _approvedVlans = new string[] { "Vlan23", "Vlan51", "Vlan56", "Vlan124" };

    public IR108(INMCIIOSDevice device, int odmnAclMajorVersion) {
      this.Device = device;
      this._odmnAclMajorVersion = odmnAclMajorVersion;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;

      var interfacesWithOdmnAcl = device.Interfaces
        .Where(c => c.AccessGroups.Any(d => d.Name.Contains(ODMN_ACL_NAME) && d.Direction.Equals("in")))
        .Where(c => c.AccessGroups.Any(d => d.Name.Contains(ODMN_ACL_NAME) && d.Direction.Equals("out")));

      return interfacesWithOdmnAcl.Count() == 1 &&
              interfacesWithOdmnAcl.All(c => c.AccessGroups.All(d => d.Name.Contains(ODMN_ACL))) &&
              (interfacesWithOdmnAcl.All(c => c.Physical || _approvedVlans.Contains(c.ShortName)));
    }
  }
}