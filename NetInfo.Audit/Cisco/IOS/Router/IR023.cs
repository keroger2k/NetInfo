using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure the "username password" command is configured per the hardening script and remove any extraneous usernames.
  /// </summary>
  public class IR023 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<string> __passwords;

    public IR023(INMCIIOSDevice device, IEnumerable<string> passwords) {
      this.Device = device;
      __passwords = passwords;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var distinctUsers = device.UserSettings.Users.GroupBy(c => c.Username).Select(c => c.First());
      return distinctUsers.Count() <= 1 && device.UserSettings.Users.All(c => __passwords.Contains(c.Password));
    }
  }
}