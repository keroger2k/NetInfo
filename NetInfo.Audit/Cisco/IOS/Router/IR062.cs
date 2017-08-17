using System;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure SNMPv3 uses MD5 packet authentication (see "show snmp user" output) (see comment)
  ///
  /// The STIG requires SHA authentication.
  /// If the device does not suport SHA, and uses MD5 instead, the finding will be downgraded to category III.
  /// To confirm the setting, view the show snmp user command output to see the following:  Authentication Protocol: MD5
  /// </summary>
  public class IR062 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR062(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      if (device.ShowSnmpUser.UserSettings != null) {
        return device.ShowSnmpUser.UserSettings
          .All(c => c.AuthenticationProtocol != null && (c.AuthenticationProtocol.Equals("sha", StringComparison.OrdinalIgnoreCase) ||
               c.AuthenticationProtocol.Equals("md5", StringComparison.OrdinalIgnoreCase)));
      }
      return false;
    }
  }
}