using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IDS.Commands;
using NetInfo.Devices.NMCI.Cisco.IOS.IDS;

namespace NetInfo.Audit.Cisco.IOS.IDS {

  /// <summary>
  /// Validate NO user account has the following privilege: "service"
  /// </summary>
  public class ID011 : ISTIGItem {

    public IDevice Device { get; private set; }

    public ID011(INMCIIDSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIDSDevice)Device;
      return device.ShowUsersAll.Users.All(c => c.UserPrivilege != ShowUsersAll.User.Privilege.service);
    }
  }
}