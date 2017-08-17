using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {
  /// <summary>
  /// Ensure the "username password" commands are configured per the hardening script

  ///
  /// Exceptions to this requirement (these cannot be deleted)
  ///   (Note: these must be configured as shown; if they are not, they must be defaulted.):
  ///  username ""administrator"" password 7 *
  ///  username ""rcud"" password 7 *
  ///  username ""root"" password 7 *
  ///  username ""vserveruser"" password 7 *"
  /// </summary>
  public class RB012 : ISTIGItem {

    public IDevice Device { get; private set; }

    private string[] defaultNames = new string[] { "administrator", "rcud", "root", "vserveruser" };
    private string[] approvedNames = new string[] { "admin", "monitor" };
    private readonly IEnumerable<string> _passwords;

    public RB012(INMCIRIOSDevice device, IEnumerable<string> passwords) {
      this.Device = device;
      this._passwords = passwords;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      var allUsers = device.UserSettings.Users.ToList();
      var defaultUsers = allUsers.Where(c => defaultNames.Contains(c.Name));
      var nondefaultUsers = allUsers.Except(defaultUsers);
      var distinctUsers = nondefaultUsers.Select(c => c.Name).Distinct();
      return defaultUsers.All(c => _passwords.Contains(c.Hash)) && distinctUsers.Count() == 2 && nondefaultUsers.All(c => approvedNames.Contains(c.Name));
    }
  }
}