using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure VTY lines 5 - 15 are configured with this command: "transport input none"
  /// </summary>
  public class IS059 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS059(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var line = ((INMCIIOSDevice)Device).Lines.SingleOrDefault(c => c.Type == LineType.VTY &&
        c.Name.Equals("line vty 5 15", System.StringComparison.OrdinalIgnoreCase));
      return (line != null) ? line.Commands.Any(c => c.Trim().Equals("transport input none")) : true;
    }
  }
}