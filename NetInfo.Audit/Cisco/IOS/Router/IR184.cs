using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure the VTY password matches the current hardening script.
  /// </summary>
  public class IR184 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> vtyPasswords;

    public IR184(INMCIIOSDevice device, IEnumerable<string> vtyPasswords) {
      this.Device = device;
      this.vtyPasswords = vtyPasswords;
    }

    public bool Compliant() {
      var vtys = ((INMCIIOSDevice)Device).Lines.Where(c => c.Type == LineType.VTY);
      var allvtyPasswords = vtys.Where(c => c.Password != null).Select(c => c.Password.Value);
      if (allvtyPasswords.Count() != vtys.Count()) { return false; }
      foreach (var item in allvtyPasswords) {
        if (!vtyPasswords.Contains(item)) {
          return false;
        }
      }
      return true;
    }
  }
}