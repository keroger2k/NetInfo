using NetInfo.Devices;
using NetInfo.Devices.Brocade.BOS.Commands;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure SNMPv3 uses DES encryption (see "show snmp user" output)
  ///
  /// The STIG requires AES encryption.
  /// If the device does not suport AES, and uses DES or 3DES instead, the finding will be downgraded to category III.
  /// To confirm, view the "show snmp user" command output to see the following:   "privtype = des"
  /// </summary>
  public class BS030 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS030(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIBOSDevice)Device);
      return device.ShowSnmpUser != null && device.ShowSnmpUser.SnmpUser.PrivType == ShowSnmpUser.PrivType.des;
    }
  }
}