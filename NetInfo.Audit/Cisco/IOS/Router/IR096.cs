using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Validate CDP is enabled globally and on each interface connecting to a Cisco device
  ///
  /// This check is for all inner devices and managed outer devices
  /// </summary>
  public class IR096 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR096(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var trunkingInterfaces = device.Interfaces.Where(c => !c.Shutdown && c.SwitchPort.Type == IOSInterface.SwitchPortSettings.PortType.Trunk).ToList();
      return trunkingInterfaces.All(c => c.IsCDPEnabled);
    }
  }
}