using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS.IDS;

namespace NetInfo.Audit.Cisco.IOS.IDS {

  /// <summary>
  /// Validate the system name contains the correct function identifier (SN20-24)
  /// </summary>
  public class ID001 : ISTIGItem {

    public IDevice Device { get; private set; }

    public ID001(INMCIIDSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIDSDevice)Device;
      return new Regex(@"[a-zA-Z]+sn(20|21|22|23|24)", RegexOptions.IgnoreCase).Match(device.Hostname).Success;
    }
  }
}