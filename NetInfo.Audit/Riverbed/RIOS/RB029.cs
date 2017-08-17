using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Remove non-NMCI NTP servers
  /// </summary>
  public class RB029 : ISTIGItem {

    public IDevice Device { get; private set; }

    private Regex rgxNtpServers = new Regex(@"(^172\.1[16,18,19]\.\d+\.\d+$|^10\.\d+\.\d+\.\d+$|^138\.156\.\d+\.\d+$|^205\.110\.\d+\.\d+$)", RegexOptions.IgnoreCase);

    public RB029(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      return device.NTP.Servers.All(c => rgxNtpServers.Match(c.Address.ToString()).Success);
    }
  }
}