using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command exists on all external interfaces: "no ip unreachables"
  ///
  /// Exceptions:
  ///   1. loopback interfaces excludes this command, except for those loopback interfaces
  ///      used as a tunnel source interface on the uB1 OR, and loopback interfaces on classified routers;
  ///   2. tunnel interfaces on the uB1 OR excludes this command;
  ///   3. the legacy network connection (Vlan98),
  ///      Vlan141 & Vlan170 on the uST outer router exclude "no ip unreachables";
  /// </summary>
  public class IR191 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR191(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var interfaceWithAddresses = device.Interfaces.Where(c => c.Address != null);
      return interfaceWithAddresses.All(c => !c.IP.Unreachables);
    }
  }
}