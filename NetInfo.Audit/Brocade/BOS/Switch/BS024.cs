using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure the "username password" command is configured per the hardening script and remove any extraneous usernames.
  /// </summary>
  public class BS024 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> _approvedUsers;

    public BS024(INMCIBOSDevice device, IEnumerable<string> approvedUsers) {
      this.Device = device;
      this._approvedUsers = approvedUsers;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      var deviceUsers = device.UserSettings.Users.Select(c => c.Name).OrderBy(c => c);
      var approvedUsers = _approvedUsers.OrderBy(c => c);
      return approvedUsers.SequenceEqual(deviceUsers);
    }
  }
}