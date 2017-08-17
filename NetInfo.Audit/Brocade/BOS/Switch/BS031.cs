using NetInfo.Devices;
using NetInfo.Devices.Brocade.BOS.Commands;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure SNMPv3 uses MD5 packet authentication (see "show snmp user" output)
  ///
  /// The STIG requires SHA authentication.
  /// If the device does not suport SHA, and uses MD5 instead, the finding will be downgraded to category III.
  /// To confirm, view the show snmp user command output to see the following:  authtype = md5
  /// </summary>
  public class BS031 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS031(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIBOSDevice)Device);
      return device.ShowSnmpUser != null && device.ShowSnmpUser.SnmpUser.AuthType == ShowSnmpUser.AuthType.md5;
    }
  }
}