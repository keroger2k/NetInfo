using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure this command exists on console port: "exec-timeout 3 0"
  /// </summary>
  public class IS071 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS071(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var lines = ((INMCIIOSDevice)Device).Lines;
      var consoles = lines.Where(c => c.Type == LineType.CONSOLE);
      var execRegex = new Regex(@"^ exec-timeout 3 0$", RegexOptions.IgnoreCase);

      foreach (var line in consoles) {
        if (!line.Commands.Any(c => execRegex.Match(c).Success)) {
          return false;
        }
      }

      return true;
    }
  }
}