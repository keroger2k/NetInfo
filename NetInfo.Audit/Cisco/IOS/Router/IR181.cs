using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure the uB1 WAN Outbound ACL exists, is correctly configured per the template and is properly applied.
  ///
  /// The STIG stipulates that the outbound ACL be applied to the internal-facing interfaces
  /// in an inbound direction, but the low side template only specifies that is be
  /// applied to the external-facing interface in an outbound direction.
  /// </summary>
  public class IR181 : ISTIGItem {

    public IDevice Device { get; private set; }

    public static Regex uTB_ACL_NAME_REGEX = new Regex(@"b1acl-out-.*|initial.*", RegexOptions.IgnoreCase);

    public IR181(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var b1Acl = device.Interfaces.Any(c =>
        c.AccessGroups.Any(d => d.Direction.Equals("out") && IR181.uTB_ACL_NAME_REGEX.Match(d.Name).Success));
      return b1Acl;
    }
  }
}