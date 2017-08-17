using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Validate "line aux 0" does not include the following command:
  /// "access-class XX in",
  /// where XX is 97, 98 or 99.
  /// </summary>
  public class IR133 : ISTIGItem {

    public IDevice Device { get; private set; }

    private string[] flaggedCommands = new string[] {
      " access-class 97 in",
      " access-class 98 in",
      " access-class 99 in",
    };

    public IR133(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIIOSDevice)Device);
      var auxLines = device.Lines.SingleOrDefault(c => c.Type == LineType.AUX);
      return (auxLines != null) ? auxLines.Commands.Intersect(flaggedCommands).Count() == 0
        : true;
    }
  }
}