using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command exists on console port: "exec-timeout 3 0"
  ///
  /// The STIG stipulates the exec-timeout be set to a value of 10 minutes or less.  The default is 10 minutes.
  /// </summary>
  public class IR071 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR071(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var lines = ((INMCIIOSDevice)Device).Lines;
      var consoles = lines.Where(c => c.Type == LineType.CONSOLE);

      foreach (var line in consoles) {
        if (!line.Commands.Any(c => new Regex(@"^ exec-timeout 3 0$", RegexOptions.IgnoreCase).Match(c).Success)) {
          return false;
        }
      }

      return true;
    }
  }
}