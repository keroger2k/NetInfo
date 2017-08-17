using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure VLAN1 is not configured as untagged on any ports
  /// </summary>
  public class BS014 : ISTIGItem {

    public IDevice Device { get; private set; }

    public BS014(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      var vlanInfo = device.ShowVlan.Vlans.SingleOrDefault(c => c.Number == 1);
      return vlanInfo == null || !vlanInfo.UnTaggedPorts.Any();
    }
  }
}