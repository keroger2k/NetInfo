using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure the enable secret password matches the current hardening script.
  /// </summary>
  public class IS120 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> validMD5Passwords;

    public IS120(INMCIIOSDevice device, IEnumerable<string> validMD5Passwords) {
      this.Device = device;
      this.validMD5Passwords = validMD5Passwords;
    }

    public bool Compliant() {
      var enable = ((INMCIIOSDevice)Device).EnableSecret;
      return enable != null && validMD5Passwords.Contains(enable.Value);
    }
  }
}