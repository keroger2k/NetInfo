using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Ensure at least 2 AAA authentication TACACS servers are configured
  /// </summary>
  public class RB005 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB005(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      return device.Tacacs.Hosts.Count() >= 2;
    }
  }
}