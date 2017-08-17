using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure the NTP authentication is enabled with the following command:  "sntp server … authentication-key ..."
  /// </summary>
  public class BS036 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS036(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      return device.SNTP.Servers.Any() && device.SNTP.Servers.All(c => !string.IsNullOrEmpty(c.Key));
    }
  }
}