using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command is set on all VTY lines: "exec-timeout 3 0"
  /// </summary>
  public class IR060 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR060(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var lines = ((INMCIIOSDevice)Device).Lines;
      var vtys = lines.Where(c => c.Type == LineType.VTY);

      foreach (var line in vtys) {
        if (line.Commands.Any(c => new Regex(@"^ exec-timeout 3 0$", RegexOptions.IgnoreCase).Match(c).Success)) {
          return true;
        }
      }

      return false;
    }
  }
}