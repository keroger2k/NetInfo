using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure VLAN1 is not configured as tagged on any ports
  /// </summary>
  public class BS016 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS016(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      var vlan1 = device.Vlans.FirstOrDefault(c => c.Number == 1 && !c.Name.Equals("U_VTP_VLAN"));
      var vlanInfo = device.ShowVlan.Vlans.SingleOrDefault(c => c.Number == 1);
      return (vlanInfo == null || !vlanInfo.TaggedPorts.Any()) && (vlan1 == null || !vlan1.Commands.Any(c => c.Contains("tagged")));
    }
  }
}