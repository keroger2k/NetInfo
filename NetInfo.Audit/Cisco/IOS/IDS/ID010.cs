using System;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS.IDS;

namespace NetInfo.Audit.Cisco.IOS.IDS {

  /// <summary>
  /// Validate only the following accounts exist (see comment):  "cids_admin", "arccids", "(cisco)", & "quan_admin"
  /// </summary>
  public class ID010 : ISTIGItem {

    public IDevice Device { get; private set; }

    public ID010(INMCIIDSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIDSDevice)Device;
      return device.ShowUsersAll.Users.Count() == 4 &&
        device.ShowUsersAll.Users
        .All(c =>
          c.Name.Equals("cids_admin", StringComparison.OrdinalIgnoreCase) ||
          c.Name.Equals("(cisco)", StringComparison.OrdinalIgnoreCase) ||
          c.Name.Equals("quan_admin", StringComparison.OrdinalIgnoreCase) ||
          c.Name.Equals("arccids", StringComparison.OrdinalIgnoreCase)
          );
    }
  }
}