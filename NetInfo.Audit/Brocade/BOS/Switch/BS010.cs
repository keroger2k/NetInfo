using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure only SSH is permitted and that telnet is disabled with this command: "no telnet server"
  /// </summary>
  public class BS010 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS010(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return !((INMCIBOSDevice)Device).TelnetServer;
    }
  }
}