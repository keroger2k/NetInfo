using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS.IDS;

namespace NetInfo.Audit.Cisco.IOS.IDS {

  /// <summary>
  /// Validate the time-zone-settings includes the command: "offset 0"
  /// </summary>
  public class ID007 : ISTIGItem {

    public IDevice Device { get; private set; }

    public ID007(INMCIIDSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIDSDevice)Device;
      return device.TimezoneOffset == 0;
    }
  }
}