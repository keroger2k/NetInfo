using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure the password (super-user-password) matches the current hardening script.
  /// </summary>
  public class BS034 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> validSuperPasswords;

    public BS034(INMCIBOSDevice device, IEnumerable<string> validSuperPasswords) {
      this.Device = device;
      this.validSuperPasswords = validSuperPasswords;
    }

    public bool Compliant() {
      return validSuperPasswords.Contains(((INMCIBOSDevice)Device).SuperPassword.Value);
    }
  }
}