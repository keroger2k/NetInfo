using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Juniper.ScreenOS;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate Unused interfaces have IP set to 0.0.0.0
  /// </summary>
  public class VP001 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP001(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.Interfaces
        .Where(c => c.Status == ScreenOSInterface.Link.down)
        .All(c => c.Address.NetworkAddress.ToString().Equals("0.0.0.0"));
    }
  }
}