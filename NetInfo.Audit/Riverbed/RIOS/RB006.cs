using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Ensure all passwords are encrypted
  /// </summary>
  public class RB006 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB006(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      return device.UserSettings.Users.All(c => c.Encryption == 7);
    }
  }
}