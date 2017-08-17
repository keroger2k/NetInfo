using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure running config was saved at the beginning of the test script.
  /// </summary>
  public class IS051 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS051(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIIOSDevice)Device).WriteMem.Success;
    }
  }
}