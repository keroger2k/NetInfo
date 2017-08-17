using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure ACL99 is properly applied using the following command: "ssh access-group 99"
  /// </summary>
  public class BS004 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS004(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return ((INMCIBOSDevice)Device).SSH.AccessGroup == 99;
    }
  }
}