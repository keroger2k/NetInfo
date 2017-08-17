using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Validate the connected server is clearly identified by the port name or description
  ///
  /// The port name or description of all connected ports must clearly identify the connected server.
  /// This should be the server name and, if necessary, the port designation.
  /// </summary>
  public class IS075 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS075(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var connectedPorts = device.Interfaces
        .Where(c => c.Physical &&
              !c.Shutdown &&
              !new[] { 1, 2 }.Contains(c.Vlan) &&
              c.SwitchPort.Type == IOSInterface.SwitchPortSettings.PortType.Access);
      return connectedPorts.All(c =>
        !string.IsNullOrEmpty(c.Description) &&
        !(c.Description.Contains("RESERVED") || c.Description.Contains("DISABLED")));
    }
  }
}