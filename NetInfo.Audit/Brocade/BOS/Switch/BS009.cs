using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure this command exists: "console timeout 3"
  /// </summary>
  public class BS009 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS009(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIBOSDevice)Device).ConsoleTimeOut == 3;
    }
  }
}