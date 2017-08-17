using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Brocade.BOS.Classes.Commands;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure all host-facing access ports include this command: dot1x port-control auto
  /// </summary>
  public class BS039 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS039(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;

      var accessPorts = device.ShowInterface.Interfaces
        .Where(c => c.Enabled)
        .Where(c => c.Protocol == ShowInterface.Interface.ProtocolStatus.up || c.Protocol == ShowInterface.Interface.ProtocolStatus.down)
        .Where(c => c.PortInformation.PortType == ShowInterface.Interface.PType.Access)
        .Select(c => c.Number);

      var accessPortInterfaceList = device.Interfaces.Where(c => accessPorts.Contains(c.Number));
      return accessPortInterfaceList.All(c => c.Dot1x.PortControlAuto || c.Dot1x.MACAuthentication);
    }
  }
}