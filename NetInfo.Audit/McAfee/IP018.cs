using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate Aux Port is Disabled
  /// </summary>
  public class IP018 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IP018(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIMcAfeeDevice)Device;
      return !device.AuxPortEnabled;
    }
  }
}