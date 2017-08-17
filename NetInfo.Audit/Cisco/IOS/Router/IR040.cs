using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command exists on all external interfaces: "no ip redirects"
  ///
  /// Exceptions:
  ///   1. loopback interfaces excludes this command, except for those
  ///      loopback interfaces used as a tunnel source interface on the
  ///      uB1 OR, and loopback interfaces on classified routers;
  ///   2. tunnel interfaces on the uB1 OR excludes this command.
  /// </summary>
  public class IR040 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR040(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var enabledInterfaces = device.Interfaces.Where(c => !c.Shutdown && (c.Type != "Loopback" || c.Type != "Tunnel")).ToList();
      var externalInterfaces = enabledInterfaces.Where(c => !device.ShowCdpInterface.Interfaces.Select(d => d.Name).Contains(c.ShortName)).ToList();
      return externalInterfaces.All(c => !c.IP.Redirects);
    }
  }
}