using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure the AUX line has these commands: "no exec" & "transport output none"
  /// Also ensure there is no "transport input xxxx" command.
  /// </summary>
  public class IR054 : ISTIGItem {

    public IDevice Device { get; private set; }

    private string[] requiredCommands = new string[] {
      " no exec",
      " transport output none",
    };

    public IR054(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIIOSDevice)Device);
      var auxLines = device.Lines.SingleOrDefault(c => c.Type == LineType.AUX);
      return (auxLines != null) ?
        requiredCommands.Union(auxLines.Commands).Count() == auxLines.Commands.Count() &&
        !auxLines.Commands.Any(c => new Regex(@"transport input", RegexOptions.IgnoreCase).Match(c).Success) : true;
    }
  }
}