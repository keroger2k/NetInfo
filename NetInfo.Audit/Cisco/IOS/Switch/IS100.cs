using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Validate all test ports are disabled
  /// </summary>
  public class IS100 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS100(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var enabledInterfaces = device.ShowInterface.Interfaces
        .Where(c => c.Enabled && c.Protocol == ShowInterface.Interface.ProtocolStatus.down).ToList();
      return !enabledInterfaces.Any(c => c.Description.ToLower().Contains("test"));
    }
  }
}