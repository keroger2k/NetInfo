using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Ensure this command exists: no web ssl protocol sslv2
  /// </summary>
  public class RB055 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB055(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      return !device.Web.SSL.V2Enabled;
    }
  }
}