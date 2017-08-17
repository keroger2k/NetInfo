using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure this command exists: "no web-management http"
  /// </summary>
  public class BS008 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS008(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return !((INMCIBOSDevice)Device).WebManagement.Http;
    }
  }
}