using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure running config was saved at the beginning of the test script.
  /// </summary>
  public class IR051 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR051(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIIOSDevice)Device).WriteMem.Success;
    }
  }
}