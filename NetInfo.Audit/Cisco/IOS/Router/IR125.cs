using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Validate all test ports are shutdown
  /// </summary>
  public class IR125 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR125(INMCIIOSDevice device) {
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