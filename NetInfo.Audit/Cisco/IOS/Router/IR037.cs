using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure these commands exist on all external interfaces: "no ip proxy-arp"
  /// </summary>
  public class IR037 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR037(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var enabledInterfaces = device.Interfaces.Where(c => !c.Shutdown).ToList();
      var externalInterfaces = enabledInterfaces.Where(c => !device.ShowCdpInterface.Interfaces.Select(d => d.Name).Contains(c.ShortName)).ToList();
      return externalInterfaces.All(c => !c.IP.ProxyArp);
    }
  }
}