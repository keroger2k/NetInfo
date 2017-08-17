using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure the "username password" command "privilege" level is set to 0
  /// </summary>
  public class IR024 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR024(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var user = device.UserSettings.Users.FirstOrDefault();
      return (user == null) ? false : user.PrivilegeLevel == 0;
    }
  }
}