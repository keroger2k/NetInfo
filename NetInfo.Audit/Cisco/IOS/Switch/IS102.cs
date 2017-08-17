using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Validate VLAN names and numbers match the standard
  /// </summary>
  public class IS102 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<Vlan> _vlanStandars;

    public IS102(INMCIIOSDevice device, IEnumerable<Vlan> vlanStandards) {
      this.Device = device;
      this._vlanStandars = vlanStandards;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var deviceVlans = device.Vlans;
      foreach (var vlan in deviceVlans) {
        if (!_vlanStandars.Any(c => vlan.Number == c.Number && c.ShortDescription.Trim() == vlan.ShortDescription.Trim())) {
          return false;
        }
      }
      return true;
    }
  }
}