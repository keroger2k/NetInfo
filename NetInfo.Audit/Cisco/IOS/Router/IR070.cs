using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure VTY lines 0 - 4 are configured with this command: "transport input telnet"
  ///
  /// The STIG stipulates that a FIPS 140-2 validated encryption algorithms or protocols be used for management connections for administrative access.
  /// If the device is using a k9 version of IOS, it must be configured for management access using the SSH protocol.
  /// </summary>
  public class IR070 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR070(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var vtys = device.Lines.Where(c => c.Type == LineType.VTY);
      return vtys.Any() ? vtys.All(c => c.Commands.Any(d => new Regex(@"transport input telnet", RegexOptions.IgnoreCase).Match(d).Success)) : false;
    }
  }
}