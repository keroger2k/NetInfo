using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure ACL98 is properly applied to all virtual terminal (VTY) lines per the hardening script
  /// </summary>
  public class IR194 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR194(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var vtyLines = device.Lines.Where(c => c.Type == LineType.VTY);
      var vty0_4 = vtyLines.SingleOrDefault(c => c.Name.Equals("line vty 0 4"));
      var vty5_15 = vtyLines.SingleOrDefault(c => c.Name.Equals("line vty 5 15"));
      return vty0_4.Commands.Any(c => c.Equals(" access-class 98 in")) && ((vty5_15 == null) ? true : vty5_15.Commands.All(c => !c.Equals(" access-class 98 in")));
    }
  }
}