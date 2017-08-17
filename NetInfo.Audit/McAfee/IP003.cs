using System.Collections.Generic;
using NetInfo.Audit.NMCI.Models;
using NetInfo.Devices;
using NetInfo.Devices.Infrastructure.Helpers;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate assigned IP Address falls within VLAN 17 for SF and VLAN 15 for MSF SN60-SN79 devices
  /// </summary>
  public class IP003 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<Subnet> _allowedAddresses;

    public IP003(INMCIMcAfeeDevice device, IEnumerable<Subnet> allowedAddresses) {
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