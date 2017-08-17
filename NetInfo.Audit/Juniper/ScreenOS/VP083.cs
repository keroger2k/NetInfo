using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate the "set ntp server" command is set to the gateway address of the vlan that the VPN is connected to (HSRP address if applicable)
  /// </summary>
  public class VP083 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP083(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.GetRoute.Trusted.Select(c => c.Gateway).Contains(device.NTP.Server);
    }
  }
}