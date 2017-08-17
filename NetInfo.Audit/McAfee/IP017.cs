using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate "Console timeout" is set to 10 minutes
  /// </summary>
  public class IP017 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IP017(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIMcAfeeDevice)Device;
      return device.ConsoleTimeout == 10;
    }
  }
}