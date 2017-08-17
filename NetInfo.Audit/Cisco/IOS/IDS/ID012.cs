using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS.IDS;

namespace NetInfo.Audit.Cisco.IOS.IDS {

  /// <summary>
  /// Validate this command exists: "virtual-sensor vs0"
  /// </summary>
  public class ID012 : ISTIGItem {

    public IDevice Device { get; private set; }

    public ID012(INMCIIDSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIDSDevice)Device;
      return device.VirtualSensorVS0;
    }
  }
}