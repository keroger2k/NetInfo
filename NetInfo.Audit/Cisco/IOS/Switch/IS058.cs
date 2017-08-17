using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure VTY lines 0 - 4 are configured with this command: "transport input ssh" (see comment)
  ///
  /// Exceptions:
  ///   Devices not running an IOS k9 image, do not support the SSH protocol and managed devices will be configured with "tranport input telnet";
  ///   if the device is non-managed, "line vty 0 4" should be configured with: "tranport input none".
  /// </summary>
  public class IS058 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS058(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var line = ((INMCIIOSDevice)Device).Lines.SingleOrDefault(c => c.Type == LineType.VTY &&
        c.Name.Equals("line vty 0 4", System.StringComparison.OrdinalIgnoreCase));
      return (line != null) ? line.Commands.Any(c => c.Trim().Equals("transport input ssh")) : true;
    }
  }
}