using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure the "username password" command is configured per the hardening script and remove any extraneous usernames.
  /// </summary>
  public class IS023 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<string> __passwords;

    public IS023(INMCIIOSDevice device, IEnumerable<string> passwords) {
      this.Device = device;
      __passwords = passwords;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.UserSettings.Users.Count() == 1 && device.UserSettings.Users.All(c => __passwords.Contains(c.Password));
    }
  }
}