using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate only the permitted user names are configured per the appropriate hardening script
  /// </summary>
  public class VP087 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> _approvedUsers;

    public VP087(INMCIScreenOSDevice device, IEnumerable<string> approvedUsers) {
      this.Device = device;
      this._approvedUsers = approvedUsers;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      var deviceUsers = device.AdminSettings.Users.Select(c => c.Name).OrderBy(c => c);
      var approvedUsers = _approvedUsers.OrderBy(c => c);
      return approvedUsers.SequenceEqual(deviceUsers);
    }
  }
}