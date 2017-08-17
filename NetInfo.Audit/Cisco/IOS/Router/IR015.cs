using System;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Infrastructure.ExtensionMethods;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure all interfaces participating in EIGRP are configured with MD5 keys when OSPF is configured
  ///
  /// </summary>
  public class IR015 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR015(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var eigrpInterface = device.GetCoveredInterfaces().Where(c => !c.Shutdown);
      return eigrpInterface.All(c => c.IP.EIGRP.Mode.Equals("md5", StringComparison.OrdinalIgnoreCase));
    }
  }
}