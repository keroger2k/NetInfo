using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure the uTB WAN Outbound ACL is applied to the internal interfaces in an inbound direction
  /// The STIG stipulates that the outbound ACL must applied to the internal-facing interfaces in an inbound direction.
  /// </summary>
  public class IR163 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly int[] internalVirutalInterfaceNumbers = new int[] { 40, 95, 113 };
    private readonly Regex accessListRegex = new Regex(@"OR0[12]_NMCI_UTB_OUT", RegexOptions.IgnoreCase);

    public IR163(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var internalVirtualInterfaces = device.Interfaces
        .Where(c => !c.Physical &&
          !c.Shutdown &&
          internalVirutalInterfaceNumbers.Contains(c.Vlan));

      return internalVirtualInterfaces
        .All(c => c.AccessGroups.Any() &&
          c.AccessGroups
          .All(d => accessListRegex.Match(d.Name).Success &&
            d.Direction.Equals("in")));
    }
  }
}