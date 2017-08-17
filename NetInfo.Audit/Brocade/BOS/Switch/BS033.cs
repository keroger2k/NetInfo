using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure running config was saved at the beginning of the test script using the command: "write memory"
  /// </summary>
  public class BS033 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS033(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      return device.WriteMem.Success;
    }
  }
}