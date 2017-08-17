using System.Collections.Generic;
using NetInfo.Audit.NMCI.Models;
using NetInfo.Devices;
using NetInfo.Devices.Infrastructure.Helpers;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate assigned IP Address falls within VLAN 74 or VLAN 882 for DMZ SN40-SN59 devices
  /// </summary>
  public class IP005 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<Subnet> _allowedAddresses;

    public IP005(INMCIMcAfeeDevice device, IEnumerable<Subnet> allowedAddresses) {
      this.Device = device;
      this._allowedAddresses = allowedAddresses;
    }

    public bool Compliant() {
      var device = (INMCIMcAfeeDevice)Device;
      bool inside = false;
      foreach (var item in _allowedAddresses) {
        if (IPHelper.ip_inside_range(string.Format("{0}/{1}",
                                        item.NetworkAddress.ToString(),
                                        item.NetworkMask.ToString()),
                      device.SensorNetworkConfig.Address.ToString())) {
          inside = true;
        }
      }
      return inside;
    }
  }
}