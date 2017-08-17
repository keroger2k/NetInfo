using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate the admin password hash is current
  /// </summary>
  public class VP002 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> validAdminPasswords;

    public VP002(INMCIScreenOSDevice device, IEnumerable<string> validAdminPasswords) {
      this.Device = device;
      this.validAdminPasswords = validAdminPasswords;
    }

    public bool Compliant() {
      var enable = ((INMCIScreenOSDevice)Device).AdminSettings.Password;
      return validAdminPasswords.Contains(enable);
    }
  }
}