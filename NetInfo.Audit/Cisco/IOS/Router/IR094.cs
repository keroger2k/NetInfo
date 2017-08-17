using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure CDP is disabled globally or disabled on each active external interface
  /// </summary>
  public class IR094 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR094(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var cdpEnabled = device.ShowCdpInterface.CDPEnabled;
      return !cdpEnabled || device.ShowCdpInterface.Interfaces.Count() == 0;
    }
  }
}