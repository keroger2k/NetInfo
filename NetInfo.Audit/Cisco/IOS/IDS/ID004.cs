using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS.IDS;

namespace NetInfo.Audit.Cisco.IOS.IDS {

  /// <summary>
  /// Validate this command exists: "telnet-option disabled"
  /// </summary>
  public class ID004 : ISTIGItem {

    public IDevice Device { get; private set; }

    public ID004(INMCIIDSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIDSDevice)Device;
      return device.TelnetOptionDisabled;
    }
  }
}