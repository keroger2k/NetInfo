using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command does NOT exist on any external Layer 3 IP interface: "ip mask-reply"
  /// </summary>
  public class IR041 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR041(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.Interfaces
        .Where(c => !c.Shutdown && c.Address != null)
        .All(c => !c.IP.MaskReply);
    }
  }
}