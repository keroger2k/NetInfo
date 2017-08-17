using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Ensure this command exists: no web http enable
  /// </summary>
  public class RB011 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB011(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      return !device.Web.Http.Enabled;
    }
  }
}