using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure trunking is disabled on all access ports
  /// </summary>
  public class IS013 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly int[] approvedVlans = new int[] { 2, 3 };

    public IS013(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var a = device.Interfaces.Where(c => c.Physical && !c.Shutdown);
      var b = a.Where(c => c.SwitchPort.Type == IOSInterface.SwitchPortSettings.PortType.Access && !approvedVlans.Contains(c.Vlan));
      return b.All(c => !c.SwitchPort.AllowedVlans.Any()) && b.All(c => c.SwitchPort.Encapsulation == null);
    }
  }
}