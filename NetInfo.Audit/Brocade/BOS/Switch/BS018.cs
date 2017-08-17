using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Brocade.BOS.Classes.Commands;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure all access ports have no tagged vlans
  /// </summary>
  public class BS018 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS018(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      var accessPorts = device.ShowInterface.Interfaces
        .Where(c => c.PortInformation.PortType == ShowInterface.Interface.PType.Access)
        .All(c => c.PortInformation.PortMode == ShowInterface.Interface.PMode.untagged);
      var dualModePorts = device.ShowInterface.Interfaces
        .Where(c => c.PortInformation.PortMode == ShowInterface.Interface.PMode.dual)
        .Select(c => c.PortInformation)
        .Cast<ShowInterface.Interface.TrunkPort>()
        .All(c => new[] { 1, 3 }.Contains(c.NativeVlan));

      return accessPorts && dualModePorts;
    }
  }
}